using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public int playerNumber;
    public float speed = Config.PLAYER_SPEED;
    public float rotationSpeed = 100f;
    public GameObject tailPrefab;
    public List<Transform> body;
    
    public KeyCode left;
    public KeyCode right;
    public KeyCode powerup;
    public int points = 0;

    public float timeLeft = 100f; //for speed powerup

    public Vector3 lastLocation;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("point"))
        {
            Destroy(collision.gameObject);
            points += 1;
            add_tail();
        }
        if (collision.gameObject.tag.Equals("snake"))
        {
            if (body.Contains(collision.transform))
            {
                return;
            }
            Debug.Log("I have died");
            transform.position = gameController.get_random_position();
            points = 0;
            foreach(Transform part in body)
            {
                Destroy(part.gameObject);
            }
            body = new List<Transform>();
        }
        if(collision.gameObject.tag.Equals("powerup"))
        {

        }
    }


    public void start(List<int> inputs)
    {
        int amountOfPlayers = inputs[0];
        playerNumber = inputs[1];
        Debug.Log("player " + playerNumber + " started");
        this.left = Config.playerControls[playerNumber].Left;
        this.right = Config.playerControls[playerNumber].rigth;
        this.powerup = Config.playerControls[playerNumber].powerup;
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);    
        
        if (Input.GetKey(left)){
            transform.Rotate(Vector3.up * rotationSpeed * -1);
        }
        if (Input.GetKey(right))
        {
            transform.Rotate(Vector3.up * rotationSpeed * 1);
        }
        //speed powerup
        timeLeft -= Time.deltaTime;
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
        if (Input.GetKey(powerup))
        {
            transform.Translate(transform.up * speed * Time.smoothDeltaTime, Space.World);
        }
        //end of jump powerup
        
        float step = speed * Time.deltaTime;
        if (body.Count > 0)
        {   
            var MaximumDistance = 1.3;
            var MinimumDistance = 1.0;
            var MaxSpeed = speed * 1.5;
            var MinSpeed = speed * 0.8;
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
        {
            var MaximumDistance = 1.3;
            var MinimumDistance = 1.0;
            var MaxSpeed = speed * 1.5;
            var MinSpeed = speed * 0.8;
            var bodySpeed = 0.0;

            var dist=Vector3.Distance(body[i].position, body[i-1].position);

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
            body[i].LookAt(body[i-1]);
            body[i].Translate(body[i].forward * (float)bodySpeed * Time.smoothDeltaTime, Space.World);
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
            newPart = Instantiate(tailPrefab as GameObject, transform.position - transform.forward, transform.rotation).transform;
        }
        body.Add(newPart);
    }
}
