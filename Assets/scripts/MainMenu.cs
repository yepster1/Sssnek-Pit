using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    private ArrayList players;
    public string control;
    public TMP_Text errorText;
    public TMP_InputField p1LInputField;
    public TMP_InputField p1RInputField;
    public TMP_InputField p2LInputField;
    public TMP_InputField p2RInputField;
    public TMP_InputField p3LInputField;
    public TMP_InputField p3RInputField;
    public TMP_InputField p4LInputField;
    public TMP_InputField p4RInputField;
    public static controls player;

   
    public void PlayGame()
    {
        if(CheckPlayerInput()){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }

    }

    private bool CheckPlayerInput(){
        errorText.text ="";
        try
        {
            //Add player 1 input. Theremust be at leat one player.
            player = controls.create_control( (KeyCode) System.Enum.Parse(typeof(KeyCode), p1LInputField.text) , (KeyCode) System.Enum.Parse(typeof(KeyCode), p1RInputField.text));
            Config.playerControls[0]= player; 
            Debug.Log("Config "+ Config.playerControls[0].Left+ " " + Config.playerControls[0].rigth);

            if(!(p2LInputField.text.Equals("") && p2RInputField.text.Equals(""))){
                player = controls.create_control( (KeyCode) System.Enum.Parse(typeof(KeyCode), p2LInputField.text) , (KeyCode) System.Enum.Parse(typeof(KeyCode), p2RInputField.text));
                Config.playerControls[1]= player; 
                Debug.Log("Config "+ Config.playerControls[1].Left+ " " + Config.playerControls[1].rigth);
            }
            if(!(p3LInputField.text.Equals("") && p3RInputField.text.Equals(""))){
                player = controls.create_control( (KeyCode) System.Enum.Parse(typeof(KeyCode), p3LInputField.text) , (KeyCode) System.Enum.Parse(typeof(KeyCode), p3RInputField.text));
                Config.playerControls[2]= player;
                Debug.Log("Config "+ Config.playerControls[2].Left+ " " + Config.playerControls[2].rigth);
            }
            if(!(p4LInputField.text.Equals("") && p4RInputField.text.Equals(""))){
                player = controls.create_control( (KeyCode) System.Enum.Parse(typeof(KeyCode), p4LInputField.text) , (KeyCode) System.Enum.Parse(typeof(KeyCode), p4RInputField.text));
                Config.playerControls[3]= player; 
                Debug.Log("Config "+ Config.playerControls[3].Left+ " " + Config.playerControls[3].rigth);
            }

           

            return true;
            
        }
        catch (System.Exception)
        {
            errorText.text="Please make sure that at leat player one has controls.";
            return false;
           
        }
    
    }    
    public  void StorePlayerDetails()
    {

        if(!p1LInputField.text.Equals("")){
        player = controls.create_control( (KeyCode) System.Enum.Parse(typeof(KeyCode), p1LInputField.text) , (KeyCode) System.Enum.Parse(typeof(KeyCode), "R"));
        Config.playerControls[0]= player;
        Debug.Log("Config "+ Config.playerControls[0].Left);
        }
        if(p1LInputField.text.Equals("")){
            Debug.Log("Empty Field");
        }
        

    }

}
