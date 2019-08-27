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
    // public string rightControl;
    public TMP_InputField inputField;
    // public TMP_InputField rigthInputField;
    public GameObject leftTextDisplay;
    public GameObject rightTextDisplay;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    // public void StorePlayerDetails(string text)
    public void StorePlayerDetails()
    {
        //if (leftInputField.GetComponent<Text>().text != null)
        //{
        // Debug.Log(text);
        // leftInputField.AddComponent
        control = inputField.text;
        Debug.Log(control);
        Debug.Log(inputField.ToString());

        // leftControl = text;
        // leftTextDisplay.GetComponent<Text>().text = leftControl;
        //}
    }

}
