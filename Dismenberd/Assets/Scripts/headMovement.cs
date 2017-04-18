using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class headMovement : MonoBehaviour {
	public GameObject[] character;
	public GameObject throwPart;
	GameObject player;
	Rigidbody rb;
	float speed = 4f;
	float rotationSpeed = 100f;
	Movement move;
	
	bool grounded;

	public static float headTimer; //timer hoelang je hoofd detached kan zijn van een body

	// Use this for initialization
	Door door;
	public bool touchDoor = false;
	
	
	public Text pickUpText;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();


		headTimer = 1000f;
		player = GameObject.FindGameObjectWithTag ("SkeletonHead");
	}
	
	// Update is called once per frame
	void Update () {
		if (headTimer != null) {
			headTimer -= Time.deltaTime;
			if (headTimer < 0) {
				player.gameObject.active = false;
			}
			//Debug.Log (headTimer);
		}

		if (Movement.headMovement) {
			float translationVertical = Input.GetAxis ("Vertical") * -speed;
			float translationHorizontal = Input.GetAxis ("Horizontal") * -speed;
			
			if (!grounded) {
				translationVertical = translationVertical / 3f;
				translationHorizontal = translationHorizontal / 3f;
			}

			speed = 8f;
			
			translationVertical *= Time.deltaTime;
			translationHorizontal *= Time.deltaTime;
			
			float rotation = Camera.main.transform.eulerAngles.y;
			if (Input.GetKey ("w") || Input.GetKey ("s") || Input.GetKey ("d") || Input.GetKey ("a")) {
				rb.drag = 1;
				transform.eulerAngles = new Vector3 (0, rotation + 180, 0);
			}
			else{
				//rb.drag = 5;
			}
			//transform.Translate (translationVertical, 0, translationHorizontal);
			rb.AddRelativeForce (translationHorizontal * 1.7f, 0, translationVertical * 1.7f, ForceMode.Impulse);

		}	
	}

	
	void OnCollisionStay(Collision coll){
		grounded = true;

		if (coll.gameObject.tag == "RightLowerLeg" || coll.gameObject.tag == "LeftLowerLeg" || coll.gameObject.tag == "Skeleton") {
			pickUpText.text = "Hold 'E' to pickup your body";
			Debug.Log("headtoucheslegorso");
			if(Input.GetKey("e")){
				Movement.headMovement = false;
				transform.position += new Vector3(0 , 0.5f, 0);
				move.hasHead = true;
				move.AttachPart (Movement.SKELETONHEAD);
				pickUpText.text = "";
			}
		}
	}
}
