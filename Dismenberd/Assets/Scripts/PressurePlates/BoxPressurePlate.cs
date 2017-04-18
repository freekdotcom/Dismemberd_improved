using UnityEngine;
using System.Collections;

public class BoxPressurePlate : PlateScript {

	public GameObject box;
	// Use this for initialization
	void Start () {
		box.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (isPressed) {
			box.gameObject.SetActive (true);
		} else {
			box.gameObject.SetActive (false);
		}
	}
}
