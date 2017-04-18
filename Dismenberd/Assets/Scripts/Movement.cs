using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Movement : MonoBehaviour
{
	public GameObject[] character;
	public GameObject throwPart;
	public GameObject turner;
	public GameObject MainCamera;
	public ThirdPersonOrbitCam thirdPersonOrbitCamera;
	Rigidbody rb;
	public float speed = 4f;
	float rotationSpeed = 100f;
	float timer = 2f;
	public Vector3 windForce, rotationForc;
	public bool hasLeftArmLower;
	public bool hasRigthArmLower;
	public bool hasLeftLowerLeg;
	public bool hasRightLowerLeg;
	public bool hasLeftUpperArm;
	public bool hasRightUpperArm;
	public bool hasLeftUpperLeg;
	public bool hasRightUpperLeg;
	public bool hasHead;
	GameObject player;
	bool grounded = false;
	public bool canDie = false;
	public bool canPickUp = true;
	public static bool headMovement;// camera volgt hoofd ja/nee
	
	public Texture left;
	public Texture right;
	public Texture up;
	public Texture down;
	public const int LEFTUPPERARM = 0;
	public const int LEFTLOWERARM = 1;
	public const int RIGHTUPPERARM = 2;
	public const int RIGHTLOWERARM = 3;
	public const int LEFTLOWERLEG = 4;
	public const int LEFTUPPERLEG = 5;
	public const int RIGHTLOWERLEG = 7;
	public const int RIGHTUPPERLEG = 6;
	public const int SKELETONHEAD = 8;
	//public const int SKELETON = 9;
	// Use this for initialization
	Door door;
	public bool touchDoor = false;
	Animator animator;
	public float rotation;
	private float distToGround;
	public Text pickUpText;
	
	AudioClip jump;
	AudioSource audio;

	// Use this for initialization
	void Start ()
	{
		canDie = false;
		rb = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
		hasLeftArmLower = true;
		hasRigthArmLower = true;
		hasLeftLowerLeg = true;
		hasRightLowerLeg = true;
		
		hasLeftUpperArm = true;
		hasRightUpperArm = true;
		hasLeftUpperLeg = true; 
		hasRightUpperLeg = true;
		
		hasHead = true;
		
		headMovement = false;
		
		player = GameObject.Find ("Skeleton_Complete");
		
		distToGround = GetComponent<Collider> ().bounds.extents.y;

		jump = Resources.Load<AudioClip>("Jump");
		audio = GetComponent<AudioSource>();
	}
	
	void FixedUpdate ()
	{
		
		
		if (!headMovement) {
			
			float translationVertical = Input.GetAxis ("Vertical") * -speed * Time.deltaTime;
			float translationHorizontal = Input.GetAxis ("Horizontal") * -speed * Time.deltaTime;
			//transform.Translate (translationVertical, 0, translationHorizontal);
			rb.AddRelativeForce (translationHorizontal * speed, 0, translationVertical * speed, ForceMode.Impulse);
			
			//rb.AddForce(transform.forward * -speed);
			
			//springen
			if (Input.GetKeyDown ("space") && IsGrounded () && hasRightLowerLeg && hasLeftLowerLeg) {
				rb.AddForce (transform.up * 400);
				audio.PlayOneShot(jump, 0.5f);
				grounded = false;
			}
		}
	}
	
	bool IsGrounded ()
	{
		return Physics.Raycast (transform.position, -Vector3.up, distToGround - 0.1f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		//movement
		/*if (headMovement) {
			player.gameObject.active = false;
		}*/
		if (!headMovement) {
			
			
			if (Input.GetKey (KeyCode.Escape)) {
				Application.Quit ();
			}
			
			float rotation = Camera.main.transform.eulerAngles.y;
			if (Input.GetKey ("w") || Input.GetKey ("s") || Input.GetKey ("d") || Input.GetKey ("a")) {
				
				transform.eulerAngles = new Vector3 (0, rotation + 180, 0);
			}
		}
		
		if (!hasRightLowerLeg && !hasLeftLowerLeg) {
			turner.transform.rotation = Quaternion.Euler (180f, Camera.main.transform.eulerAngles.y + 180f, 0f);
			
		} else {
			turner.transform.rotation = Quaternion.Euler (-90f, Camera.main.transform.eulerAngles.y + 180f, 0f);
		}
		
		//linkerarm
		if (Input.GetKeyDown (KeyCode.UpArrow) && hasLeftArmLower) {
			DetachPart (LEFTLOWERARM);
			hasLeftArmLower = false;   
		} else if (Input.GetKeyDown (KeyCode.UpArrow) && !hasLeftArmLower && hasLeftUpperArm) {
			hasLeftUpperArm = false;
			DetachPart (LEFTUPPERARM);
		}
		
		//rechterarm
		if (Input.GetKeyDown (KeyCode.DownArrow) && hasRigthArmLower) {
			DetachPart (RIGHTLOWERARM);
			hasRigthArmLower = false;
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && !hasRigthArmLower && hasRightUpperArm) {
			hasRightUpperArm = false;
			DetachPart (RIGHTUPPERARM);
		}
		
		//linkerbeen
		if (Input.GetKeyDown (KeyCode.RightArrow) && hasLeftLowerLeg) {
			DetachPart (LEFTLOWERLEG);
			hasLeftLowerLeg = false;
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && !hasLeftLowerLeg && hasLeftUpperLeg) {
			hasLeftUpperLeg = false;
			DetachPart (LEFTUPPERLEG);
		}
		
		//rechterbeen
		if (Input.GetKeyDown (KeyCode.LeftArrow) && hasRightLowerLeg) {
			DetachPart (RIGHTLOWERLEG);
			hasRightLowerLeg = false;
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && !hasRightLowerLeg && hasRightUpperLeg) {
			hasRightUpperLeg = false;
			DetachPart (RIGHTUPPERLEG);
		}
		
		//hoofd
		if (Input.GetKeyDown (KeyCode.F) && hasHead) {
			hasHead = false;
			DetachPart (SKELETONHEAD);
			thirdPersonOrbitCamera.camOffset = new Vector3 (0f, 0f, -5f);
		}
		if (hasHead == true) {
			thirdPersonOrbitCamera.camOffset = new Vector3 (0f, 3f, -10f);
		} 
		
		//hoofd
		if (Input.GetKeyDown (KeyCode.F) && hasHead) {
			hasHead = false;
			DetachPart (SKELETONHEAD);
			thirdPersonOrbitCamera.camOffset = new Vector3 (0f, 0f, -5f);
		}
		if (hasHead == true) {
			thirdPersonOrbitCamera.camOffset = new Vector3 (0f, 3f, -10f);
		}
		
		
		
	}
	
	//gooi een bepaald lichaamsdeel weg
	void DetachPart (int part)
	{ 
		if (part == LEFTUPPERARM) {
			character [part].gameObject.active = false;
			GameObject go = Instantiate (Resources.Load ("BodyPart"), throwPart.transform.position, Quaternion.identity) as GameObject;
			go.tag = "LeftUpperArm";
		}
		if (part == LEFTLOWERARM) {
			character [part].gameObject.active = false;
			GameObject go = Instantiate (Resources.Load ("BodyPart"), throwPart.transform.position, Quaternion.identity) as GameObject;
			go.tag = "LeftLowerArm";
		}
		if (part == RIGHTUPPERARM) {
			character [part].gameObject.active = false;
			GameObject go = Instantiate (Resources.Load ("BodyPart"), throwPart.transform.position, Quaternion.identity) as GameObject;
			go.tag = "RightUpperArm";
		}
		if (part == RIGHTLOWERARM) {
			character [part].gameObject.active = false;
			GameObject go = Instantiate (Resources.Load ("BodyPart"), throwPart.transform.position, Quaternion.identity) as GameObject;
			go.tag = "RightLowerArm";
		}
		if (part == LEFTLOWERLEG) {
			character [part].gameObject.active = false;
			GameObject go = Instantiate (Resources.Load ("BodyPart"), throwPart.transform.position, Quaternion.identity) as GameObject;
			go.tag = "LeftLowerLeg";
		}
		if (part == LEFTUPPERLEG) {
			character [part].gameObject.active = false;
			GameObject go = Instantiate (Resources.Load ("BodyPart"), throwPart.transform.position, Quaternion.identity) as GameObject;
			go.tag = "LeftUpperLeg";
		}
		if (part == RIGHTLOWERLEG) {
			character [part].gameObject.active = false;
			GameObject go = Instantiate (Resources.Load ("BodyPart"), throwPart.transform.position, Quaternion.identity) as GameObject;
			go.tag = "RightLowerLeg";
		}
		if (part == RIGHTUPPERLEG) {
			character [part].gameObject.active = false;
			GameObject go = Instantiate (Resources.Load ("BodyPart"), throwPart.transform.position, Quaternion.identity) as GameObject;
			go.tag = "RightUpperLeg";
		}
		if (part == SKELETONHEAD) {
			character [part].gameObject.active = false;
			GameObject go = Instantiate (Resources.Load ("SkullHead"), throwPart.transform.position, Quaternion.identity) as GameObject;
			go.tag = "SkeletonHead";
		}
	}
	
	public void AttachPart (int part)
	{
		character [part].gameObject.active = true;
	}
	
	void OnTriggerStay (Collider coll)
	{
		grounded = true;
		
		if (coll.gameObject.tag == "LeftUpperArm" && canPickUp) {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
				AttachPart (LEFTUPPERARM);
				Throwing.currentObject = null;
				Throwing.holding = false;
				hasLeftUpperArm = true;
				pickUpText.text = "";
				Destroy (coll.gameObject);
			}
		}
		
		
		if (coll.gameObject.tag == "LeftLowerArm" && hasLeftUpperArm && canPickUp) {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
				hasLeftArmLower = true;
				AttachPart (LEFTLOWERARM);
				Throwing.currentObject = null;
				Throwing.holding = false;
				pickUpText.text = "";
				Destroy (coll.gameObject);
				
			}
		}
		if (coll.gameObject.tag == "RightUpperArm" && canPickUp) {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
				hasRightUpperArm = true;
				AttachPart (RIGHTUPPERARM);
				Throwing.currentObject = null;
				Throwing.holding = false;
				pickUpText.text = "";
				Destroy (coll.gameObject);
			}
		}
		
		if (coll.gameObject.tag == "RightLowerArm" && hasRightUpperArm && canPickUp) {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
				hasRigthArmLower = true;
				AttachPart (RIGHTLOWERARM);
				Throwing.currentObject = null;
				Throwing.holding = false;
				pickUpText.text = "";
				Destroy (coll.gameObject);
			}
		}
		if (coll.gameObject.tag == "LeftUpperLeg" && canPickUp) {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
				transform.position += new Vector3 (0, 1.5f, 0);
				hasLeftUpperLeg = true;
				AttachPart (LEFTUPPERLEG);
				Throwing.currentObject = null;
				Throwing.holding = false;
				pickUpText.text = "";
				Destroy (coll.gameObject);
			}
		}
		
		if (coll.gameObject.tag == "LeftLowerLeg" && hasLeftUpperLeg && canPickUp) {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
				transform.position += new Vector3 (0, 1.5f, 0);
				hasLeftLowerLeg = true;
				AttachPart (LEFTLOWERLEG);
				Throwing.currentObject = null;
				Throwing.holding = false;
				pickUpText.text = "";
				Destroy (coll.gameObject);
			}
		}
		if (coll.gameObject.tag == "RightUpperLeg" && canPickUp) {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
				transform.position += new Vector3 (0, 1.5f, 0);
				hasRightUpperLeg = true;
				AttachPart (RIGHTUPPERLEG);
				Throwing.currentObject = null;
				Throwing.holding = false;
				pickUpText.text = "";
				Destroy (coll.gameObject);
			}
		}
		
		if (coll.gameObject.tag == "RightLowerLeg" && hasRightUpperLeg && canPickUp) {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
				transform.position += new Vector3 (0, 1.5f, 0);
				hasRightLowerLeg = true;
				AttachPart (RIGHTLOWERLEG);
				Throwing.currentObject = null;
				Throwing.holding = false;
				pickUpText.text = "";
				Destroy (coll.gameObject);
			}
		}
		if (coll.gameObject.tag == "Wall")
			grounded = false;
		
		if (coll.gameObject.tag == "SkeletonHead") {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
				headMovement = false;
				transform.position += new Vector3 (0, 0.5f, 0);
				hasHead = true;
				AttachPart (SKELETONHEAD);
				Throwing.currentObject = null;
				Throwing.holding = false;
				pickUpText.text = "";
				Destroy (coll.gameObject);
			}
		}
		
		if (coll.gameObject.tag == "Keyhole") {
			touchDoor = true;
		}
	}
	
	void OnGUI ()
	{
		
		//controls text
		if (hasLeftArmLower) {
			GUI.TextArea (new Rect (100, 35, 95, 25), "LowerLeftArm");
		} else {
			GUI.TextArea (new Rect (100, 35, 95, 25), "UpperLeftArm");
		}
		
		
		if (hasRightLowerLeg) {
			GUI.TextArea (new Rect (35, 65, 95, 25), "LowerLeftLeg");
		} else {
			GUI.TextArea (new Rect (35, 65, 95, 25), "UpperLeftLeg");
		}
		
		if (hasLeftLowerLeg) {
			GUI.TextArea (new Rect (175, 65, 95, 25), "LowerRightLeg");
		} else {
			GUI.TextArea (new Rect (175, 65, 95, 25), "UpperRightLeg");
		}
		
		if (hasRigthArmLower) {
			GUI.TextArea (new Rect (100, 95, 95, 25), "LowerRightArm");
		} else {
			GUI.TextArea (new Rect (100, 95, 95, 25), "UpperRightArm");
			
		}
		
		GUI.DrawTexture (new Rect (135, 10, 25, 25), up);
		GUI.DrawTexture (new Rect (10, 65, 25, 25), left);
		GUI.DrawTexture (new Rect (270, 65, 25, 25), right);
		GUI.DrawTexture (new Rect (135, 120, 25, 25), down);
		
		//controls text
		if (touchDoor) {
			GUI.TextArea (new Rect (100, 10, 150, 25), "Press 'o'to open the door");
		} else {
			GUI.TextArea (new Rect (0, 0, 0, 0), "");
		}
	}
	
	/*  if (touchDoor)
        {
            if (Input.GetKey("o"))
            {
                Debug.Log("werkt dit?");
                Destroy(GameObject.FindGameObjectWithTag("Door"));
                DetachPart(1);
                door.PlayAnimation();
                touchDoor = false;
            }
        }*/
	
	
	void OnTriggerExit (Collider coll)
	{
		if (coll.gameObject.tag == "Keyhole") {
			touchDoor = false;
		}
		pickUpText.text = "";
	}
}