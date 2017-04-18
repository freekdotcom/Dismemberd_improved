using UnityEngine;
using System.Collections;

public class CluePressurePlate : PlateScript {

	public GameObject clue;
	// Use this for initialization
	void Start () {
		isPressed = false;
		clue.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (isPressed) {
			clue.gameObject.SetActive (true);
		} else {
			clue.gameObject.SetActive(false);
		}
	}
}
