﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Nut : MonoBehaviour {

    [SerializeField]
    private float speed;

    [SerializeField]
    float timer;

    private Rigidbody2D myRigidbody;

    private Vector2 direction;

	// Use this for initialization
	void Start () {

        myRigidbody = GetComponent<Rigidbody2D>();

        
        Destroy(gameObject, timer);
	}

    void FixedUpdate()
    {
        myRigidbody.velocity = direction * speed;
        
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }
	
    
}
