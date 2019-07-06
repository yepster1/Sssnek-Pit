using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
	public float speed = 100f;
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
            Debug.Log("collided");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        body = new List<Transform>();
        body.Add(transform);
    }

    // Update is called once per frame
    void Update()
    {
        body[0].Translate(transform.forward * speed * Time.smoothDeltaTime, Space.World);
        if (Input.GetKey(left)){
            body[0].Rotate(Vector3.up * rotationSpeed * -1);
        }
        if (Input.GetKey(right))
        {
            body[0].Rotate(Vector3.up * rotationSpeed * 1);
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
        Transform newPart = Instantiate(tailPrefab as GameObject, body[body.Count - 1].position - body[body.Count - 1].forward, body[body.Count - 1].rotation).transform;
        body.Add(newPart);
    }
}
