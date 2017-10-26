using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemy : character {


    public bool melee;

    private IEnemyState currentState;

    public GameObject Target { get; set; }

    
    [SerializeField]
    private Transform nutPos;

    [SerializeField]
    private GameObject nutPrefab;
    
    [SerializeField]
    public float meleeRange;

    [SerializeField]
    public float throwRange;

    [SerializeField]
    private Transform leftEdge;
    [SerializeField]
    private Transform rightEdge;

    bool drop = true;
    
    
    public bool InMeleeRange 
    {

        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }

            return false;
        }
    }

    public bool InThrowRange
    {

        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= throwRange;
            }

            return false;
        }
    }

    
	// Use this for initialization
	public override void Start () 
    {
        

        base.Start();

        player.Instance.Dead += new DeadEventHandler(RemoveTarget);

        ChangeState(new IdleState());
	}

    
	// Update is called once per frame
	void Update () 
    {
        if (!IsDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute();
            }
            

            LookAtTarget();
        } 
	}

    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new PatrolState());
    }

    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;

            if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                ChangeDirection();
            }
        }
    }

    

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void ThrowNut(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(nutPrefab, nutPos.position, Quaternion.Euler(new Vector3(0,0,-90)));
            tmp.GetComponent<Nut>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(nutPrefab, nutPos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<Nut>().Initialize(Vector2.left);
        }
    }
    public void Move()
    {
        if (!Attack && melee)
        {
            if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
            {
                MyAnimator.SetFloat("speed", 1);

                transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));            
            }
            else if (currentState is PatrolState)
            {
                ChangeDirection();
            }
        }
        
   
    }

    

    

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }

    public override IEnumerator TakeDamage()
    {
        health -= 1;

        if (!IsDead)
        {
            MyAnimator.SetTrigger("dmg");
        }
        else
        {
            if (drop)
            {
                int rnd = UnityEngine.Random.Range(1, 10);
                if (rnd == 1)
                {
                    GameObject bBerry = (GameObject)Instantiate(GameManager.Instance.BlueBerry, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity);

                    Physics2D.IgnoreCollision(bBerry.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                    drop = false;
                }
            }
            
            
            MyAnimator.SetTrigger("death");
            yield return null;
        }
    }

    public override bool IsDead
    {
        get 
        {
            return health <= 0;
        }
    }

    public override void Death()
    {
        
        Destroy(gameObject);
    }
}
