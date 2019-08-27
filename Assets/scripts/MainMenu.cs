using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private ArrayList players;
    public string leftControl;
    public string rightControl;
    public GameObject leftInputField;
    public GameObject rigthInputField;
    public GameObject leftTextDisplay;
    public GameObject rightTextDisplay;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void StorePlayerDetails()
    {
        //if (leftInputField.GetComponent<Text>().text != null)
        //{
        Debug.Log(leftInputField.GetComponent<Text>().text);
        //leftControl = leftInputField.GetComponent<Text>().text;
        //leftTextDisplay.GetComponent<Text>().text = leftControl;
        //}


    }

}
