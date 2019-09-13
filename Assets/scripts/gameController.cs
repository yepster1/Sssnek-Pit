using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  struct totalPlayers
{
    public static totalPlayers addTotalPlayers(int pnum){
        totalPlayers player = new totalPlayers();
        player.numOfPlayers=pnum;
        return player;
    }
    public int numOfPlayers;
    
}
public class gameController : MonoBehaviour
{
    public GameObject point_prefab;
    public GameObject powerup_prefab;
    public GameObject player_prefab;
    public GameObject AI_prefab;
    
    private  int numOfPlayers;

    // Start is called before the first frame update

   private void Start()
    {
        InvokeRepeating("Spawn", 0.5f, 0.5f);
        InvokeRepeating("SpawnPowerups", 0.5f , 3f);
        GameStateHandler.playerList = new List<GameObject>();
        GameStateHandler.aiList = new List<GameObject>(); 
        GameStateHandler.pointList = new List<GameObject>();
        GameStateHandler.powerupsList = new List<GameObject>();
        // numOfPlayers = totPlayers.numOfPlayers;
        // AddPlayers(numOfPlayers);
        Debug.Log("In GAME control");
        // numOfPlayers = ;
        AddPlayers(MainMenu.totPlayers.numOfPlayers == 0 ? 1 :  MainMenu.totPlayers.numOfPlayers);
        AddAIs(4);
    }
    
    public void AddAIs(int amountOfAI)
    {
        for(int i = 0; i < amountOfAI; i++) {
            addAI();
        }
    }

    void AddPlayers(int numberOfPlayers)
    {
        for (int i = 0; i < numberOfPlayers; i++) {
            AddPlayer(numberOfPlayers, i);
        }
    }
    private void addAI()
    {
        Instantiate(AI_prefab, GetRandomPosition(), new Quaternion());
    }

    void AddPlayer(int numberOfPlayers, int playerNumber)
    {
        Debug.Log("spawning player " + playerNumber);
        GameObject player = Instantiate(player_prefab, GetRandomPosition(), new Quaternion());
        Camera cam = player.GetComponentInChildren<Camera>();
        player.SendMessage("spawnPlayer", new List<int> { numberOfPlayers, playerNumber });
        GameStateHandler.playerList.Add(player);
    }

    void CheckIfSomeoneWon()
    {
        foreach(GameObject player in GameStateHandler.playerList)
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
        GameStateHandler.pointList.Add(Instantiate(point_prefab, GetRandomPosition(), new Quaternion()));
    }
    
    void SpawnPowerups()
    {
        GameStateHandler.powerupsList.Add(Instantiate(powerup_prefab, GetRandomPosition(), new Quaternion()));
    }
    
}
