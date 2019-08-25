using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRightAngle : MonoBehaviour
{
    private int playerNumber;
    public float speed = Config.PLAYER_SPEED;
    private float rotationSpeed = Config.PLAYER_ROTATION;
    public GameObject tailPrefab;
    public List<Transform> body;
    public List<Powerup> powerups;
    public KeyCode left;
    public KeyCode right;
    public float timeToTurn; //need this otherwise turning too fast
    public float maxTimeToTurn = 0.2f; //edit this to define how long between can turn
    
    private Powerup powerup;
    private float timeBetweenJumps = 2f;
    private float jumpTimer;
    private Rigidbody rb;
    private bool canJump;

    public float points = 0;
    public GameObject auraPrefab;
    private Transform auraTransform;

    private Camera cam;
    public GameObject cameraPrefab;
    private float rotation = 45;
    private float yOffset = 30f;
    private float zOffset = -30f;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("snake"))
        {
            CollideWithOtherSnake(collision);
        }
        if (collision.gameObject.tag.Equals("point"))
        {
            CollideWithPoint(collision);
        }
        if (collision.gameObject.tag.Equals("powerup"))
        {
            CollideWithPowerup(collision);
        }
    }

    private void CollideWithOtherSnake(Collision collision)
    {
        if (body.Contains(collision.transform))
        {
            return;
        }
        Debug.Log("I have died");
        transform.position = gameController.GetRandomPosition();
        points = 0;
        foreach (Transform part in body)
            Destroy(part.gameObject);
        body = new List<Transform>();
        
    }

    private void CollideWithPoint(Collision collision)
    {
        Destroy(collision.gameObject);
        points += 1;
        add_tail();
        increase_aura();
        
    }

    private void CollideWithPowerup(Collision collision)
    {
        GameObject powerup = collision.gameObject;
        Powerup powerupScript = powerup.GetComponent<Powerup>();
        Debug.Log("powerup type: " + powerupScript.powerupType);
        Debug.Log("powerup is active: " + powerupScript.isActive);
        powerups.Add(powerupScript);
        
        Destroy(collision.gameObject);
        points += 1;
        
        
    }

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
        powerup = this.gameObject.GetComponent<Powerup>();
        // Powerup defaultScript = default.GetComponent<Powerup>();
        jumpTimer = 0.0f;
        powerup.powerupType = "jump";
        powerup.isActive = true;
        powerup.activateNow = false;
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

        
            
        moveTail();
        moveAura();
        moveCamera();
    }

    private void moveForward()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
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
                powerups[0].GetComponent<Powerup>().activateNow = true;
                Debug.Log(powerups[0].powerupType);
                jumpTimer = 0.0f;
                canJump = false;
            }
            
        }else{
            Debug.Log("no powerups in list");
        }
		
	}

    private void moveTail()
    {
        if (body.Count > 0)
        {
            moveTail(0, transform);
        }
        for (int i = 1; i < body.Count; i++)
        {
            moveTail(i, body[i - 1]);
        }
    }

    public void moveTail(int i, Transform transform)
	{
		var MaximumDistance = 1.3;
		var MinimumDistance = 1.0;
		var MaxSpeed = speed * 1.5;
		var MinSpeed = speed * 0.8;
		var bodySpeed = 0.0;
		var dist = Vector3.Distance(body[i].position, transform.position);

		if (dist > MaximumDistance)
		{
			bodySpeed = MaxSpeed; //to far so max speed
		}
		else if (dist < MinimumDistance)
		{
			bodySpeed = MinSpeed; //to close so min speed
		}
		else
		{
			// bodyPart is between Max/Min distance so give it a proportional speed
			// between Min and Max speed
			// This is the % ratio between Max and Min distance
			var distRatio = (dist - MinimumDistance) / (MaximumDistance - MinimumDistance);
			// This is the extra speed above min speed he can go up too
			var diffSpeed = MaxSpeed - MinSpeed;
			bodySpeed = (distRatio * diffSpeed) + MinSpeed; // Final calc 
		}
		body[i].LookAt(transform);
		body[i].Translate(body[i].forward * (float)bodySpeed * Time.smoothDeltaTime, Space.World);
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

    private void moveCamera(){
        Vector3 temp = this.transform.position;
		temp.z += zOffset;
        temp.y = yOffset;
        cam.transform.position = temp;
        cam.transform.rotation = Quaternion.Euler(rotation, 0, 0);
    }

    private void add_tail()
    {
        Transform newPart;
        
        if (body.Count != 0)
        {   
            
            newPart = Instantiate(tailPrefab as GameObject, body[body.Count - 1].position - body[body.Count - 1].forward, body[body.Count - 1].rotation).transform;
        } else
        {   
            auraTransform = Instantiate(auraPrefab as GameObject, transform.position - transform.forward, transform.rotation).transform;
            newPart = Instantiate(tailPrefab as GameObject, transform.position - transform.forward, transform.rotation).transform;
        }
        body.Add(newPart);

    }

    private void increase_aura(){
        if (points > 0 && auraTransform != null){

            float size = points * 10 / 1500; //change this to modify size faster or slower
            Debug.Log(size);
            auraTransform.localScale += new Vector3(size, size, size);
        }
        
    }
}
