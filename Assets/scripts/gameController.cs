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
        add_players(2);
    }

    void add_players(int numberOfPlayers)
    {
        for (int i = 0; i < numberOfPlayers; i++) {
            Debug.Log("spawning player " + i);
            GameObject player = Instantiate(player_prefab, get_random_position(), new Quaternion());
            Camera cam  = player.GetComponentInChildren< Camera > ();
            player.SendMessage("start", i);
            playerList.Add(player);
        }
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
