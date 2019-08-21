﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GameObject point_prefab;
    public GameObject player_prefab;
    public GameObject AI_prefab;

    public static List<GameObject> playerList;
    public List<GameObject> aiList;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0.5f, 0.5f);
        playerList = new List<GameObject>();
        aiList = new List<GameObject>();
        AddPlayers(1);
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
