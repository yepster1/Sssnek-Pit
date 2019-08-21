using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementRightAngle : MonoBehaviour
{
    public int playerNumber;
    public float speed = Config.PLAYER_SPEED;
    public float rotationSpeed = 100f;
    public GameObject tailPrefab;
    public List<Transform> body;
    
    public GameObject auraPrefab;
    private Transform auraTransform;
    public KeyCode left;
    public KeyCode right;
    
    public float points = 0;

    private Camera cam;
    public GameObject cameraPrefab;
    private float rotation = 45;
    private float yOffset = 30f;
    private float zOffset = -30f;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("point"))
        {
            Destroy(collision.gameObject);
            points += 1;
            add_tail();
            increase_aura();

        }
        if (collision.gameObject.tag.Equals("snake"))
        {
            if (body.Contains(collision.transform))
            {
                return;
            }
            Debug.Log("I have died");
            transform.position = gameController.GetRandomPosition();
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
    }

    // Update is called once per frame
    void Update()
    {
        //modify camera position with offset
        Vector3 temp = this.transform.position;
		temp.z += zOffset;
        temp.y = yOffset;
        cam.transform.position = temp;
        cam.transform.rotation = Quaternion.Euler(rotation, 0, 0);
        
		
        transform.Translate(transform.forward * speed * Time.smoothDeltaTime, Space.World);
        if (Input.GetKeyDown(left) ){
            
            transform.Rotate(Vector3.up* -90);
        }
        if (Input.GetKeyDown(right) )
        {
            
            transform.Rotate(Vector3.up * 90);
        }
        if(body.Count > 0)
        {
            if (Vector3.Distance(transform.position, body[0].position) > 1.3f)
            {
                body[0].LookAt(transform);
                body[0].Translate(body[0].forward * speed * Time.smoothDeltaTime, Space.World);
            }
            if (Vector3.Distance(transform.position, body[0].position) < 1.3f && Vector3.Distance(transform.position, body[0].position) > 1)
            {
                body[0].LookAt(transform);
                body[0].Translate(body[0].forward * speed * Time.smoothDeltaTime , Space.World);
            }
        }
        for (int i = 1; i < body.Count; i++)
        {
            //current follows previous;
            Transform previous = body[i - 1];
            Transform current = body[i];
            if (Vector3.Distance(previous.position, current.position) > 1.3f) {
                current.LookAt(previous);
                current.Translate(current.forward * speed * Time.smoothDeltaTime , Space.World);
            }
            if (Vector3.Distance(previous.position, current.position) < 1.3f && Vector3.Distance(previous.position, current.position) > 1)
            {
                current.LookAt(previous);
                current.Translate(current.forward * speed * Time.smoothDeltaTime , Space.World);
            }
        }
        if (points > 0){
            
            auraTransform.position = transform.position;
            auraTransform.rotation = transform.rotation;
        }
        
       
        //if(Input.GetButtonDown)
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

            float size = points * 10 / 7500; //change this to modify size faster or slower
            Debug.Log(size);
            auraTransform.localScale += new Vector3(size, size, size);
        }
        
    }
}
