using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;

    void shoot(Vector2 startPos, Vector2 direction, float speed)
    {
    	GameObject newBullet = Instantiate(bullet, startPos, Quaternion.identity);
    	newBullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
    }
}
