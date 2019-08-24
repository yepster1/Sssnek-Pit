using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private int playerNumber;
    private float speed = Config.PLAYER_SPEED;
    private float rotationSpeed = Config.PLAYER_ROTATION;
    public GameObject tailPrefab;
    public List<Transform> body;
    
    public KeyCode left;
    public KeyCode right;

    public KeyCode powerup;
    private Rigidbody rb;
    public float force = 500.0f;

    
    public int points = 0;
    public GameObject auraPrefab;
    private Transform auraTransform;

    public Vector3 lastLocation;

    private void OnCollisionEnter(Collision collision)
    {
        CollideWithPoint(collision);
        CollideWithOtherSnake(collision);
        if (collision.gameObject.tag.Equals("powerup"))
        {

        }
    }

    private void CollideWithOtherSnake(Collision collision)
    {
        if (collision.gameObject.tag.Equals("snake"))
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
    }

    private void CollideWithPoint(Collision collision)
    {
        if (collision.gameObject.tag.Equals("point"))
        {
            Destroy(collision.gameObject);
            points += 1;
            add_tail();
            increase_aura();
        }
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
        Camera cam = GetComponentInChildren<Camera>();
        view view = Config.playerViews[amountOfPlayers-1][playerNumer];
        cam.rect = new Rect(view.x, view.y, view.w, view.h);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        body = new List<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveForward();
        performTurn();

        if (Input.GetKey(left) && Input.GetKey(right))
        {
            activatePowerup();

        }
        moveTail();

        moveAura();
    }

    private void moveForward()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
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
        // jump powerup
        if (Input.GetKey(left) && Input.GetKey(right))
        {
            Debug.Log("pressed both buttons");
            // rb.AddForce(transform.up * force );
            transform.Translate(transform.up * speed * Time.smoothDeltaTime, Space.World);
        }
        //end of jump powerup
        
        float step = speed * Time.deltaTime;
        if (body.Count > 0)
        {   
            var MaximumDistance = 1.2;
            var MinimumDistance = 1.0;
            var MaxSpeed = speed * 1.3;
            var MinSpeed = speed * 0.7;
            var bodySpeed = 0.0;

            var dist=Vector3.Distance(body[0].position, transform.position);
 
            if ( dist > MaximumDistance) 
            { 
                bodySpeed = MaxSpeed; //to far so max speed
            } 
            else if ( dist < MinimumDistance) 
            { 
                bodySpeed = MinSpeed; //to close so min speed
            } 
            else 
            { 
                // bodyPart is between Max/Min distance so give it a proportional speed
                // between Min and Max speed
                // This is the % ratio between Max and Min distance
                var distRatio=(dist - MinimumDistance)/(MaximumDistance - MinimumDistance);
                // This is the extra speed above min speed he can go up too
                var diffSpeed = MaxSpeed - MinSpeed;
                bodySpeed = ( distRatio * diffSpeed) + MinSpeed; // Final calc 
            }
            body[0].LookAt(transform);
            body[0].Translate(body[0].forward * (float)bodySpeed * Time.smoothDeltaTime, Space.World);
        }
        for (int i = 1; i < body.Count; i++)

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

    public void activatePowerup()
	{
		transform.Translate(transform.up * speed * Time.smoothDeltaTime, Space.World);
	}

    private void increase_aura(){
        if (points > 0 && auraTransform != null){
            
            float size = points * 10 / 1500;
            Debug.Log(size);
            auraTransform.localScale += new Vector3(size, size, size);
        }
        
    }
}
