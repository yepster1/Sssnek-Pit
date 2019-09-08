using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : BaseMovement
{
    private int playerNumber;
    public KeyCode left;
    public KeyCode right;
    
    public void spawnPlayer(List<int> inputs)
    {
        int amountOfPlayers = inputs[0];
        this.playerNumber = inputs[1];
        Debug.Log("player " + playerNumber + " started");
        this.left = Config.playerControls[playerNumber].Left;
        this.right = Config.playerControls[playerNumber].rigth;
        snakeColourSetter.SetColor(playerNumber, GetComponent<SkinnedMeshRenderer>());
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
        powerups = new List<Powerup>();
        Powerup jumpDefault = (Powerup)this.gameObject.GetComponent<Jump>();
        jumpDefault.powerupType = "jump";
        jumpDefault.isActive = true;
        jumpDefault.deactivateNow();
        powerups.Add(jumpDefault);
        
        // Powerup defaultScript = default.GetComponent<Powerup>();
        jumpTimer = 0.0f;
        
        // powerups.Add(powerup);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       jumpTimer+= Time.smoothDeltaTime;
        // powerup = GameObject.FindGameObjectWithTag("powerup").GetComponent<Powerup>();
        moveForward();
        performTurn();

        activatePowerup();
        
        moveMyTail();
        moveAura();
        
    }

    private void moveForward()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    private void speedPowerUp()
    {
        //speed powerup
        // timeLeft -= Time.deltaTime;
        // if (Input.GetKey("v"))    
        // {
        //     if (timeLeft > 0)
        //     {
        //         speed = 50f;
        //         Debug.Log("Poweup ON. player speed:" + speed +". Time left:" + timeLeft);
        //     }
        //     else
        //     {
        //         speed = 20f;
        //         Debug.Log("Poweup OFF. player speed:" + speed + ". Time left:" + timeLeft);
        //     }
        // }
        //end of speed powerup 
    }
    private void moveAura()
    {
        if (points > 0)
        {

            // auraTransform.position = transform.position;
            float auraPos;
            if (points < 90)
            {
                auraPos = transform.position.y + (points * 10 / 100.0f);
            }
            else
            { //want to cap y value
                auraPos = transform.position.y + 9.0f;
            }

            auraTransform.position = new Vector3(transform.position.x, auraPos, transform.position.z);
            auraTransform.rotation = transform.rotation;
        }
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
        if(powerups!= null){
            if (Input.GetKey(left) && Input.GetKey(right) && jumpTimer > timeBetweenJumps)
            {
                Debug.Log(powerups[0].powerupType);
                powerups[0].activateNow();
                jumpTimer = 0.0f;

            }
        }else{
            Debug.Log("no powerups in list");
        }
	}

    public override void setColor(Transform tail)
    {
        snakeColourSetter.SetColor(playerNumber, tail.gameObject.GetComponent<SkinnedMeshRenderer>());
    }

}
