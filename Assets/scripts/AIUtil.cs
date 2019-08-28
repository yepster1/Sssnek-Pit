using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIUtil
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

        foreach(GameObject snake in GameStateHandler.aiList)
        {
            if(snake == me)
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

    private static GameObject getIfSomeonesPointsAreHigh(AIPersonality aIPersonality)
    {
        int highestScore = 0;
        GameObject playerWithHighest = null;
        foreach (GameObject snake in GameStateHandler.playerList)
        {
            BaseMovement movement = snake.GetComponent<BaseMovement>();
            if(movement.points > aIPersonality.getKillPoint() && movement.points > highestScore)
            {
                playerWithHighest = snake;
                highestScore = movement.points;
            }
        }
        return playerWithHighest;
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

    public static Vector3 getObjectToTarget(GameObject me, AIPersonality aiPersonality)
    {
        GameObject snakeAboveThreshHoldForKill = getIfSomeonesPointsAreHigh(aiPersonality);
        if(snakeAboveThreshHoldForKill != me && snakeAboveThreshHoldForKill != null)
        {
            Debug.Log("SNAKE ABOVE THRESHHOLD. KILLING");
            return getPositionToCutOfSnake(snakeAboveThreshHoldForKill);
        }
        GameObject closestSnake = getClosestOtherSnake(me);
        GameObject closestPoint = getClosestPoint(me);
        float distanceToClosestSnake = getDistance(closestSnake.transform.position, me.transform.position);
        if(closestPoint == null)
        {
            return getPositionToCutOfSnake(closestSnake);
        }
        float distanceToClosetPoint = getDistance(closestPoint.transform.position, me.transform.position);
        if (distanceToClosestSnake * (1-aiPersonality.getAgressivness())  < distanceToClosetPoint * (1 - aiPersonality.getGreed()))
        {
            return getPositionToCutOfSnake(closestSnake);
        }
        return closestPoint.transform.position;
    }

    private static Vector3 getPositionToCutOfSnake(GameObject snake)
    {
        Transform t = snake.transform;
        return t.position + (t.forward * 20);
    }

    public static float getDistance(Vector3 other, Vector3 me)
    {
        return Vector3.Distance(other, me);
    }
}
