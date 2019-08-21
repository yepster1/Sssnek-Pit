using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    public int playerNumber;
    public KeyCode left;
    public KeyCode right;

    public override void CollectPoint()
    {
        points += 1;
        add_tail();
    }

    public void Start(List<int> inputs)
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
        view view = Config.playerViews[amountOfPlayers- 1][playerNumer];
        cam.rect = new Rect(view.x, view.y, view.w, view.h);

    }
    // Start is called before the first frame update
    void Start()
    {
        body = new List<Transform>();
		speed = Config.PLAYER_SPEED;
		rotationSpeed = Config.PLAYER_ROTATION;
	}

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.smoothDeltaTime, Space.World);
        if (Input.GetKey(left))
        {
            transform.Rotate(Vector3.up * rotationSpeed * -1);
        }
        if (Input.GetKey(right))
        {
            transform.Rotate(Vector3.up * rotationSpeed * 1);
        }
        if (body.Count > 0)
        {
            body[0].LookAt(transform);
            body[0].Translate(body[0].forward * speed * Time.smoothDeltaTime * 0.8f * DistanceTo(transform.position, body[0].position), Space.World);
        }
        for (int i = 1; i < body.Count; i++)
        {
            //current follows previous;
            Transform previous = body[i - 1];
            Transform current = body[i];
            current.LookAt(previous);
            current.Translate(current.forward * speed * Time.smoothDeltaTime * 0.8f * DistanceTo(previous.position, current.position), Space.World);
        }
        //if(Input.GetButtonDown)
    }

}
