using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResults : MonoBehaviour
{
    float time=0;
    // Start is called before the first frame update
    void Start()
    {
        DisplayResults();
    }

    // Update is called once per frame
    void DisplayResults()
    {   
        
        Debug.Log("Player1: "+Config.playerScores[0]+" Player2: "+Config.playerScores[1]+" Player3: "+Config.playerScores[2]+" Player4: "+Config.playerScores[3]);
        int winner = Mathf.Max((int)Config.playerScores[0],(int)Config.playerScores[1],(int)Config.playerScores[2],(int)Config.playerScores[3]);
        Debug.Log(winner);
        for(int i=0; i<4;i++){
            Debug.Log((int)Config.playerScores[i]);
            // if((int)Config.playerScores[i]==i){
            // }
        }
    }
}
