using UnityEngine;
using System.Collections;

public class FanManager : MonoBehaviour {
	
	public GameObject player, bodypart;
	public Vector3 WindForce;
	public Vector3 WindForceBones;
	Rigidbody rbs;
	Rigidbody rbb;
	bool canFly = false;
	ConstantForce rbc;
	ConstantForce rbd;
	public float timer = 0.5f;
	// Use this for initialization
	void Start () {
		rbc = player.GetComponent<ConstantForce> ();
		rbd = bodypart.GetComponent<ConstantForce> ();
		rbc.force = new Vector3 (0, 0, 0);
		rbd.force = new Vector3 (0, 0, 0);
	}
	
	void Update(){
		timer -= Time.deltaTime;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		
		if (timer < 0 ) {
			canFly = false;
			rbc.force = new Vector3(0,0,0);
			rbd.force = new Vector3(0,0,0);
		}
		
		if (canFly = false) {
			rbc.force = new Vector3(0,0,0);
			rbd.force = new Vector3(0,0,0);
		}
		Debug.Log (timer);
	}
	
	void OnParticleCollision(GameObject other)
	{
		if (other.gameObject.tag == "Bones") {
			canFly = true;
			rbc.force = WindForce;
			rbc.enabled = true;
			timer = 0.5f;
			Debug.Log ("player");
		}
		if(other.gameObject.name == "BodyPart(Clone)"){
			canFly = true;
			rbd.force = WindForceBones;
			rbd.enabled = true;
			timer = 0.5f;
			Debug.Log ("bodyParts");
		}
	}
}
