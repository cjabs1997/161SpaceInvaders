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
    private Player player;
    [SerializeField] private int playerLives;
    [SerializeField] private int score;

    private List<List<GameObject>> enemyGrid = new List<List<GameObject>>();


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnHit.AddListener(LoseLife);
        justSwapped = false;
        count = 0;
        shootTimer = 0;
        playerLives = 3;
        score = 0;
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
                newEnemy.GetComponent<EnemyScript>().OnDeath.AddListener(GameOver);

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

    bool checkEmpty()
    {
        for (int x = 0; x < 11; ++x)
        {
            for (int y = 0; y < 5; ++y)
            {
                if (enemyGrid[x][y] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void EnemyShoot()
    {
        bool shot = false;
        while(!shot && !checkEmpty())
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

    private void LoseLife()
    {
        --playerLives;
        GameOver();
    }

    private void GameOver()
    {
        if (playerLives <= 0 || checkEmpty())
        {
            Time.timeScale = 0;
            player.gameObject.SetActive(false);
        }
    }
}
