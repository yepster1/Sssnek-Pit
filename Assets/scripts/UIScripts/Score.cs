using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Score : MonoBehaviour
{
    public static void getScore()
    {
        Debug.Log("Score is: "  );
    }
}
public  struct score
{
    public static score setScore(int totScore){
        score totalScore = new score();
        totalScore.currentScore=totScore;
        return totalScore;
    }
    public int currentScore;
    
}