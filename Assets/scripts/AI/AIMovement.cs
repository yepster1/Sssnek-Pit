using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : Movement
{

    int aiNumber;
    BehaviorNode behaviorNode;
    Condition condition;

    public override void CollectPoint()
    {
        points += 1;
        add_tail();
        condition.setMyScore(points);
    }

    void Start()
    {
        speed = AiConfig.speed;
        rotationSpeed = AiConfig.rotationSpeed;
        body = new List<Transform>();
        behaviorNode = new BehaviorNode();

    }

    public void start(List<int> inputs)
    {
        int amountOfAIs = inputs[0];
        aiNumber = inputs[1];
        Debug.Log("AI " + aiNumber + " started");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.smoothDeltaTime, Space.World);
        Action AIAction = behaviorNode.doAction(condition);
        if (AIAction.Equals(Action.MoveLeft))
        {
            transform.Rotate(Vector3.up * rotationSpeed * -1);
        }
        if (AIAction.Equals(Action.MoveRight))
        {
            transform.Rotate(Vector3.up * rotationSpeed * 1);
        }
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
