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
        this.playerNumber = 5 + GameStateHandler.aiList.Count * 2;
        intention = new Intention();
        init();
        navMeshAgent.speed = speed;
        aIPersonality = new Desires(0.5f, 0.5f,30);
        snakeColourSetter.SetColor(GetComponentInChildren<MeshRenderer>(), textures[0]);
    }

    void changeTarget(Vector3 whoToTarget)
    {
        target = whoToTarget;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject objectToTarget  = intention.Focus(gameObject, aIPersonality);

        Vector3 theTarget;
        if (objectToTarget == null)
        {
            Debug.Log("about to hit into something");
            theTarget = gameObject.transform.position - (gameObject.transform.forward * 20);
        }
        else
        if (objectToTarget.tag.Equals("snake"))
        {
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

    public override void setColor(Transform tail, Texture texture)
    {
        snakeColourSetter.SetColor(tail.gameObject.GetComponentInChildren<MeshRenderer>(), texture);
    }

}
