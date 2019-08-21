using System;
using System.Collections;
using UnityEngine;

public class Condition
{

    float myScore;
    GameObject[] otherPlayers;
    Transform[] points;

    public Condition()
    {
    }

    public void setPlayer(int playerNumber, GameObject player)
    {
        this.otherPlayers[playerNumber] = player;
    }

    public void setPoints(Transform[] points)
    {
        this.points = points;
    }

    public void setMyScore(float score)
    {
        this.myScore = score;
    }
}
