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

    // Update is called once per frame
    void Update()
    {
       
        target = GameStateHandeler.playerList[0];
        if (target != null)
        {

            navMeshAgent.SetDestination(target.transform.position);
        }
        moveMyTail();
    }
}
