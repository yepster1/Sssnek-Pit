using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRightAngle : BaseMovement
{
    private int playerNumber;
    public KeyCode left;
    public KeyCode right;

    public float timeToTurn; //need this otherwise turning too fast
    public float maxTimeToTurn = 0.2f; //edit this to define how long between can turn
    private bool canJump;
    
    private Camera cam;
    public GameObject cameraPrefab;
    private float rotation = 45;
    private float yOffset = 30f;
    private float zOffset = -30f;
    
    

    public void spawnPlayer(List<int> inputs)
    {
        int amountOfPlayers = inputs[0];
        playerNumber = inputs[1];
        Debug.Log("player " + playerNumber + " started");
        this.left = Config.playerControls[playerNumber].Left;
        this.right = Config.playerControls[playerNumber].rigth;
        setCamara(amountOfPlayers, playerNumber);
    }

    private void setCamara(int amountOfPlayers, int playerNumer)
    {
        Debug.Log("setting view with amount of players " + amountOfPlayers + " and player number " + playerNumber);
        
        //set up cam rotation and position
        Quaternion camRotation = Quaternion.Euler(rotation, 0, 0);
        Vector3 temp = this.transform.position;
        cam = Instantiate(cameraPrefab as GameObject, temp, camRotation).GetComponent<Camera>();

        view view = Config.playerViews[amountOfPlayers-1][playerNumer];
        cam.rect = new Rect(view.x, view.y, view.w, view.h);

        //modify camera position with offset
        temp.z += zOffset;
        temp.y = yOffset;
        cam.transform.position = temp;

    }

    // Start is called before the first frame update
    void Start()
    {
        body = new List<Transform>();
        timeToTurn = 0.0f;
        rb = GetComponent<Rigidbody>();

        powerups = new List<Powerup>();
        Powerup jumpDefault = (Powerup)this.gameObject.GetComponent<Jump>();
        jumpDefault.powerupType = "jump";
        jumpDefault.isActive = true;
        jumpDefault.deactivateNow();
        powerups.Add(jumpDefault);
        canJump = false;
        powerups.Add(powerup);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (jumpTimer > timeBetweenJumps){
            canJump = true;
        }
        // powerup = GameObject.FindGameObjectWithTag("powerup").GetComponent<Powerup>();
        moveForward();

        activatePowerup();
        jumpTimer+= Time.smoothDeltaTime;
        
        performTurn();
        timeToTurn += Time.deltaTime;

        moveMyTail();
        moveAura();
        moveCamera();
    }

    private void performTurn()
	{
		if (Input.GetKeyDown(left) && (timeToTurn > maxTimeToTurn) ){
            canJump = false;
            transform.Rotate(Vector3.up* -90);
            timeToTurn = 0.0f;

        }
        if (Input.GetKeyDown(right) && (timeToTurn > maxTimeToTurn) ){
            canJump = false;
            transform.Rotate(Vector3.up * 90);
            timeToTurn = 0.0f;
        }
        
	}

    public void activatePowerup()
	{
        if(powerups!= null){
            if (Input.GetKey(left) && Input.GetKey(right) && jumpTimer > timeBetweenJumps  && canJump)
            {
                Debug.Log(powerups[0].powerupType);
                powerups[0].activateNow();
                jumpTimer = 0.0f;
                canJump = false;
            }
            
        }else{
            Debug.Log("no powerups in list");
        }
		
	}

    private void moveCamera(){
        Vector3 temp = this.transform.position;
		temp.z += zOffset;
        temp.y = yOffset;
        cam.transform.position = temp;
        cam.transform.rotation = Quaternion.Euler(rotation, 0, 0);
    }
}
