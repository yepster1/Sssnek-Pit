using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : Movement
{
   

    void Start()
    {
        speed = AiConfig.speed;
        rotationSpeed = AiConfig.rotationSpeed;
        body = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.smoothDeltaTime, Space.World);
        if (body.Count > 0)
        {
            body[0].LookAt(transform);
            body[0].Translate(body[0].forward * speed * Time.smoothDeltaTime * 0.8f * DistanceTo(transform.position, body[0].position), Space.World);
        }
        for (int i = 1; i < body.Count; i++)
        {
            //current follows previous;
            Transform previous = body[i - 1];
            Transform current = body[i];
            current.LookAt(previous);
            current.Translate(current.forward * speed * Time.smoothDeltaTime * 0.8f * DistanceTo(previous.position, current.position), Space.World);
        }
    }
}
