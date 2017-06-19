using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlateScript : MonoBehaviour {

	public static bool canSink = false;
	public bool doorOpens = false;
	public bool isPressed = false;
	public GameObject door;

	public Vector3 start;
	public Vector3 end;

	bool reached;

	AudioClip PP_On;
	AudioClip PP_Off;
	AudioClip DoorOpen;
	private AudioSource audio;

	// Use this for initialization
	void Start() {

		doorOpens = false;

		if (door.gameObject != null) {
			start = door.transform.position;
			end = new Vector3 (door.transform.position.x, -7.8f, door.transform.position.z);
		}

		PP_On = Resources.Load<AudioClip>("Pressureplate_On");
		PP_Off = Resources.Load<AudioClip>("Pressureplate_Off");
		DoorOpen = Resources.Load<AudioClip>("Door");
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (isPressed) {
			if(door.gameObject != null){
				door.transform.Translate (new Vector3 (0, -0.1f, 0));
				doorOpens = true;
			}
			door.GetComponent<DoorSFX>().PlaySound();
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag ("Bones") || col.gameObject.CompareTag("LeftUpperArm")
		    || col.gameObject.CompareTag("RightLowerArm") || col.gameObject.CompareTag("RightUpperArm") 
		    || col.gameObject.CompareTag("LeftUpperLeg") || col.gameObject.CompareTag("LeftLowerLeg") 
		    || col.gameObject.CompareTag("RightUpperLeg") || col.gameObject.CompareTag("RightLowerLeg")) 
		{
			isPressed = true;
			Debug.Log (isPressed);
			audio.PlayOneShot(PP_On, 0.5f);
		}

		if (col.gameObject.tag == "Box") {
			audio.PlayOneShot(PP_On, 0.5f);
			isPressed = true;
			Debug.Log (isPressed);
		}

		if (col.gameObject.tag == "Box") {
			isPressed = true;
			audio.PlayOneShot(PP_On, 0.5f);
		}

		if (col.gameObject.tag == "SkeletonHead") {
			isPressed = true;
			audio.PlayOneShot(PP_On, 0.5f);
		}
	}

	void OnCollisionExit(Collision col){
		if (col.gameObject.CompareTag ("LeftLowerArm") || col.gameObject.CompareTag ("LeftUpperArm")
			|| col.gameObject.CompareTag ("RightLowerArm") || col.gameObject.CompareTag ("RightUpperArm") 
			|| col.gameObject.CompareTag ("LeftUpperLeg") || col.gameObject.CompareTag ("LeftLowerLeg") 
			|| col.gameObject.CompareTag ("RightUpperLeg") || col.gameObject.CompareTag ("RightLowerLeg"))
		{
			isPressed = false;
			doorOpens = false;
			audio.PlayOneShot(PP_Off, 0.5f);
		}
		

		if (col.gameObject.tag == "Box") {
			isPressed = false;
			doorOpens = false;
			audio.PlayOneShot(PP_Off, 0.5f);
		}
	}
}
