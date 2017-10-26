﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class character : MonoBehaviour 
{

    

    

    [SerializeField]
    protected float movementSpeed;

    protected bool facingRight;

    [SerializeField]
    protected int health;

    [SerializeField]
    private BoxCollider2D clawCollider;

    public BoxCollider2D ClawCollider
    {
        get 
        { 
            return clawCollider; 
        }
        
    }

    [SerializeField]
    private List<string> damageSources;

    public abstract bool IsDead{ get; }

    public bool Attack { get; set; }

    public bool TakingDamage { get; set; }

    public Animator MyAnimator { get; private set; }

	// Use this for initialization
	public virtual void Start () 
    {
        facingRight = true;
        MyAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public abstract IEnumerator TakeDamage();

    public abstract void Death();

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y* 1, 1);
    }

    public void MeleeAttack()
    {
        ClawCollider.enabled = true;
    }

    

    public virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if (damageSources.Contains(other.tag))
	    {
            StartCoroutine(TakeDamage());		 
	    }
    }
}
