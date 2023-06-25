using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    float zoom;

    float smoothTime = 0.25f;

    float velocity = 1f;
    
    Vector3 velocity3 = Vector3.zero;

    Vector3 targetPosition;

    [SerializeField]
    Vector3 offset = new Vector3(0f, -4f, 2f);

    Timer winSceneTimer;

    [SerializeField]
    float duration = 5f;

    GameObject[] players;

    [SerializeField]
    Transform SpawnPoint1, SpawnPoint2;

    #endregion
    /// <summary>
    /// Summon the characters and initialize some fields for pvp mode
    /// </summary>
    #region Methods

    void Start()
    {
        myCamera = GetComponent<Camera>();
        SpawnPosP1 = SpawnPoint1.position;
        SpawnPosP2 = SpawnPoint2.position;
        int select = Random.Range(0, 2);
        Instantiate(prefabs[select], SpawnPosP1, Quaternion.identity);
        select = Random.Range(0, 2);
        Instantiate(prefabs[select], SpawnPosP2, Quaternion.identity);
        players = GameObject.FindGameObjectsWithTag("Player");
        
        players[0].tag = "Player1";
        players[0].layer = 7;
        players[1].tag = "Player2";
        players[1].layer = 8;

        winSceneTimer = gameObject.AddComponent<Timer>();
        winSceneTimer.Duration = duration;

        //initialize the EnemyLayer for each player
        players[0].GetComponent<Control>().EnemyLayer = LayerMask.GetMask(LayerMask.LayerToName(players[1].layer));
        players[1].GetComponent<Control>().EnemyLayer = LayerMask.GetMask(LayerMask.LayerToName(players[0].layer));
    }

    void Update()
    {
        if (players[0].GetComponent<Health>().Dead)
        {
            targetPosition = players[1].transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity3, smoothTime);
            myCamera.orthographicSize = Mathf.SmoothDamp(myCamera.orthographicSize, zoom, ref velocity, smoothTime);
            winSceneTimer.Run();
            if (winSceneTimer.Finished)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        else if (players[1].GetComponent<Health>().Dead)
        {
            targetPosition = players[0].transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity3, smoothTime);
            myCamera.orthographicSize = Mathf.SmoothDamp(myCamera.orthographicSize, zoom, ref velocity, smoothTime);
            winSceneTimer.Run();
            if (winSceneTimer.Finished)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    #endregion
}
