using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GameObject point_prefab;
    public GameObject player_prefab;

    public List<GameObject> playerList;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawn", 0.5f, 0.5f);
        playerList = new List<GameObject>();
        add_players(4);
    }

    void add_players(int numberOfPlayers)
    {
        for (int i = 0; i < numberOfPlayers; i++) {
            add_player(numberOfPlayers, i);
        }
    }

    void add_player(int numberOfPlayers, int playerNumber)
    {
        Debug.Log("spawning player " + playerNumber);
        GameObject player = Instantiate(player_prefab, get_random_position(), new Quaternion());
        Camera cam = player.GetComponentInChildren<Camera>();
        player.SendMessage("start", new List<int> { numberOfPlayers, playerNumber });
        playerList.Add(player);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Vector3 get_random_position()
    {
        return new Vector3(Random.Range(-Config.MAP_LENGTH, Config.MAP_LENGTH), 1, Random.Range(-Config.MAP_WIDTH, Config.MAP_WIDTH));
    }

    void spawn()
    {
         Instantiate(point_prefab, new Vector3(Random.Range(-Config.MAP_LENGTH, Config.MAP_LENGTH), 1, Random.Range(-Config.MAP_WIDTH, Config.MAP_WIDTH)), new Quaternion());
    }
}
