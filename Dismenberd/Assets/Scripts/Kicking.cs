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
				//Kick (kickStrength);
				head.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 1, -1) * kickStrength);
				Movement.headMovement = true;

			}
		}
		//Debug.Log (triggered);
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

	/*void Kick(float kickStrength)
	{
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
		{
			if(hit.rigidbody != null)
			{
				hit.rigidbody.AddForceAtPosition(Camera.main.transform.forward * kickStrength, hit.point, ForceMode.Impulse);
				thirdPersonOrbitCamera.camOffset = new Vector3(0f, 3f, -5f);
			
		}
	}
}
*/
}
