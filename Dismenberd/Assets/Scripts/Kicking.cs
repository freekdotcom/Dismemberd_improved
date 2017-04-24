using UnityEngine;
using System.Collections;

public class Kicking : MonoBehaviour {
    public float kickStrength;
	bool triggered;
	float collisionCheck;
	GameObject head;

	// Use this for initialization
	void Start () {
		triggered = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (triggered) {
			if (Input.GetKeyDown (KeyCode.Q)) {
				head.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 1, -1) * kickStrength);
				Movement.headMovement = true;

			}
		}
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "SkeletonHead") {
			head = coll.gameObject;
			triggered = true;
		}
	}

	void OnTriggerExit(){
		triggered = false;
		head = null;
	}
}
