using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hittableSearchScript : MonoBehaviour
{

	GameObject[] hittableGameObjects;
	public bool HaveColidersOnEveryHittable;

	void Start ()
	{
		
		if (HaveColidersOnEveryHittable) {
			hittableGameObjects = GameObject.FindGameObjectsWithTag ("snake");
			foreach (GameObject hittable in hittableGameObjects) {
				AudioSource AS = hittable.AddComponent<AudioSource> ();
				AS.spatialBlend = 1;
				hittable.AddComponent<HittableSound> ();
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
