using UnityEngine;
using System.Collections;

public static class Perception
{
    
    public static GameObject getClosestOtherSnake(GameObject me)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = me.transform.position;
        foreach (GameObject snake in GameStateHandler.playerList)
        {
            float dist = getDistance(snake.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = snake;
                minDist = dist;
            }
        }

        foreach (GameObject snake in GameStateHandler.aiList)
        {
            if (snake == me)
            {
                continue;
            }
            float dist = getDistance(snake.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = snake;
                minDist = dist;
            }
        }
        return tMin;
    }

    public static GameObject getHighestScore(GameObject me)
    {
        int highestScore = 0;
        GameObject playerWithHighest = null;
        foreach (GameObject snake in GameStateHandler.playerList)
        {
            BaseMovement movement = snake.GetComponent<BaseMovement>();
            if (highestScore < movement.points)
            {
                playerWithHighest = snake;
                highestScore = movement.points;
            }
        }
        return playerWithHighest;
    }

    public static RaycastHit getDistanceToCollision(GameObject me)
    {
        int layerMask = 1 << 8;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(me.transform.position, me.transform.TransformDirection(Vector3.forward), out hit, 10, layerMask))
        {
            Debug.DrawRay(me.transform.position, me.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(me.transform.position, me.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
        return hit;
    }

    public static GameObject getClosestPoint(GameObject me)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = me.transform.position;
        foreach (GameObject t in GameStateHandler.pointList)
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

    public static Vector3 getPositionToCutOfSnake(GameObject me, GameObject snake)
    {
        Transform t = snake.transform;
        return t.position + (t.forward * 20);
    }

    public static float getDistance(Vector3 other, Vector3 me)
    {
        return Vector3.Distance(other, me);
    }
}
