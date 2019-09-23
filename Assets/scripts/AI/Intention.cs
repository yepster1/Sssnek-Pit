using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Intention
{
    
    //Get Point
    //Get Powerup
    //Activate Powerup
    //Get Attack player
    GameObject lastTarget;

    public GameObject Focus(GameObject me, Desires desires)
    {
        GameObject closestPoint = Perception.getClosestPoint(me);
        float distanceToClosestPoint = Perception.getDistance(me.transform.position, closestPoint.transform.position);
        GameObject closetSnake = Perception.getClosestOtherSnake(me);
        RaycastHit hit = Perception.getDistanceToCollision(me);
        float distanceToClosestSnake = Perception.getDistance(me.transform.position, closetSnake.transform.position);
        GameObject highestScore = Perception.getHighestScore(me);
        if (hit.rigidbody != null && hit.rigidbody.gameObject.tag.Equals("snake"))
        {
            return null;
        }
        if (highestScore != null)
        {
            float distanceToHighestScore = Perception.getDistance(me.transform.position, closestPoint.transform.position);

            Movement highestScoreMovement = highestScore.GetComponent<Movement>();
            if (highestScoreMovement.points > desires.getFrenzyPoint())
            {
                Debug.Log("Frenzy");
                return highestScore;
            }
        }
        Debug.Log(distanceToClosestPoint + " " + distanceToClosestSnake );
        if(distanceToClosestPoint * desires.getGreed() < distanceToClosestSnake * desires.getBloodlust())
        {
            Debug.Log("targeting point");
            return closestPoint;
            
        } else
        {
            Debug.Log("targeting snake");
            return closetSnake;
        }
        
    }

    private GameObject getPoint(GameObject me)
    {
        return Perception.getClosestPoint(me);
    }

    private GameObject activatePowerup(GameObject me)
    {
        return null;
    }

    private GameObject getPowerup(GameObject me)
    {
        return null;
    }

    private GameObject attackPlayer(GameObject me)
    {
        return null;
    }

    private GameObject dodge()
    {
        return null;
    }
}
