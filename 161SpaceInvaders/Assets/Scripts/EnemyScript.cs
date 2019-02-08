using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyEvent : UnityEvent{}

public class EnemyScript : MonoBehaviour
{
	public EnemyEvent OnWallCollide = new EnemyEvent();
    public EnemyEvent OnDeath = new EnemyEvent();
	public float moveSpeed;
    public float shootSpeed;
    public int points;

    private Shoot m_shoot;
    private Rigidbody2D m_rigidbody;
    private Transform m_transform;

	void Awake()
	{
		m_rigidbody = this.GetComponent<Rigidbody2D>();
        m_transform = this.GetComponent<Transform>();
        m_shoot = this.GetComponent<Shoot>();
    }

	void Start()
	{
		m_rigidbody.velocity = new Vector2(moveSpeed, m_rigidbody.velocity.y);
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
    	if(collider.gameObject.CompareTag("Wall"))
    	{
    		OnWallCollide.Invoke();
    	}

        if (collider.gameObject.CompareTag("playerBullet"))
        {
            OnDeath.Invoke();
            Destroy(this.gameObject);
        }
    }

    public void Shoot()
    {
        Vector2 bulletSpawn = new Vector2(m_transform.position.x, m_transform.position.y - 0.5f);
        m_shoot.shoot(bulletSpawn, Vector2.down, shootSpeed);
    }
}
