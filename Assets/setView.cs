using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setView : MonoBehaviour
{
    private Camera cam;
    public Rect view;
    void Start()
    {
        cam = Camera.main;
        cam.rect = view;
    }

    void Update()
    {
        if (Input.GetKey("space"))
        {
            // choose the margin randomly
            float margin = Random.Range(0.0f, 0.3f);
            // setup the rectangle
            
        }
    }
}
