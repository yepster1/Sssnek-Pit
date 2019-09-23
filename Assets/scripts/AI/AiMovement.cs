using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : BaseMovement
{

    [SerializeField] private NavMeshAgent navMeshAgent;
    Desires aIPersonality; // Default AI
    private Vector3 target;
    Intention intention;

    // Start is called before the first frame update
    void Start()
    {
        intention = new Intention();
        init();
        navMeshAgent.speed = speed;
        aIPersonality = new Desires(0.5f, 0.5f,30);
        snakeColourSetter.SetColor(4, GetComponent<SkinnedMeshRenderer>());
    }

    void changeTarget(Vector3 whoToTarget)
    {
        target = whoToTarget;
    }
    // Update is called once per frame
    void Update()
    {
        GameObject objectToTarget = intention.Focus(gameObject, aIPersonality);
        if(objectToTarget == null)
        {
            Debug.Log("about to hit into something");
            return;
        }
        Vector3 theTarget;
        if (objectToTarget.tag.Equals("snake"))
        {
            Debug.Log("Cutting off snake");
            theTarget = Perception.getPositionToCutOfSnake(gameObject, objectToTarget);
        }
        else
        {
            theTarget = objectToTarget.transform.position;
        }
        if (theTarget != target) {
            changeTarget(target);
        }
        navMeshAgent.SetDestination(theTarget);
        transform.LookAt(theTarget);
        moveMyTail(MaxSpeed, MinSpeed);
        moveAura();
    }

    public override void setColor(Transform tail)
    {
        snakeColourSetter.SetColor(4, tail.gameObject.GetComponent<SkinnedMeshRenderer>());
    }

}
