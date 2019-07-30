using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorNode
{
    BehaviorNode leftChild;
    BehaviorNode rightChild;

    public Action doAction(Condition condition) 
    {
        return Action.MoveLeft;
    }
    
}
