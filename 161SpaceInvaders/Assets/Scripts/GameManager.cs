using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyBulletPrefab;
    public Transform startPosition;

    private List<List<GameObject>> enemyGrid = new List<List<GameObject>>();


    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies()
    {
        for(int x = 0; x < 11; x++)
        {
            enemyGrid.Add(new List<GameObject>());
            for(int y = 0; y < 5; y++)
            {
                Vector2 spawnPostion = new Vector2(startPosition.position.x + x, startPosition.position.y + y);
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPostion, Quaternion.identity);

                newEnemy.GetComponent<Shoot>().bullet = enemyBulletPrefab;
                newEnemy.GetComponent<EnemyScript>().moveSpeed = 2.0f;
                newEnemy.GetComponent<EnemyScript>().OnWallCollide.AddListener(DropAndSwap);

                enemyGrid[x].Add(newEnemy);
            }
        }
    }

    void DropAndSwap()
    {
        for (int x = 0; x < 11; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if(enemyGrid[x][y] != null)
                {
                    enemyGrid[x][y].GetComponent<Transform>().position = new Vector2(enemyGrid[x][y].GetComponent<Transform>().position.x, enemyGrid[x][y].GetComponent<Transform>().position.y-1);
                    enemyGrid[x][y].GetComponent<Rigidbody2D>().velocity = new Vector2(-enemyGrid[x][y].GetComponent<Rigidbody2D>().velocity.x, 0.0f);
                }
            }
        }
    }
}
