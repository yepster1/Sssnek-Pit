using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;
    public GameObject tailPrefab;
    public List<Transform> body;
    public int points;

    public abstract void CollectPoint();

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("point"))
        {
            Destroy(collision.gameObject);
            CollectPoint();
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
            foreach (Transform part in body)
            {
                Destroy(part.gameObject);
            }
            body = new List<Transform>();
        }
        if (collision.gameObject.tag.Equals("powerup"))
        {

        }
    }

    public void add_tail()
    {
        Transform newPart;
        if (body.Count != 0)
        {
            newPart = Instantiate(tailPrefab as GameObject, body[body.Count - 1].position - body[body.Count - 1].forward, body[body.Count - 1].rotation).transform;
        }
         else
        {
            newPart = Instantiate(tailPrefab as GameObject, transform.position - transform.forward, transform.rotation).transform;
        }
        body.Add(newPart);
    }

    public float DistanceTo(Vector3 first, Vector3 second)
    {
        return Vector3.Distance(first, second);
    }
}
