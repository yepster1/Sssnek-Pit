using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tail : MonoBehaviour
{
    public GameObject tailPrefab;
    public List<Transform> body;
    // Start is called before the first frame update
    void Start()
    {
        body = new List<Transform>();
           body.Add(transform);
    }

    private void add_tail()
    {
        Transform newPart = Instantiate(tailPrefab as GameObject, body[body.Count -1].position - body[body.Count - 1].forward, body[body.Count -1].rotation).transform;
        newPart.SetParent(transform);
        body.Add(newPart);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown("q"))
        {
            add_tail();
        }
    }

}
