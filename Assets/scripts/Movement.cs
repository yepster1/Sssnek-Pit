using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : BaseMovement
{
    public int playerNumber;
    public KeyCode left;
    public KeyCode right;
    public PowerupManager powerupManager;
    public void spawnPlayer(List<int> inputs)
    {
        int amountOfPlayers = inputs[0];
        this.playerNumber = inputs[1];
        head = this.gameObject;
        // Debug.Log("**** "+ playerNumber+ " ****");
        
        if (playerNumber == 0){ //name for head (different to tag)
            gameObject.name = "player0";
        }
        if (playerNumber == 1){
            gameObject.name = "player1";
        }
        if (playerNumber == 2){
            gameObject.name = "player2";
        }
        if (playerNumber == 3){
            gameObject.name = "player3";
        }
        Debug.Log("player " + playerNumber + " started");
        // Debug.Log("player left: "+Config.playerControls[playerNumber].Left );
        this.left = Config.playerControls[playerNumber].Left;
        this.right = Config.playerControls[playerNumber].rigth;
        setCamara(amountOfPlayers, playerNumber);
    }

    private void setCamara(int amountOfPlayers, int playerNumer)
    {
        Debug.Log("setting view with amount of players " + amountOfPlayers + " and player number " + playerNumber);
        Camera cam = GetComponentInChildren<Camera>();
        view view = Config.playerViews[amountOfPlayers-1][playerNumer];
        cam.rect = new Rect(view.x, view.y, view.w, view.h);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        body = new List<GameObject>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        // if (powerupManager == null){
        powerupManager = this.gameObject.GetComponent<PowerupManager>();
        powerupManager.SetPowerupManager();
        // }
        
        //original
        MaxSpeed = Config.MAX_PLAYER_SPEED;
        MinSpeed = Config.MIN_PLAYER_SPEED;
        // Powerup defaultScript = default.GetComponent<Powerup>();
        
        // // for powerup demonstration
        // MaxSpeed = 0.0f;
        // MinSpeed = 0.0f;
        // speed = 0.0f;
        // //for powerup demonstration
        // if (playerNumber == 0){
        //     gameObject.transform.position = new Vector3(-10.0f, 1.0f,-10.0f);
        //     gameObject.transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
        //     for (int i = 0 ;i < 20 ;i++ ){
        //         add_tail();
        //     }
        // }
        // else if (playerNumber == 1){
        //     gameObject.transform.position = new Vector3(10.0f, 1.0f,10.0f);
        //     gameObject.transform.rotation = Quaternion.Euler(0.0f,180.0f,0.0f);
        //     for (int i = 0 ;i < 20 ;i++ ){
        //         add_tail();
        //     }
        // }
        // else if (playerNumber == 2){
        //     gameObject.transform.position = new Vector3(0.0f, 1.0f,10.0f);
        //     gameObject.transform.rotation = Quaternion.Euler(0.0f,90.0f,0.0f);
        //     for (int i = 0 ;i < 20 ;i++ ){
        //         add_tail();
        //     }
        // }
        
        // else if (playerNumber == 3){
        //     gameObject.transform.position = new Vector3(0.0f, 1.0f,-10.0f);
        //     gameObject.transform.rotation = Quaternion.Euler(0.0f,-90.0f,0.0f);
        //     for (int i = 0 ;i < 20 ;i++ ){
        //         add_tail();
        //     }
        // }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    //    jumpTimer+= Time.smoothDeltaTime;
        // powerup = GameObject.FindGameObjectWithTag("powerup").GetComponent<Powerup>();
        moveForward();
        performTurn();

        if (Input.GetKey(left) && Input.GetKey(right))
        {   
            
            powerupManager.activatePowerup();
        }
        
        moveMyTail(MaxSpeed,MinSpeed);
        moveAura();
        
    }

    private void performTurn()
	{
		if (Input.GetKey(left))
		{
			transform.Rotate(Vector3.up * rotationSpeed * -1);
		}
		if (Input.GetKey(right))
		{
			transform.Rotate(Vector3.up * rotationSpeed * 1);
		}
	}

    
    
    

   

}
