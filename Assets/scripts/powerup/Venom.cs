using System.Collections;
using System; //for int parsing
using System.Collections.Generic;
using UnityEngine;

public class Venom: MonoBehaviour
{
    // [SerializeField]
    
    public Rigidbody venomObjectRB;
    public float speed = 0.001f;
    public bool activate;
    private string myName;
    public int myPlayerNum;
    //for venom
    public List<GameObject> otherPlayers;

    private VenomShootingScript vss;
    private float maxTimeToShoot;
    private float shootTimer;
    
    List <Collider> currentCollisions = new List <Collider> ();

    private static int count =0;

    // Update is called once per frame
    public void InitVenom(int _myPlayerNum, bool _activate, List <GameObject> _otherPlayers)
    {
        // myName = _myName;
        myPlayerNum = _myPlayerNum;
        otherPlayers = _otherPlayers;
        
        for (int i = 0 ; i < _otherPlayers.Count;i++){
            otherPlayers[i]= _otherPlayers[i];
        }
        
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
 
         // Print the entire list to the console.
         foreach (Collider gObject in currentCollisions) {
            
            if (gObject.gameObject.tag == "snake" && gObject.gameObject != null)
            {   
                // Debug.Log("collided with " +collision.gameObject.tag);
                // Movement movement = collision.GetComponent<Movement>();
                // Debug.Log("snake number: "  + movement.playerNumber);
                CollideWithOtherObject(gObject);
            }
         }
        
        
        
    }

    void OnTriggerExit (Collider collision) {
 
         // Remove the GameObject collided with from the list.
         currentCollisions.Remove (collision);
 
         // Print the entire list to the console.
         foreach (Collider gObject in currentCollisions) {
             if (gObject!= null){}
            //  print (gObject.gameObject.name);
         }
    }

    protected void CollideWithOtherObject(Collider collision){
        Tail1 tail = collision.gameObject.GetComponent<Tail1>();
        if (tail != null){
            if (collision.gameObject.name.Substring(0,4) == "tail"){

                int colPlayerNum = ConvertToInt(collision.gameObject.name.Substring(5,1));
                
                for (int i = 0 ; i < otherPlayers.Count; i++){
                    if ( otherPlayers[i].GetComponent<PowerupManager>().myPlayerNum == colPlayerNum){
                        Debug.Log("hit tail");
                        Debug.Log("player number: " +collision.gameObject.name.Substring(5,1));
                        Debug.Log("my number: " +myPlayerNum);
                        
                        DestroyTail(collision.gameObject.name);
                    }
                }
                
            }
        }
        else{
            if (collision.gameObject.name.Substring(0,6) == "player"){
                
                int colPlayerNum = ConvertToInt(collision.gameObject.name.Substring(6));
                    for (int i = 0 ; i < otherPlayers.Count; i++){
                        if (otherPlayers[i].GetComponent<PowerupManager>().myPlayerNum == colPlayerNum){

                            Debug.Log("hit head");
                            Debug.Log( "player number: " + colPlayerNum);
                            Debug.Log("my number: " + myPlayerNum);
                            DestroyHead(collision.gameObject.GetComponent<Movement>().head.name);
                        
                        }else {
                            Debug.Log("myPlayerNum == colPlayerNum");
                            return;
                        }
                    }
                    
                
            }
        }
    }

    
    private void DestroyHead(string headWithPlayerNumber){
        
        int playerToHit = ConvertToInt(headWithPlayerNumber.Substring(6));
        
        //for respawn
        GameStateHandler.playerList[playerToHit].transform.position = gameController.GetRandomPosition();
        GameStateHandler.playerList[playerToHit].GetComponent<Movement>().points = 0;
        foreach (GameObject part in GameStateHandler.playerList[playerToHit].GetComponent<Movement>().body)
            Destroy(part.gameObject);
        GameStateHandler.playerList[playerToHit].GetComponent<Movement>().body = new List<GameObject>();
        return;
        
    }

    private void DestroyTail(string tailWithPlayerNumber){
        int playerToHit = ConvertToInt(tailWithPlayerNumber.Substring(5,1));
        int tailNum = ConvertToInt(tailWithPlayerNumber.Substring(6)); //get index of where snake was hit
        
        for (int j = GameStateHandler.playerList[playerToHit].GetComponent<Movement>().body.Count-1 ; j > tailNum ; j--  ){
            Destroy(GameStateHandler.playerList[playerToHit].GetComponent<Movement>().body[j]);
        }
    }

    private int ConvertToInt(string itemToCheck){
        int partForPlayerNum;
                
        // Debug.Log("tailNum: " + tailNum);
        bool success = Int32.TryParse(itemToCheck, out partForPlayerNum); 
        if (success)
        {
            // Debug.Log("Converted: "+ itemToCheck+ " to int: " + partForPlayerNum); 
            return partForPlayerNum;        
        }
        else
        {
            Console.WriteLine("Conversion failed");
            return -1;
        }
    }
}
