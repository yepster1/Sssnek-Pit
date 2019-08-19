
using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

	
	public Transform target;
	public float smoothSpeed = 0.3f;
	private float yOffset = 100; 
	private Vector3 offset = new Vector3(0,100,0);
	private Vector3 velocity = Vector3.zero;

	//use late update so player and camera don't compete to avoid jittery movement
	void OnAwake(){
		
		// Vector3 desiredPosition = target.position + offset;
		
		// transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
	}

	void Update () {
		// GameObect p1 = GameObect.FindObjectWithTag("snake");
		//  Debug.Log(target.gameObject.tag);
		// Vector3 desiredPosition = target.position + offset;
		Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, yOffset));
		transform.position = Vector3.SmoothDamp(target.transform.position, targetPosition, ref velocity, smoothSpeed);
	}
}


