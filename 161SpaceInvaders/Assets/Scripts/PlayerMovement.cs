using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_rgb2d;
    public int speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        m_rgb2d = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float movementModifier = Input.GetAxisRaw("Horizontal");

        Vector2 currentVelocity = m_rgb2d.velocity;
        m_rgb2d.velocity = new Vector2(movementModifier * speed, m_rgb2d.velocity.y);
    }
}