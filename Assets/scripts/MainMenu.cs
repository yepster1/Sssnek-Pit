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
    public TMP_InputField  inputField;
    public TMP_InputField p1LInputField;
    public TMP_InputField p1RInputField;
    public TMP_InputField p2LInputField;
    public TMP_InputField p2RInputField;
    public TMP_InputField p3LInputField;
    public TMP_InputField p3RInputField;
    public TMP_InputField p4LInputField;
    public TMP_InputField p4RInputField;

    private void Start() {
        // p1LInputField
        // p1RInputField
        // p2LInputField
        // p2RInputField
        // p3LInputField
        // p3RInputField
        // p4LInputField
        // p4RInputField
        // for(int i = 0; i<8;++i){
        //     players.Add()
        // }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        
    }

    // public void StorePlayerDetails(string text)
    public void StorePlayerDetails()
    {

        // control = p1LInputField.text;
        // Debug.Log(control);
        
        // Debug.Log(p1LInputField.ToString());
        // // Debug.Log(p2LInputField.text);
        // if(p1LInputField.ToString().Equals("Player1LeftControl (TMPro.TMP_InputField)")){
        //     Debug.Log("Success");
        // }
        // // if(p2LInputField.ToString().Equals("Player1LeftControl (TMPro.TMP_InputField)")){
        // //     Debug.Log("Success");
        // // }
       

    }

}
