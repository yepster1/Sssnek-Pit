using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GameObject point_prefab;
    public GameObject player_prefab;
    public GameObject AI_prefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0.5f, 0.5f);
        GameStateHandeler.playerList = new List<GameObject>();
        GameStateHandeler.aiList = new List<GameObject>(); 
        GameStateHandeler.pointList = new List<GameObject>();
        AddPlayers(1);
        AddAIs(10);
    }
    
    public void AddAIs(int amountOfAI)
    {
        for(int i = 0; i < amountOfAI; i++) {
            addAI();
        }
    }

    private void addAI()
    {
        Instantiate(AI_prefab, GetRandomPosition(), new Quaternion());
    }
    void AddPlayers(int numberOfPlayers)
    {
        for (int i = 0; i < numberOfPlayers; i++) {
            AddPlayer(numberOfPlayers, i);
        }
    }

    void AddPlayer(int numberOfPlayers, int playerNumber)
    {
        Debug.Log("spawning player " + playerNumber);
        GameObject player = Instantiate(player_prefab, GetRandomPosition(), new Quaternion());
        Camera cam = player.GetComponentInChildren<Camera>();
        player.SendMessage("spawnPlayer", new List<int> { numberOfPlayers, playerNumber });
        GameStateHandeler.playerList.Add(player);
    }

    void CheckIfSomeoneWon()
    {
        foreach(GameObject player in GameStateHandeler.playerList)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public static Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-Config.MAP_LENGTH + 10, Config.MAP_LENGTH - 10 ), 1, Random.Range(-Config.MAP_WIDTH + 10 , Config.MAP_WIDTH - 10));
    }

    void Spawn()
    {
        GameStateHandeler.pointList.Add(Instantiate(point_prefab, GetRandomPosition(), new Quaternion()));
    }
}
