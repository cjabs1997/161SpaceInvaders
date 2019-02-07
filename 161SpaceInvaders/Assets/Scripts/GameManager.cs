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

                enemyGrid[x].Add(newEnemy);
            }
        }
    }
}
