using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GameObject spawning;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawn", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static Vector3 move()
    {
        return new Vector3(Random.Range(-Config.MAP_LENGTH, Config.MAP_LENGTH), 1, Random.Range(-Config.MAP_WIDTH, Config.MAP_WIDTH));
    }
    void spawn()
    {
        Instantiate(spawning, new Vector3(Random.Range(-Config.MAP_LENGTH, Config.MAP_LENGTH), 1, Random.Range(-Config.MAP_WIDTH, Config.MAP_WIDTH)), new Quaternion());
    }
}
