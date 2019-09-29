using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
	void Update()
	{
		//Note how deltatime is used here (Hint: not in FixedUpdate!)
		transform.Rotate(new Vector3(15f,30f,0f)*Time.deltaTime);
	}
}
