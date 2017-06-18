using UnityEngine;
using System.Collections;

public class BodyPartManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider coll)
	{
		if(coll.gameObject.tag == "LiquidDead")
		{
			Destroy(gameObject);
		}
		
		if (coll.gameObject.tag == "FireSpitter") {
			Destroy(gameObject);
			Debug.Log("Collision Detected");
		}
		
		
	}
}
