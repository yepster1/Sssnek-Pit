using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Tail1 : MonoBehaviour
{
    public GameObject tailPrefab;
    public GameObject head;
    private BaseMovement movement;
    private int myPlayerNum;

    private List <Collision> currentCollisions = new List <Collision> (); //used to test collisions with other objects

    public void setHead(GameObject _head)
    {
        head = _head;
        tailPrefab = this.gameObject;
        movement = _head.GetComponent<BaseMovement>();
        myPlayerNum = movement.playerNumber;
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
            return tailPrefab;
        }
        
    }

    //only check collisions with tail
    //head collisions are already checked in BaseMovement
    protected void OnCollisionEnter(Collision collision)
    {
        currentCollisions.Add(collision);
        foreach (Collision gObject in currentCollisions) {
            // int colPlayerNum = -1; //not set yet
            if (collision.gameObject.name.Substring(0,4) == "tail"){
               int tailPlayerNum = ConvertToInt(collision.gameObject.name.Substring(5,1));
                //    if (movement != null){
                //        Debug.Log ("movement set in oncollision enter");
                //    }else{
                //        Debug.Log("movement **not** set in onCollisionEnter for p: "+ myPlayerNum);
                //    }

                //if (gObject.gameObject.tag == "snake" && tailPlayerNum != myPlayerNum && !(myPlayerNum > 4 || tailPlayerNum > 4))
                //{
                //    // Debug.Log("myPlayerNum: " + myPlayerNum);
                //    // Debug.Log(" vs tailPlayerNum: " + tailPlayerNum);
                //    // Debug.Log(" colliding with player: " + tailPlayerNum);
                //    CollideWithOtherTail(gObject);
                //}
            }
        }
    }

    protected void OnCollisionExit (Collision collision) 
    {
        // Remove the GameObject collided with from the list.
        currentCollisions.Remove (collision);
    }

    protected void CollideWithOtherTail(Collision collision)
    {
        Tail1 tail = collision.gameObject.GetComponent<Tail1>();
        if (tail != null){
            if (collision.gameObject.name.Substring(0,4) == "tail"){
                int tailPlayerNum = ConvertToInt(collision.gameObject.name.Substring(5,1));
                Debug.Log("in CollideWithOtherTail: " + myPlayerNum + " vs tailPlayerNum: " + tailPlayerNum);
                // Debug.Log("myPlayerNum: " + myPlayerNum + " vs tailPlayerNum: " + tailPlayerNum);
                if (tailPlayerNum != myPlayerNum){
                    Debug.Log("hit tail");
                    Debug.Log("player number: " +collision.gameObject.name.Substring(5,1));
                    Debug.Log("my number: " +myPlayerNum);
                    DestroyTail(collision.gameObject.name);
                }
            }
        }
    }

    private void DestroyTail(string tailWithPlayerNumber)
    {   //note: here tail will be 'tailP<playernumber><bodyIndex>
        //ie tailP111 is player1's 11th item in the body
        int playerToHit = ConvertToInt(tailWithPlayerNumber.Substring(5,1)); //val after `tailP`
        
        if (playerToHit != myPlayerNum){
            int tailNum = ConvertToInt(tailWithPlayerNumber.Substring(6)); //get index of where snake was hit eg after `tailP1`
            //delete items from the end of the body to index
            for (int j = GameStateHandler.playerList[playerToHit].GetComponent<Movement>().body.Count-1 ; j > tailNum ; j--  ){
                Destroy(GameStateHandler.playerList[playerToHit].GetComponent<Movement>().body[j]);
            }
            Destroy(this.gameObject);
        }else {
            return;
        }
    }

    private int ConvertToInt(string itemToCheck)
    {
        int itemToInt;
        
        bool success = Int32.TryParse(itemToCheck, out itemToInt); 
        if (success)
        {
            // Debug.Log("Converted: "+ itemToCheck+ " to int: " + partForPlayerNum); 
            return itemToInt;        
        }
        else
        {
            Console.WriteLine("Conversion failed");
            return -1;
        }
    }
}
