﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Throwing : MonoBehaviour {
	
	public GameObject throwStartPosition;
	public GameObject kickStartPosition;
	public GameObject boxStartPosition;
	public static GameObject currentObject;
	public static bool holding;

	public bool canThrow;
	bool hasThrown ,hasThrownBox;

	public float throwSpeed;
	public Slider throwForce;

	public Text pickUpThrow;

	AudioClip throwSFX;
	AudioSource audio;
	
	// Use this for initialization
	void Start () {
		canThrow = true;
		hasThrown = false;

		throwSFX = Resources.Load<AudioClip>("Throw");
		audio = GetComponent<AudioSource>();

		throwSpeed = 0f;
		throwForce.value = throwSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		if (throwSpeed <= 1.5f)
		{
			if (Input.GetMouseButton(1))
			{
				throwSpeed += 0.01f;
				throwForce.value = throwSpeed;
			}
		}


		if (holding) {
			hasThrownBox = false;
			if(currentObject.tag == "Box"){
				pickUpThrow.text = "";
				currentObject.transform.position = boxStartPosition.transform.position;
				currentObject.transform.rotation = boxStartPosition.transform.rotation;
				currentObject.GetComponent<Rigidbody> ().isKinematic = true;

				if (Input.GetMouseButtonDown (0)) {
					audio.PlayOneShot(throwSFX, 0.5f);
					currentObject.GetComponent<Rigidbody> ().isKinematic = false;
					currentObject.GetComponent<Rigidbody> ().AddRelativeForce ((new Vector3 (0, 1, -1) * 500) * throwSpeed);
					currentObject = null;
					holding = false;

				}
				else if(Input.GetMouseButtonDown (1)){
					audio.PlayOneShot(throwSFX, 0.5f);
					currentObject.GetComponent<Rigidbody> ().isKinematic = false;
					holding = false;
					currentObject = null;
					hasThrownBox = true;
				}
				else if(Input.GetMouseButtonDown (1)){
					audio.PlayOneShot(throwSFX, 0.5f);
					currentObject.GetComponent<Rigidbody> ().isKinematic = false;
					holding = false;
					currentObject = null;
				}
			}
			
			else{
				pickUpThrow.text = "";
				currentObject.transform.position = throwStartPosition.transform.position;
				currentObject.transform.rotation = throwStartPosition.transform.rotation;
				currentObject.GetComponent<Rigidbody> ().isKinematic = true;
				
				if (Input.GetMouseButtonDown (0)) {
					if(currentObject.tag == "SkeletonHead"){
						Movement.headMovement = true;
					}
					audio.PlayOneShot(throwSFX, 0.5f);
					currentObject.GetComponent<Rigidbody> ().isKinematic = false;
					currentObject.GetComponent<Rigidbody> ().AddRelativeForce ((new Vector3 (0, 1, -1) * 500) * throwSpeed);
					currentObject = null;
					holding = false;
					hasThrown = true;
				}
				else if(Input.GetMouseButtonDown (1)){
					audio.PlayOneShot(throwSFX, 0.5f);
					currentObject.GetComponent<Rigidbody> ().isKinematic = false;
					holding = false;
					currentObject = null;
				}
				else if(Input.GetMouseButtonDown (1)){
					audio.PlayOneShot(throwSFX, 0.5f);
					currentObject.GetComponent<Rigidbody> ().isKinematic = false;
					holding = false;
					currentObject = null;
				}
			}
		} 
	}

    void CollisionStuff(Collider coll, string bodyPart)
    {
        if (coll.gameObject.tag == bodyPart && !holding && canThrow)
        {
            pickUpThrow.text = "Hold 'R' to pickup for throw";
            if (Input.GetKey("r"))
            {
                holding = true;
                currentObject = coll.gameObject;
                coll.gameObject.transform.position = throwStartPosition.transform.position;
            }
        }
    }


	void OnTriggerStay(Collider coll){

	    switch (coll.gameObject.tag)
	    {
            case "LeftUpperArm":
                CollisionStuff(coll, "LeftUpperArm");
                break;

            case "LeftLowerArm":
                CollisionStuff(coll, "LeftLowerArm");
                break;

            case "RightUpperArm":
                CollisionStuff(coll, "RightUpperArm");
                break;

            case "RightLowerArm":
                CollisionStuff(coll, "RightLowerArm");
                break;

            case "LeftUpperLeg":
                CollisionStuff(coll, "LeftUpperLeg");
                break;

            case "LeftLowerLeg":
                CollisionStuff(coll, "LeftLowerLeg");
                break;

            case "RightUpperLeg":
                CollisionStuff(coll, "RightUpperLeg");
                break;

            case "RightLowerLeg":
                CollisionStuff(coll, "RightLowerLeg");
                break;

            case "SkeletonHead":
                CollisionStuff(coll, "SkeletonHead");
                break;

            case "Box":
                CollisionStuff(coll, "Box");
                break;

            default:
	            break;
	    }		

	}
	void OnTriggerExit(Collider coll){
		pickUpThrow.text = "";
	}
	
}
