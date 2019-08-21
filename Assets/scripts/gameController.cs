using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GameObject point_prefab;
    public GameObject player_prefab;
    public GameObject AI_prefab;

    public List<GameObject> playerList;
    public List<GameObject> aiList;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0.5f, 0.5f);
        playerList = new List<GameObject>();
        aiList = new List<GameObject>();
        AddPlayers(2);
        addAIs(20);
    }

    void addAIs(int numberOfAI)
    {
        for (int i = 0; i < numberOfAI; i++)
        {
            addAI(numberOfAI, i);
        }
    }

    void addAI(int numberOfAI, int AINumber)
    {
        Debug.Log("spawning AI " + AINumber);
        GameObject AI = Instantiate(AI_prefab, GetRandomPosition(), new Quaternion());
        AI.SendMessage("start", new List<int> { numberOfAI, AINumber });
        aiList.Add(AI);
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
        player.SendMessage("start", new List<int> { numberOfPlayers, playerNumber });
        playerList.Add(player);
    }

    void CheckIfSomeoneWon()
    {
        foreach(GameObject player in playerList)
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
         Instantiate(point_prefab, GetRandomPosition(), new Quaternion());
    }
}
