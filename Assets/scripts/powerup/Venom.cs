using System.Collections;
using System; //for int parsing
using System.Collections.Generic;
using UnityEngine;

public class Venom: MonoBehaviour
{
    public Rigidbody venomObjectRB;
    public float speed = 0.001f;
    public bool activate;
    private string myName;
    public int myPlayerNum;
    //for venom
    public List<GameObject> myOtherPlayers;

    private VenomShootingScript vss;
    private float maxTimeToShoot;
    private float shootTimer;
    
    List <Collider> currentCollisions = new List <Collider> ();

    private static int count =0;

    // Update is called once per frame
    public void InitVenom(int _myPlayerNum, bool _activate)
    {
        myPlayerNum = _myPlayerNum;
        if (venomObjectRB == null){
            venomObjectRB = this.gameObject.GetComponent<Rigidbody>();
        }
        activate = _activate;
        
    }

    public void activateNow(){
        activate = true;
    }
    public void deactivateNow(){
        activate = false;
    }
    void FixedUpdate(){
        if (activate){
            venomObjectRB.velocity = venomObjectRB.transform.forward * speed;
        }
        
    }

    protected void OnTriggerEnter(Collider collision)
    {
        currentCollisions.Add(collision);
        foreach (Collider gObject in currentCollisions) {
            if (gObject.gameObject.tag == "snake")
            {   
                CollideWithOtherObject(gObject);
            }
         }
        
        
        
    }

    void OnTriggerExit (Collider collision) 
    {
        // Remove the GameObject collided with from the list.
        currentCollisions.Remove (collision);
 
        // Print the entire list to the console.
        foreach (Collider gObject in currentCollisions) {
            if (gObject!= null){
                print (gObject.gameObject.name);
            }
        }
    }

    protected void CollideWithOtherObject(Collider collision)
    {
        Tail tail = collision.gameObject.GetComponent<Tail>();
        if (tail != null){
            if (collision.gameObject.name.Substring(0,4) == "tail"){
                int colPlayerNum = ConvertToInt(collision.gameObject.name.Substring(5,1));
                if (colPlayerNum != myPlayerNum){
                    Debug.Log("hit tail");
                    Debug.Log("player number: " +collision.gameObject.name.Substring(5,1));
                    Debug.Log("my number: " +myPlayerNum);
                    DestroyTail(collision.gameObject.name);
                }
            }
        }
        else{
            if (collision.gameObject.name.Substring(0,6) == "player"){
                int colPlayerNum = ConvertToInt(collision.gameObject.name.Substring(6));
                if (colPlayerNum!= myPlayerNum ){

                    Debug.Log("hit head");
                    Debug.Log( "player number: " + colPlayerNum);
                    Debug.Log("my number: " + myPlayerNum);
                    DestroyHead(collision.gameObject.GetComponent<Movement>().head.name);
                    
                }else {
                    // Debug.Log("myPlayerNum == colPlayerNum");
                }
                
            }
        }
    }

    
    private void DestroyHead(string headWithPlayerNumber)
    {
        // Debug.Log("!!!!!!!!headWithPlayerNumber: " + headWithPlayerNumber.Substring(6));
            int playerToHit = ConvertToInt(headWithPlayerNumber.Substring(6));
            // Debug.Log("player to hit: " + playerToHit);
            // Debug.Log("my player num: " + myPlayerNum);
            if (playerToHit != myPlayerNum){
                GameStateHandler.playerList[playerToHit].transform.position = gameController.GetRandomPosition();
                GameStateHandler.playerList[playerToHit].GetComponent<Movement>().points = 0;
                foreach (GameObject part in GameStateHandler.playerList[playerToHit].GetComponent<Movement>().body)
                    Destroy(part.gameObject);
                GameStateHandler.playerList[playerToHit].GetComponent<Movement>().body = new List<GameObject>();
                Destroy(this.gameObject);
                return;
            }else { //if hit player's own head then return
                // Debug.Log("in DestroyHead: playertohit == myPlayerNum");
                return;
            } 
    }

    private void DestroyTail(string tailWithPlayerNumber)
    {   //note: here tail will be 'tailP<playernumber><bodyIndex>
        //ie tailP111 is player1's 11th item in the body
        // Debug.Log("*******in destroy tail");
        int playerToHit = ConvertToInt(tailWithPlayerNumber.Substring(5,1)); //val after `tailP`
        // Debug.Log("in tail; player to hit: "+ tailWithPlayerNumber.Substring(5,1));
        // Debug.Log("my number:" + myPlayerNum);
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
