using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D m_rgb2d;
    private Transform m_transform;
    private Shoot m_shoot;

    public float shootSpeed = 5.0f;
    public float moveSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_rgb2d = this.GetComponent<Rigidbody2D>();
        m_transform = this.GetComponent<Transform>();
        m_shoot = this.GetComponent<Shoot>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Move()
    {
        float movementModifier = Input.GetAxisRaw("Horizontal");

        Vector2 currentVelocity = m_rgb2d.velocity;
        m_rgb2d.velocity = new Vector2(movementModifier * moveSpeed, m_rgb2d.velocity.y);
    }

    private void Shoot()
    {
        Vector2 bulletSpawn = new Vector2(m_transform.position.x, m_transform.position.y + 0.5f);
        m_shoot.shoot(m_transform.position, Vector2.up, shootSpeed);
    }

}
