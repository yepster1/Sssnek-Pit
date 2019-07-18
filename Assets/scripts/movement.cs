﻿using System.Collections;
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
    public int points = 0;
    
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
        Camera cam = GetComponentInChildren<Camera>();
        view view = Config.playerViews[amountOfPlayers-1][playerNumer];
        Debug.Log(" with view " + view);
        cam.rect = new Rect(view.x, view.y, view.w, view.h);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        body = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.smoothDeltaTime, Space.World);
        if (Input.GetKey(left)){
            transform.Rotate(Vector3.up * rotationSpeed * -1);
        }
        if (Input.GetKey(right))
        {
            transform.Rotate(Vector3.up * rotationSpeed * 1);
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
                body[0].Translate(body[0].forward * speed * Time.smoothDeltaTime * 0.8f, Space.World);
            }
        }
        for (int i = 1; i < body.Count; i++)
        {
            //current follows previous;
            Transform previous = body[i - 1];
            Transform current = body[i];
            if (Vector3.Distance(previous.position, current.position) > 1.3f) {
                current.LookAt(previous);
                current.Translate(current.forward * speed * Time.smoothDeltaTime, Space.World);
            }
            if (Vector3.Distance(previous.position, current.position) < 1.3f && Vector3.Distance(previous.position, current.position) > 1)
            {
                current.LookAt(previous);
                current.Translate(current.forward * speed * Time.smoothDeltaTime * 0.8f, Space.World);
            }
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
            newPart = Instantiate(tailPrefab as GameObject, transform.position - transform.forward, transform.rotation).transform;
        }
        body.Add(newPart);
    }
}
