using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider : MonoBehaviour
{
    int points = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("point"))
        {
            Destroy(collision.gameObject);
            points += 1;
        }
        if(collision.gameObject.tag.Equals("snake"))
        {
            Destroy(this);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
