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
    public List<GameObject> body;
    public GameObject head;
    protected Rigidbody rb;
    public int points = 0;
    protected float speed = Config.PLAYER_SPEED;
    public float MaxSpeed;
    public float MinSpeed;
    protected float rotationSpeed = Config.PLAYER_ROTATION;
    public GameObject auraPrefab;
    protected Transform auraTransform;
    public static int tailNumber;
    public bool alive;

    public void init()
    {
        MaxSpeed = Config.MAX_PLAYER_SPEED;
        MinSpeed = Config.MIN_PLAYER_SPEED;
    }
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("snake"))
        {
            SoundManager.INSTANCE.PlaySpawn();
            CollideWithOtherSnake(collision);
        }
        if (collision.gameObject.tag.Equals("point"))
        {
            CollideWithPoint(collision);
            SoundManager.INSTANCE.playCollectPoint(null);
        }
        
        if (collision.gameObject.tag.Equals("powerup"))
        {
            // CollideWithPowerup(collision);
        }
    }

    protected void CollideWithOtherSnake(Collision collision)
    {
        if (body.Contains(collision.gameObject))
        {
            return;
        }
        Debug.Log("I have died");
        points = 0;
        alive=false;
        foreach (GameObject part in body){
            Destroy(part);
        }
        body = new List<GameObject>();
        startParticle(0, gameObject);
        transform.position = gameController.GetRandomPosition();
        startParticle(1, gameObject);
    }

    IEnumerable startParticle(float time, GameObject player)
    {
        yield return new WaitForSeconds(time);
        ParticleSystem particleSystem = player.GetComponentInChildren<ParticleSystem>();
        particleSystem.Play();
        StartCoroutine(StopParticleSystem(particleSystem, 1));
    }

    IEnumerator StopParticleSystem(ParticleSystem particleSystem, float time)
    {
        yield return new WaitForSeconds(time);
        particleSystem.Stop();
    }

    protected void CollideWithPoint(Collision collision)
    {
        GameStateHandler.pointList.Remove(collision.gameObject);
        Destroy(collision.gameObject);
        points += 1;
        
        
        add_tail();
        increase_aura();
        
        
    }

    // protected void CollideWithPowerup(Collision collision){
    //     GameObject powerupGameObject = collision.gameObject;
    //     Powerup powerup = powerupGameObject.GetComponent<Powerup>();
    //     // powerups can be null
    //     if (powerups != null && powerup != null){
    //         if ( powerups.Count == 1){
    //             Powerup speedPowerup = this.gameObject.AddComponent<Speed>();
    //         // Powerup speed = this.gameObject.AddComponent<Speed>();
    //             speedPowerup.setPowerup("speed", true, false);
    //             powerups.Push(speedPowerup);
    //             powerup.isActive = true;
    //             Debug.Log("powerup type: " + powerup.powerupType);
    //             Debug.Log("powerup is active: " + powerup.isActive);
    //             Debug.Log("stack peek" + powerups.Peek());
    //             GameStateHandler.powerupsList.Remove(collision.gameObject);
            
    //         }else if(powerups.Count  > 1){
    //             powerups.Pop(); //remove current powerup
    //             Powerup speedPowerup = this.gameObject.AddComponent<Speed>();
    //             speedPowerup.setPowerup("speed", true, false);
    //             powerups.Push(speedPowerup);
    //             powerup.isActive = true;
    //             Debug.Log("powerup type: " + powerup.powerupType);
    //             Debug.Log("powerup is active: " + powerup.isActive);
    //             Debug.Log("stack peek" + powerups.Peek());
    //             GameStateHandler.powerupsList.Remove(collision.gameObject);
    //         }
    //         Destroy(powerupGameObject);  
    //     }else{
    //         Debug.Log("could not find powerup script component");
    //     }
         
    // }

    protected void moveForward()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    public void moveMyTail(float maxSpeed , float minSpeed)
    {
        if (body.Count > 0)
        {
            if(body[0]!= null){
                moveTail(0, transform, maxSpeed, minSpeed);
            }
            
        }
        for (int i = 1; i < body.Count; i++)
        {
            if (body[i-1]!= null){
                moveTail(i, body[i - 1].transform, maxSpeed , minSpeed);
            }
            
        }
    }

    protected void moveTail(int i, Transform transform, float maxSpeed , float minSpeed)
    {
        var MaximumDistance = 1.3;
        var MinimumDistance = 1.0;
        var MaxSpeed = maxSpeed;
        var MinSpeed = minSpeed;
        var bodySpeed = 0.0;
        if (body[i]!= null){
            var dist = Vector3.Distance(body[i].transform.position, transform.position);

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
            body[i].transform.LookAt(transform);
            body[i].transform.Translate(body[i].transform.forward * (float)bodySpeed * Time.smoothDeltaTime, Space.World);
        }
        
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
        GameObject newPart;
         
        if (body.Count != 0)
        {
            
            newPart = Instantiate(tailPrefab as GameObject, body[body.Count - 1].transform.position - body[body.Count - 1].transform.forward, body[body.Count - 1].transform.rotation);
            tailNumber++;
            
            
        }
        else
        {
            auraTransform = Instantiate(auraPrefab as GameObject, transform.position - transform.forward, transform.rotation).transform;
            newPart = Instantiate(tailPrefab as GameObject, transform.position - transform.forward, transform.rotation);
            tailNumber = 0;
            
            
        }
        setColor(newPart.transform);
        
        Tail1 tail = newPart.GetComponent<Tail1>();
        tail.setHead(head);
        newPart = tail.add_tail(this.gameObject.name,newPart ,tailNumber);
        body.Add(newPart);
    }
    public abstract void setColor(Transform tail);

}
