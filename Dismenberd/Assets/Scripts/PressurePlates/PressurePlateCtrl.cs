using UnityEngine;
using System.Collections;

public class PressurePlateCtrl : MonoBehaviour {

	public PressurePlateCombo[] amountPlates;
	public GameObject openDoor;

	Vector3 start;
	Vector3 end;
	// Use this for initialization
	void Start () {
		if (openDoor.gameObject != null) {
			start = openDoor.transform.position;
			end = new Vector3 (openDoor.transform.position.x, -7.8f, openDoor.transform.position.z);
		}
		Reset ();
	}

	void Reset(){
		for (int i = 0; i < amountPlates.Length; i++)
			amountPlates [i].isPressed = false;
	}

	// Update is called once per frame
	void Update () {
		if(openDoor == null) return; bool swapFlag = true; 

		for(int i = 0; i < amountPlates.Length; i++){
			if(swapFlag){
				if(amountPlates[i].isPressed == false) swapFlag = false;
			}else{
				if(amountPlates[i].isPressed == true){
					Reset(); // true encountered after 1 false indicates out of order
				}
			}
		}
		// if swapFlag is true here, that means all plates are down in the correct order
		if(swapFlag == true){
			openDoor.transform.Translate (new Vector3 (0, -0.1f, 0));
			//openDoor = null; // break connection so we dont reprocess
		}  
		

	}
}
