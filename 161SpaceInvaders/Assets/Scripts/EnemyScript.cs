﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyEvent : UnityEvent{}

public class EnemyScript : MonoBehaviour
{
	public EnemyEvent OnWallCollide = new EnemyEvent();
	public float moveSpeed;
	private Rigidbody2D m_rigidbody;

	void Awake()
	{
		m_rigidbody = this.GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		m_rigidbody.velocity = new Vector2(moveSpeed, m_rigidbody.velocity.y);
		OnWallCollide.AddListener(dropNswap);
	}

	void dropNswap()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
		m_rigidbody.velocity = new Vector2(-m_rigidbody.velocity.x, m_rigidbody.velocity.y);
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
    	if(collision.collider.gameObject.CompareTag("Wall"))
    	{
    		OnWallCollide.Invoke();
    	}
    }
}
