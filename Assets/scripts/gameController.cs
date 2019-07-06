using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GameObject spawning;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawn", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawn()
    {
        Instantiate(spawning, new Vector3(Random.RandomRange(-250, 250), 1, Random.RandomRange(-250, 250)), new Quaternion());
    }
}
