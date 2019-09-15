using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public GameObject tailPrefab;
    public GameObject head;
    // private string name;
   
    public void setHead(GameObject _head){
        head = _head;
    }
    public GameObject add_tail(string _name, GameObject _newTail, int _tailNumber)
    {
        if(head != null){
            GameObject newTail = _newTail;
            if (_name == "player0"){
                newTail.name = "tailP0";
            }
            if (_name == "player1"){
                newTail.name = "tailP1";
            }
            if (_name == "player2"){
                newTail.name = "tailP2";
            }
            if (_name == "player3"){
                newTail.name = "tailP3";
            }
            newTail.name += _tailNumber;
            return newTail;
            // Debug.Log("tail: " +newPart.name.Substring(6));
        }else {
            return this.gameObject;
        }
        
    }
    // Update is called once per frame
    // void FixedUpdate()
    // {
    //     if(Input.GetKeyDown("q"))
    //     {
    //         add_tail();
    //     }
    // }

}
