using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    #region Fields

    [SerializeField]
    GameObject[] prefabs = new GameObject[2]; //the prefabs for player1 and player2

    Vector3 SpawnPosP1, SpawnPosP2;

    LayerMask enemyLayer;

    Camera myCamera;

    [SerializeField]
    float zoom = 0.5f;
    #endregion
    /// <summary>
    /// Summon the characters and initialize some fields for pvp mode
    /// </summary>
    #region Methods

    void Start()
    {

        //for testing only
        SpawnPosP1 = new Vector3((float)-1.32299995, (float) -0.050999999, (float)0.539534032);
        SpawnPosP2 = new Vector3((float)1.46500003, (float)-0.0799999982, (float)0.539534032);
        int select = Random.Range(0, 2);
        Instantiate(prefabs[select], SpawnPosP1, Quaternion.identity);
        select = Random.Range(0, 2);
        Instantiate(prefabs[select], SpawnPosP2, Quaternion.identity); 
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        myCamera = GetComponent<Camera>();
        players[0].tag = "Player1";
        players[0].layer = 7;
        players[1].tag = "Player2";
        players[1].layer = 8;

        //initialize the EnemyLayer for each player
        players[0].GetComponent<Control>().EnemyLayer = LayerMask.GetMask(LayerMask.LayerToName(players[1].layer));
        players[1].GetComponent<Control>().EnemyLayer = LayerMask.GetMask(LayerMask.LayerToName(players[0].layer));

        if (players[0].GetComponent<Health>().Dead)
        {
            gameObject.transform.position = new Vector3(players[1].transform.position.x, players[1].transform.position.y, players[1].transform.position.z);
            myCamera.orthographicSize = zoom;
        } else if (players[1].GetComponent<Health>().Dead)
        {
            gameObject.transform.position = new Vector3(players[0].transform.position.x, players[0].transform.position.y, players[0].transform.position.z);
            myCamera.orthographicSize = zoom;
        }
    }

    #endregion
}
