using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Movement : BaseMovement
{
    private int playerNumber;
    public KeyCode left;
    public KeyCode right;
    protected GameObject scoreDisplayObject;
    protected TMP_Text scoreDisplay;

    public void spawnPlayer(List<int> inputs)
    {
        int amountOfPlayers = inputs[0];
        this.playerNumber = inputs[1];
        Debug.Log("player " + playerNumber + " started");
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
        body = new List<Transform>();
        rb = GetComponent<Rigidbody>();
        powerups = new Stack<Powerup>();
        Powerup jumpDefault = this.gameObject.AddComponent<Jump>();
        jumpDefault.setPowerup("jump", true, false);
        powerups.Push(jumpDefault);
        
        MaxSpeed = Config.MAX_PLAYER_SPEED;
        MinSpeed = Config.MIN_PLAYER_SPEED;
        jumpTimer = 0.0f;
        powerupBeingUsed = false;

        alive=true;
        //Set the right Score Display
        if(playerNumber==0){
            scoreDisplayObject =GameObject.FindGameObjectWithTag("player1");
            scoreDisplay=scoreDisplayObject.GetComponentInChildren<TMP_Text>();
        }

        else if(playerNumber==1){
            scoreDisplayObject =GameObject.FindGameObjectWithTag("player2");
            scoreDisplay=scoreDisplayObject.GetComponentInChildren<TMP_Text>();
        }

        else if(playerNumber==2){
            scoreDisplayObject =GameObject.FindGameObjectWithTag("player3");
            scoreDisplay=scoreDisplayObject.GetComponentInChildren<TMP_Text>();
        }
        else if(playerNumber==3){
            scoreDisplayObject =GameObject.FindGameObjectWithTag("player4");
            scoreDisplay=scoreDisplayObject.GetComponentInChildren<TMP_Text>();
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       jumpTimer+= Time.smoothDeltaTime;
        // powerup = GameObject.FindGameObjectWithTag("powerup").GetComponent<Powerup>();
        moveForward();
        performTurn();

        if (Input.GetKey(left) && Input.GetKey(right))
        {   
            Debug.Log("powerups.Count()" + powerups.Count);
            activatePowerup();
        }
        if(alive){
        scoreDisplay.text="Player "+(playerNumber+1)+":  "+ points+"";
        Debug.Log(scoreDisplay.tag+ " "+playerNumber+" "+ points);
        }
        else{
             scoreDisplay.text="Player "+(playerNumber+1)+":  Died.";
             alive=true;
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

    public void activatePowerup()
	{
        bool otherPowerupActive = false;
        if (powerups.Count  > 1 && !powerupBeingUsed){
            Powerup p = powerups.Pop();
            if (p.powerupType == "speed" ){
                powerupInUse(); //sets powerupBeingUsed to true
                Debug.Log(p.activate);
                p.activateNow();
                Invoke("powerupInUse", p.maxTimeToSpeed); //sets powerupBeingUsed to false to allow others to be used
            }
            
        }else if (powerups.Count == 1){
            Powerup p = powerups.Peek();
            Debug.Log(p.powerupType);
            if (p.powerupType == "jump" && jumpTimer > timeBetweenJumps && !powerupBeingUsed){
                powerupInUse(); //sets powerupBeingUsed to true
                p.activateNow();
                Invoke("powerupInUse", timeBetweenJumps);
                jumpTimer = 0.0f;
            }
        } 
       
        else if (powerups.Count == 0){
            Debug.Log("no powerup to activate pushing now");
            Powerup jumpDefault = this.gameObject.AddComponent<Jump>();
            jumpDefault.setPowerup("jump", true, false);
            powerups.Push(jumpDefault);
            
        }
	}
    private void powerupInUse(){ //sets it to the opposite
        
        powerupBeingUsed = ! powerupBeingUsed;
    }
    

   

}
