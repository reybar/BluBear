using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public delegate void DeadEventHandler();

public class player : character
{



    private static player instance;

    public event DeadEventHandler Dead;

    [SerializeField]
    private Stat healthStat;

    public static player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<player>();
            }
            return instance;
        }
    }

    private SpriteRenderer spriteRenderer;

    private bool immortal = false;

    [SerializeField]
    private float immortalTime;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;
    
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private bool airControl;

    public Rigidbody2D MyRigidbody { get; set; }


    public bool Jump { get; set; }

    public bool OnGround { get; set; }

    public override bool IsDead
    {
        get
        {
            if (healthStat.CurrValue <= 0)
            {
                OnDead();
            }

            return healthStat.CurrValue <= 0;
        }
    }

    //private Vector2 startPos;
    

	// Use this for initialization
	public override void Start () 
    {
        base.Start();

        //startPos = transform.position;﻿

        spriteRenderer = GetComponent<SpriteRenderer>();

        MyRigidbody = GetComponent<Rigidbody2D>();

        healthStat.Initialize();
        
    }
    void Update()
    {
        if (!TakingDamage && !IsDead)
        {
            if (transform.position.y <= -5)
            {
                Death();

            }
            HandleInput();
        }
        

        
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {

        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");

            OnGround = IsGrounded();

            HandleMovement(horizontal);

            Flip(horizontal);

            HandleLayers();
           
            
        }
        
        
	}


    public void OnDead()
    {
        if (Dead != null)
        {
            Dead();
        }
    }

    private void HandleMovement(float horizontal)
    {
        if (MyRigidbody.velocity.y <0)
	    {
		  MyAnimator.SetBool("land", true);
	    }
        if (!Attack && (OnGround || airControl) )
	    {
		  MyRigidbody.velocity = new Vector2 (horizontal*movementSpeed, MyRigidbody.velocity.y);
	    }
        if (Jump && MyRigidbody.velocity.y == 0)
	    {
		 MyRigidbody.AddForce(new Vector2(0, jumpForce));
	    }
        

        MyAnimator.SetFloat("speed",Mathf.Abs(horizontal));
    }

    

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MyAnimator.SetTrigger("jump");
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            MyAnimator.SetTrigger("attack");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MyAnimator.SetBool("duck", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            MyAnimator.SetBool("duck", false);
        }
    }
    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();

        }

    }
   
    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }

        }
            return false;
    }
    private void HandleLayers()
    {
        if (!OnGround)
	    {
            MyAnimator.SetLayerWeight(1, 1);
	    }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }

    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }
    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {
            healthStat.CurrValue -= 1;

            if (!IsDead)
            {
                MyAnimator.SetTrigger("dmg");

                immortal = true;

                StartCoroutine(IndicateImmortal());

                yield return new WaitForSeconds(immortalTime);

                immortal = false;
            }
            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("death");
            }
        }
    }



    public override void Death()
    {
        SceneManager.LoadScene("Scene1");
       /* MyRigidbody.velocity = Vector2.zero;
        MyAnimator.SetTrigger("idle");
        healthStat.CurrValue = healthStat.MaxVal;
        transform.position = startPos;*/


        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "BBerry")
        {
            Destroy(other.gameObject);
            healthStat.CurrValue += 1;
        }
    }
}

