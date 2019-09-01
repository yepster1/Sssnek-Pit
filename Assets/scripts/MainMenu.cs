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
    private ArrayList alphabets = new ArrayList{"Q","W","E","R","T","Y","U","I","O","P","A","S","D","F","G","H","J","K","L","Z","X","C","V","B","N","M"};
    private ArrayList numbers = new ArrayList{"0","1","2","3","4","5","6","7","8","9"};
    private ArrayList arrows = new ArrayList{KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow,KeyCode.RightArrow};
   
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
            //Add player 1 input. There must be at leat one player.
            //Check if input is alphabet or integer.
            if(alphabets.Contains(p1LInputField.text.ToUpper()) && alphabets.Contains(p1RInputField.text.ToUpper()) ){
                player = controls.create_control( (KeyCode) System.Enum.Parse(typeof(KeyCode), p1LInputField.text.ToUpper()) , (KeyCode) System.Enum.Parse(typeof(KeyCode), p1RInputField.text.ToUpper()));
                Config.playerControls[0]= player; 
                Debug.Log("Config "+ Config.playerControls[0].Left+ " " + Config.playerControls[0].rigth);
            }

            if(alphabets.Contains(p2LInputField.text.ToUpper()) && alphabets.Contains(p2RInputField.text.ToUpper())){
                player = controls.create_control( (KeyCode) System.Enum.Parse(typeof(KeyCode), p2LInputField.text.ToUpper()) , (KeyCode) System.Enum.Parse(typeof(KeyCode), p2RInputField.text.ToUpper()));
                Config.playerControls[1]= player; 
                
                Debug.Log("Config "+ Config.playerControls[1].Left+ " " + Config.playerControls[1].rigth);
            }
            if(alphabets.Contains(p3LInputField.text.ToUpper()) && alphabets.Contains(p3RInputField.text.ToUpper())){
                player = controls.create_control( (KeyCode) System.Enum.Parse(typeof(KeyCode), p3LInputField.text.ToUpper()) , (KeyCode) System.Enum.Parse(typeof(KeyCode), p3RInputField.text.ToUpper()));
                Config.playerControls[2]= player;
                Debug.Log("Config "+ Config.playerControls[2].Left+ " " + Config.playerControls[2].rigth);
            }
            if(alphabets.Contains(p4LInputField.text.ToUpper()) && alphabets.Contains(p4RInputField.text.ToUpper())){
                player = controls.create_control( (KeyCode) System.Enum.Parse(typeof(KeyCode), p4LInputField.text.ToUpper()) , (KeyCode) System.Enum.Parse(typeof(KeyCode), p4RInputField.text.ToUpper()));
                Config.playerControls[3]= player; 
                Debug.Log("Config "+ Config.playerControls[3].Left+ " " + Config.playerControls[3].rigth);
            }
            
            if(numbers.Contains(p1LInputField.text) && numbers.Contains(p1RInputField.text)){
                player = controls.create_control( (KeyCode) System.Enum.Parse(typeof(KeyCode), "Keypad"+p1LInputField.text) , (KeyCode) System.Enum.Parse(typeof(KeyCode), "Keypad"+p1RInputField.text));
                Config.playerControls[0]= player; 
                Debug.Log("Config num "+ Config.playerControls[3].Left+ " " + Config.playerControls[3].rigth);
            }

            if(numbers.Contains(p2LInputField.text) && numbers.Contains(p2RInputField.text)){
                player = controls.create_control( (KeyCode) System.Enum.Parse(typeof(KeyCode), "Keypad"+p2LInputField.text) , (KeyCode) System.Enum.Parse(typeof(KeyCode), "Keypad"+p2RInputField.text));
                Config.playerControls[1]= player; 
                Debug.Log("Config "+ Config.playerControls[3].Left+ " " + Config.playerControls[3].rigth);
            }

            if(numbers.Contains(p3LInputField.text) && numbers.Contains(p3RInputField.text)){
                player = controls.create_control( (KeyCode) System.Enum.Parse(typeof(KeyCode), "Keypad"+p3LInputField.text) , (KeyCode) System.Enum.Parse(typeof(KeyCode), "Keypad"+p3RInputField.text));
                Config.playerControls[2]= player; 
                Debug.Log("Config "+ Config.playerControls[3].Left+ " " + Config.playerControls[3].rigth);
            }

            if(numbers.Contains(p4LInputField.text) && numbers.Contains(p4RInputField.text)){
                player = controls.create_control( (KeyCode) System.Enum.Parse(typeof(KeyCode), "Keypad"+p4LInputField.text) , (KeyCode) System.Enum.Parse(typeof(KeyCode), "Keypad"+p4RInputField.text));
                Config.playerControls[3]= player; 
                Debug.Log("Config "+ Config.playerControls[3].Left+ " " + Config.playerControls[3].rigth);
            }

           

            return true;
            
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            errorText.text="Please make sure that at leat player one has controls.";
            return false;
           
        }
    
    }    
    public  void StorePlayerDetails()
    {
        // p1LInputField.GetComponent(KeyCode);
        // player = controls.create_control( (KeyCode) System.Enum.Parse(typeof(KeyCode), "Keypad"+p1LInputField.text) , (KeyCode) System.Enum.Parse(typeof(KeyCode), "Keypad2"));
        // Config.playerControls[0]= player;
        // Debug.Log("Config "+ Config.playerControls[0].Left);
        
        

    }

}
