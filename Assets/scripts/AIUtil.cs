using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIUtil
{
    GameObject getClosestOtherSnake(GameObject me)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = me.transform.position;
        foreach (GameObject t in GameStateHandeler.playerList)
        {
            float dist = getDistance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }

        foreach(GameObject t in GameStateHandeler.aiList)
        {
            if(t==me)
            {
                continue;
            }
            float dist = getDistance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    GameObject getClosestPoint(GameObject me)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = me.transform.position;
        foreach (GameObject t in GameStateHandeler.pointList)
        {
            float dist = getDistance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    GameObject getClosestObject(GameObject me)
    {
        GameObject closestSnake = getClosestOtherSnake(me);
        GameObject closestPoint = getClosestPoint(me);
        float distanceToClosestSnake = getDistance(closestSnake.transform.position, me.transform.position);
        float distanceToClosetPoint = getDistance(closestPoint.transform.position, me.transform.position);
        if (distanceToClosestSnake  > distanceToClosetPoint)
        {
            return closestSnake;
        }
        return closestPoint;
    }

    float getDistance(Vector3 other, Vector3 me)
    {
        return Vector3.Distance(other, me);
    }
}
