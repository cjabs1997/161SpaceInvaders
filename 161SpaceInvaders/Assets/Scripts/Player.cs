using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvent : UnityEvent { }

public class Player : MonoBehaviour
{
    private Rigidbody2D m_rgb2d;
    private Transform m_transform;
    private Shoot m_shoot;
    private bool shooting = true;

    public PlayerEvent OnHit = new PlayerEvent();
    public float shootSpeed = 8.0f;
    public float moveSpeed = 10.0f;
    public static Player instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rgb2d = this.GetComponent<Rigidbody2D>();
        m_transform = this.GetComponent<Transform>();
        m_shoot = this.GetComponent<Shoot>();
        GameManager.instance.EndGame.AddListener(toggleShooting);
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (shooting && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)))
        {
            if (GameObject.FindGameObjectWithTag("playerBullet") == null)
                Shoot();
        }
    }

    private void toggleShooting()
    {
        shooting = !shooting;
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
        m_shoot.shoot(bulletSpawn, Vector2.up, shootSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("enemybullet"))
        {
            OnHit.Invoke();
        }
    }
}
