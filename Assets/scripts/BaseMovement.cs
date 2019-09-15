using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
// public  struct score
// {
//     public static score setScore(int totScore){
//         score totalScore = new score();
//         totalScore.currentScore=totScore;
//         return totalScore;
//     }
//     public int currentScore;
    
// }
public abstract class BaseMovement : MonoBehaviour
{
    public GameObject tailPrefab;
    public List<Transform> body;
    protected Rigidbody rb;
    public int points = 0;
    protected float speed = Config.PLAYER_SPEED;
    public float MaxSpeed;
    public float MinSpeed;
    protected float rotationSpeed = Config.PLAYER_ROTATION;
    public GameObject auraPrefab;
    protected Transform auraTransform;
    public Stack<Powerup> powerups;
    protected Powerup powerup;
    protected bool powerupBeingUsed;
    protected float timeBetweenJumps = 2f;
    protected bool alive;
    protected float jumpTimer;

    protected bool onGround;

    void OnCollisionStay()
    {
        onGround = true;
    }

    protected void OnCollisionEnter(Collision collision)
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

    protected void CollideWithOtherSnake(Collision collision)
    {
        if (body.Contains(collision.transform))
        {
            return;
        }
        Debug.Log("I have died");
        points = 0;
        alive=false;
        transform.position = gameController.GetRandomPosition();
        foreach (Transform part in body)
            Destroy(part.gameObject);
        body = new List<Transform>();
    }
    protected void CollideWithPoint(Collision collision)
    {
        GameStateHandler.pointList.Remove(collision.gameObject);
        Destroy(collision.gameObject);
        points += 1;
        add_tail();
        increase_aura();
        
        
    }

    protected void CollideWithPowerup(Collision collision){
        GameObject powerupGameObject = collision.gameObject;
        Powerup powerup = powerupGameObject.GetComponent<Powerup>();
        // powerups can be null
        if (powerups != null && powerup != null){
            if ( powerups.Count == 1){
                Powerup speedPowerup = this.gameObject.AddComponent<Speed>();
            // Powerup speed = this.gameObject.AddComponent<Speed>();
                speedPowerup.setPowerup("speed", true, false);
                powerups.Push(speedPowerup);
                powerup.isActive = true;
                Debug.Log("powerup type: " + powerup.powerupType);
                Debug.Log("powerup is active: " + powerup.isActive);
                Debug.Log("stack peek" + powerups.Peek());
                GameStateHandler.powerupsList.Remove(collision.gameObject);
            
            }else if(powerups.Count  > 1){
                powerups.Pop(); //remove current powerup
                Powerup speedPowerup = this.gameObject.AddComponent<Speed>();
                speedPowerup.setPowerup("speed", true, false);
                powerups.Push(speedPowerup);
                powerup.isActive = true;
                Debug.Log("powerup type: " + powerup.powerupType);
                Debug.Log("powerup is active: " + powerup.isActive);
                Debug.Log("stack peek" + powerups.Peek());
                GameStateHandler.powerupsList.Remove(collision.gameObject);
            }
            Destroy(powerupGameObject);  
        }else{
            Debug.Log("could not find powerup script component");
        }
         
    }

    protected void moveForward()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    public void moveMyTail(float maxSpeed , float minSpeed)
    {
        if (body.Count > 0)
        {
            moveTail(0, transform, maxSpeed, minSpeed);
        }
        for (int i = 1; i < body.Count; i++)
        {
            moveTail(i, body[i - 1], maxSpeed , minSpeed);
        }
    }

    protected void moveTail(int i, Transform transform, float maxSpeed , float minSpeed)
    {
        var MaximumDistance = 1.3;
        var MinimumDistance = 1.0;
        var MaxSpeed = maxSpeed;
        var MinSpeed = minSpeed;
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

    protected void moveAura()
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

    protected void increase_aura()
    {
        if (points > 0 && auraTransform != null)
        {

            float size = points * 10 / 1500; //change this to modify size faster or slower
           
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
}
