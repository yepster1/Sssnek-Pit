using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : BaseMovement
{

    [SerializeField] private NavMeshAgent navMeshAgent;
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
        GameObject whoToTarget = AIUtil.getClosestObject(gameObject);
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
