using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LiquidManager : MonoBehaviour {

	Movement movement;
	public Text text;
	float timer = 5;
	public bool canSpit, isded = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(isded){
			timer -= Time.deltaTime;
			text.enabled = true;
			text.text = "GAME OVER" + "\n" + "Retry in: " + timer;
			if(timer < 0)

				Application.LoadLevel(Application.loadedLevel);
		}


		//Debug.Log (timer);
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Bones") {
			Destroy (coll.gameObject);
			isded = true;
		}
	}


}
