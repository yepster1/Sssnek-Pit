using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{

    [SerializeField] private NavMeshAgent navMeshAgent;
     private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = gameController.playerList[0];
        if (target != null)
        {
            navMeshAgent.SetDestination(target.transform.position);
        }
    }
}
