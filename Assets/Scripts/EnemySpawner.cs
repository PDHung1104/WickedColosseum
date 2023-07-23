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
    int waveCount = 4;

    int currentWave;

    GameObject playerTransform;
    #endregion

    #region Methods
    // Update is called once per frame
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player1");
        currentWave = waveCount;
    }

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player1");
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            currentWave--;
            enemySpawner(enemyPrefabs);
        }
        if (currentWave == 0)
        {
            bossSpawner();
            currentWave = waveCount;
        }
    }

    void enemySpawner(GameObject[] enemyPrefabs)
    {
        Random.InitState(System.Environment.TickCount);
        int toBeSpawnEnemy;
        int selectSpawner;
        int enemyCount = Random.Range(1, 4);
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
