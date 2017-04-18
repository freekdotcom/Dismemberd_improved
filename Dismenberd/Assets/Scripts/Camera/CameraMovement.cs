using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Transform player;
	public float turnSpeed = 4.0f;
	public float height = 3f;
	public float distance = 2f;

	private Vector3 offsetX;
	private Vector3 offsetY;

	public static Quaternion Rotation;

	GameObject head;
	GameObject skeleton;

	void Start() {
		offsetX = new Vector3 (distance, height, 0);
		offsetY = new Vector3 (0, height, 0);	

		head = GameObject.FindGameObjectWithTag ("SkeletonHead");
		skeleton = GameObject.Find ("Skeleton_Complet");

		player = skeleton.transform;
	}

	void Update(){
		if (Input.GetKey ("f")) {
			head = GameObject.FindGameObjectWithTag ("SkeletonHead");
		}
		if (Input.GetKey ("e")) {
			skeleton = GameObject.Find ("Skeleton_Complet");
		}

		if (Input.GetKey ("k")) {
			Movement.headMovement = false;
			Debug.Log(head);
			Debug.Log(Movement.headMovement);
		}

		if (player == null || head == null) {
			player = skeleton.transform;
		}

		if (head != null) {
			if (Movement.headMovement) {
				player = head.transform;
			}
			if(!Movement.headMovement){
				player = skeleton.transform;
			}
		}
	}

	void LateUpdate() {
		offsetX = Quaternion.AngleAxis (Input.GetAxis ("Mouse X") * turnSpeed, Vector3.up) * offsetX;
		offsetY = Quaternion.AngleAxis (Input.GetAxis ("Mouse Y") * turnSpeed, Vector3.forward) * offsetY;

		transform.position = player.position + offsetY + offsetX;;
		transform.LookAt (player.position);
	}
}
