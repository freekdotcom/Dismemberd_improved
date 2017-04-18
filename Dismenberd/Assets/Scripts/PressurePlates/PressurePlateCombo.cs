using UnityEngine;
using System.Collections;

public class PressurePlateCombo : MonoBehaviour {

	public bool isPressed;
	public bool openDoor;
	// Use this for initialization
	void Start () {
		isPressed = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Bones") {
			isPressed = true;
			Debug.Log (isPressed + " " + gameObject.name);
		}
	}
}
