using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class BaseMovement : MonoBehaviour
{

    public GameObject tailPrefab;
    public List<Transform> body;
    public int points = 0;

    protected float speed = Config.PLAYER_SPEED;
    public GameObject auraPrefab;
    protected Transform auraTransform;

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

    protected void CollideWithPoint(Collision collision)
    {
        if (collision.gameObject.tag.Equals("point"))
        {
            Destroy(collision.gameObject);
            GameStateHandeler.pointList.Remove(collision.gameObject);
            points += 1;
            add_tail();
            increase_aura();
        }
    }

    private void increase_aura()
    {
        if (points > 0 && auraTransform != null)
        {

            float size = points * 10 / 1500; //change this to modify size faster or slower
            Debug.Log(size);
            auraTransform.localScale += new Vector3(size, size, size);
        }

    }

    protected void add_tail()
    {
        Transform newPart;
        if (body.Count != 0)
        {
            newPart = Instantiate(tailPrefab as GameObject, body[body.Count - 1].position - body[body.Count - 1].forward, body[body.Count - 1].rotation).transform;
        }
        else
        {
            auraTransform = Instantiate(auraPrefab as GameObject, transform.position - transform.forward, transform.rotation).transform;
            newPart = Instantiate(tailPrefab as GameObject, transform.position - transform.forward, transform.rotation).transform;
        }
        body.Add(newPart);
    }

    protected void moveMyTail()
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
}
