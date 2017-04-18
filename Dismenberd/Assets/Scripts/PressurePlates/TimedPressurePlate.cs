using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimedPressurePlate : MonoBehaviour {

	public float timer = 0;
	float timerStart;
	bool pressed;
	public GameObject door;
	public Text countDownText;
	Vector3 doorTransformStart;
	// Use this for initialization
	void Start () {
		doorTransformStart = door.transform.position;
		timerStart = timer;
	}
	
	// Update is called once per frame
	void Update () {

		if (pressed && timer > 0 || timer < timerStart && timer > -1) {
			CountDownTimer ();
			ShowTimerGUI ();
			door.transform.Translate (new Vector3 (0, -0.1f, 0));
		}		
		else if(timer < 0){
			if(door.gameObject != null){
				DisableTimerGUI();
				pressed = false;
				timer = timerStart;
				door.transform.position = doorTransformStart;
			}
		}

		Debug.Log (pressed + " " + timer);
	}

	float CountDownTimer(){
		timer -= Time.deltaTime;
		return timer;
	}

	void ShowTimerGUI(){
		countDownText.text = "" + (int)timer;
	}

	void DisableTimerGUI(){
		countDownText.text = "";
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag ("LeftLowerArm") || col.gameObject.CompareTag("LeftUpperArm")
		    || col.gameObject.CompareTag("RightLowerArm") || col.gameObject.CompareTag("RightUpperArm") 
		    || col.gameObject.CompareTag("LeftUpperLeg") || col.gameObject.CompareTag("LeftLowerLeg") 
		    || col.gameObject.CompareTag("RightUpperLeg") || col.gameObject.CompareTag("RightLowerLeg")
		    || col.gameObject.CompareTag("SkeletonHead")) 
		{
			pressed = true;
		}
		
		if (col.gameObject.tag == "Box") {
			pressed = true;
		}
	}
	
	void OnCollisionExit(Collision col){
		if (col.gameObject.CompareTag ("LeftLowerArm") || col.gameObject.CompareTag ("LeftUpperArm")
		    || col.gameObject.CompareTag ("RightLowerArm") || col.gameObject.CompareTag ("RightUpperArm") 
		    || col.gameObject.CompareTag ("LeftUpperLeg") || col.gameObject.CompareTag ("LeftLowerLeg") 
		    || col.gameObject.CompareTag ("RightUpperLeg") || col.gameObject.CompareTag ("RightLowerLeg")
		    || col.gameObject.CompareTag("SkeletonHead")) 
		{
			pressed = false;
		}
		
		if (col.gameObject.tag == "Box") {
			pressed = false;
		}
	}
}
