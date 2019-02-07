using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyBulletPrefab;
    public Transform startPosition;

    private bool justSwapped;
    private float count;
    private float shootTimer;
    private float shootInterval;

    private List<List<GameObject>> enemyGrid = new List<List<GameObject>>();


    // Start is called before the first frame update
    void Start()
    {
        justSwapped = false;
        count = 0;
        shootTimer = 0;
        shootInterval = Random.Range(2.0f, 4.0f);
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if(justSwapped)
        {
            count += Time.deltaTime;
        }

        if(count>=1)
        {
            justSwapped = false;
        }

        if(shootTimer >= shootInterval)
        {
            EnemyShoot();
            shootTimer = 0;
            shootInterval = Random.Range(2.0f, 4.0f);
        }
        shootTimer += Time.deltaTime;
    }

    void SpawnEnemies()
    {
        for(int x = 0; x < 11; x++)
        {
            enemyGrid.Add(new List<GameObject>());
            for(int y = 0; y < 5; y++)
            { 
                Vector2 spawnPostion = new Vector2(startPosition.position.x + x * 1.15f, startPosition.position.y + y);
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPostion, Quaternion.identity);

                newEnemy.GetComponent<Shoot>().bullet = enemyBulletPrefab;
                newEnemy.GetComponent<EnemyScript>().moveSpeed = 2.0f;
                newEnemy.GetComponent<EnemyScript>().shootSpeed = 5.0f;
                newEnemy.GetComponent<EnemyScript>().OnWallCollide.AddListener(DropAndSwap);

                enemyGrid[x].Add(newEnemy);
            }
        }
    }

    void DropAndSwap()
    {
        if(!justSwapped)
        {
            for (int x = 0; x < 11; ++x)
            {
                for (int y = 0; y < 5; ++y)
                {
                    if(enemyGrid[x][y] != null)
                    {
                        enemyGrid[x][y].GetComponent<Transform>().position = new Vector2(enemyGrid[x][y].GetComponent<Transform>().position.x, enemyGrid[x][y].GetComponent<Transform>().position.y-1);

                        enemyGrid[x][y].GetComponent<EnemyScript>().moveSpeed = -enemyGrid[x][y].GetComponent<EnemyScript>().moveSpeed;
                        enemyGrid[x][y].GetComponent<Rigidbody2D>().velocity = new Vector2(enemyGrid[x][y].GetComponent<EnemyScript>().moveSpeed, 0.0f);
                    }
                }
            }
            justSwapped = true;
        }
    }

    private void EnemyShoot()
    {
        bool shot = false;
        while(!shot)
        {
            int shootCol = Random.Range(0, enemyGrid.Count);
            for(int n = 0; n<enemyGrid[shootCol].Count && !shot; ++n)
            {
                if(enemyGrid[shootCol][n]!=null)
                {
                    enemyGrid[shootCol][n].GetComponent<EnemyScript>().Shoot();
                    shot = true;
                }
            }
        }
    }
}
