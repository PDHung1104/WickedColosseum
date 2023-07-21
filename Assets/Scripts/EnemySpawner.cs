using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Fields
    // Start is called before the first frame update
    [SerializeField]
    GameObject[] enemyPrefabs;

    [SerializeField]
    GameObject[] Bosses;

    [SerializeField]
    Transform[] spawners;

    [SerializeField]
    int enemyCount = 4;

    GameObject playerTransform;
    #endregion

    #region Methods
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
            bossSpawner();
            Destroy(this);
        }
    }

    void enemySpawner(GameObject[] enemyPrefabs)
    {
        Random.InitState(System.Environment.TickCount);
        int toBeSpawnEnemy;
        int selectSpawner;
        int enemyCount = Random.Range(1, spawners.Length);
        for (int i = 0; i < enemyCount; i++) {
            toBeSpawnEnemy = Random.Range(0, enemyPrefabs.Length);
            selectSpawner = Random.Range(0, spawners.Length);
            Instantiate(enemyPrefabs[toBeSpawnEnemy], spawners[selectSpawner].position, Quaternion.identity);
        }
    }

    void bossSpawner()
    {
        Random.InitState(System.Environment.TickCount);
        int toBeSpawnBoss = Random.Range(0, Bosses.Length);

        int selectSpawner = Random.Range(1, spawners.Length);
        Instantiate(Bosses[toBeSpawnBoss], spawners[selectSpawner].position, Quaternion.identity);
        
    }
    private void OnDrawGizmos()
    {
        foreach(Transform spawner in spawners)
        {
            Gizmos.DrawWireSphere(spawner.position, 0.2f);
        }
    }

    #endregion
}
