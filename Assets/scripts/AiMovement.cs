using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : BaseMovement
{

    [SerializeField] private NavMeshAgent navMeshAgent;
    AIPersonality aIPersonality = new AIPersonality(50,50); // Default AI
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.speed = speed;
    }

    void changeTarget(GameObject whoToTarget)
    {
        Debug.Log("targing change");
        target = whoToTarget;
    }
    // Update is called once per frame
    void Update()
    {
        GameObject whoToTarget = AIUtil.getObjectToTarget(gameObject, aIPersonality);
        if (whoToTarget != target) {
            changeTarget(whoToTarget);
        }
        if (target != null)
        {

            navMeshAgent.SetDestination(target.transform.position);
        }
        moveMyTail();
    }
}
