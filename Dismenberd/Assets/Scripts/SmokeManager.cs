using UnityEngine;
using System.Collections;

public class SmokeManager : MonoBehaviour {

	Movement movement;
	float timer = 5;
	//public ParticleSystem part;
	public bool isded = false;
	// Use this for initialization
	void Start () {
		//canSpit = part.enableEmission;
		//part.enableEmission = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(isded){
				timer -= Time.deltaTime;
		}
		if (timer < 0) {
			Application.LoadLevel (Application.loadedLevel);
		}

		//Debug.Log (timer + " fire");
	}
	
	void OnParticleCollision(GameObject other){
		if (other.gameObject.tag == "Bones") {
			Debug.Log ("hit player");
			isded = true;
			Destroy (other);
		}
		if (other.gameObject.tag == "SkeletonHead") {
			Debug.Log ("hit player");
			isded = true;
			Destroy (other);
		}
	}
}
