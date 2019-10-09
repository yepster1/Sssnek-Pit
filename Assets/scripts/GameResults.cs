using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameResults : MonoBehaviour
{

    public TMP_Text winnerLabel;
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
        // Debug.Log(winner);
        List<int> countWinners = new List<int>();
        // Debug.Log(countWinners.Count);
        // Debug.Log(countWinners.Count);

        for(int i=0; i<4;i++){
            // Debug.Log((int)Config.playerScores[i]);
            if((int)Config.playerScores[i]==winner){
                countWinners.Add(i);
                // winnerLabel.text = "Player "+ i+1+" Won!";
            }
        }

        if(countWinners.Count>1){
            string winningText ="Tie between ";
            for(int i =0; i<countWinners.Count; i++){
                Debug.Log(countWinners[i]);
                int winnerNum = (int)countWinners[i]+1;
                winningText +="Player "+winnerNum+" ,";
            }

            winnerLabel.text =winningText.Substring(0,winningText.Length-1);
        }
        else if(countWinners.Count==1){
            winnerLabel.text = "Player "+ countWinners[0]+" Won!";
        }
    }

    public void Replay(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void BackToMainMenu(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
