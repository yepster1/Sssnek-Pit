using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : BaseMovement
{

    [SerializeField] private NavMeshAgent navMeshAgent;
    AIPersonality aIPersonality; // Default AI
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.speed = speed;
        aIPersonality = new AIPersonality(0.5f, 0.5f,3);
    }

    void changeTarget(Vector3 whoToTarget)
    {
        target = whoToTarget;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 theTarget = AIUtil.getObjectToTarget(gameObject, aIPersonality);
        if (theTarget != target) {
            changeTarget(target);
        }
        navMeshAgent.SetDestination(theTarget);
        moveMyTail();
        moveAura();
    }


}
