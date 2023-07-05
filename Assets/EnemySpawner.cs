using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject enemyPrefabs;

    [SerializeField]
    GameObject levelBoss;

    GameObject enemy = null;

    [SerializeField]
    Transform leftSpawner, rightSpawner;

    [SerializeField]
    int enemyCount = 4;

    GameObject playerTransform;

    // Update is called once per frame
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player1");
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            enemyCount--;
            enemySpawner(enemyPrefabs);
        }
        if (enemyCount == 0)
        {
            Destroy(this);
        }
    }

    void enemySpawner(GameObject enemyPrefabs)
    {
        Random.InitState(System.Environment.TickCount);
        int selectSpawner = Random.Range(1, 3);

        switch (selectSpawner)
        {
            case 1:
                enemy = Instantiate(enemyPrefabs, leftSpawner.position, Quaternion.identity);
                enemy.GetComponent<AIDestinationSetter>().target = playerTransform.transform;
                break;
            case 2:
                enemy = Instantiate(enemyPrefabs, rightSpawner.position, Quaternion.identity);
                enemy.GetComponent<AIDestinationSetter>().target = playerTransform.transform;
                break;
        }
    }

    void bossSpawner()
    {
        int selectSpawner = Random.Range(1, 3);

        switch (selectSpawner)
        {
            case 1:
                enemy = Instantiate(levelBoss, leftSpawner.position, Quaternion.identity);
                enemy.GetComponent<AIDestinationSetter>().target = playerTransform.transform;
                break;
            case 2:
                enemy = Instantiate(levelBoss, rightSpawner.position, Quaternion.identity);
                enemy.GetComponent<AIDestinationSetter>().target = playerTransform.transform;
                break;
        }
    }

}
