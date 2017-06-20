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

    public PlayerBooleanManager playerBooleanManager;
	GameObject player;
    private bool grounded;
	private bool canDie;
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
	    playerBooleanManager = new PlayerBooleanManager();

		canDie = false;
		rb = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
        playerBooleanManager.hasLeftArmLower = true;
        playerBooleanManager.hasRigthArmLower = true;
        playerBooleanManager.hasLeftLowerLeg = true;
        playerBooleanManager.hasRightLowerLeg = true;
        playerBooleanManager.hasLeftUpperArm = true;
        playerBooleanManager.hasRightUpperArm = true;
        playerBooleanManager.hasLeftUpperLeg = true;
        playerBooleanManager.hasRightUpperLeg = true;
        playerBooleanManager.hasHead = true;
        playerBooleanManager.hasKey = false;
		
		headMovement = false;
		
		player = GameObject.Find ("Skeleton_Complete");
		
		distToGround = GetComponent<Collider> ().bounds.extents.y;

		jump = Resources.Load<AudioClip>("Jump");
		audio = GetComponent<AudioSource>();
	}
	
	void FixedUpdate ()
	{
		
		if (!headMovement) {

            float translationVertical;
            float translationHorizontal;


            if(!playerBooleanManager.hasLeftLowerLeg && !playerBooleanManager.hasRightLowerLeg && !IsGrounded())
            {
                translationVertical = Input.GetAxis("Vertical") * -speed * Time.deltaTime / 2;
                translationHorizontal = Input.GetAxis("Horizontal") * -speed * Time.deltaTime / 2;
            }
            else if (!playerBooleanManager.hasLeftLowerLeg && !playerBooleanManager.hasRightLowerLeg && IsGrounded())
            {
                translationVertical = Input.GetAxis("Vertical") * -speed * Time.deltaTime / 2;
                translationHorizontal = Input.GetAxis("Horizontal") * -speed * Time.deltaTime / 2;
            }
            else if (IsGrounded())
            {
                translationVertical = Input.GetAxis("Vertical") * -speed * Time.deltaTime;
                translationHorizontal = Input.GetAxis("Horizontal") * -speed * Time.deltaTime;
            }
            else
            {
                translationVertical = Input.GetAxis("Vertical") * -speed * Time.deltaTime / 4;
                translationHorizontal = Input.GetAxis("Horizontal") * -speed * Time.deltaTime / 4;
            }
			//transform.Translate (translationVertical, 0, translationHorizontal);
			rb.AddRelativeForce (translationHorizontal * speed, 0, translationVertical * speed, ForceMode.Impulse);
			
			//rb.AddForce(transform.forward * -speed);
			
			//springen
			if (Input.GetKeyDown ("space") && IsGrounded () && playerBooleanManager.hasRightLowerLeg && playerBooleanManager.hasLeftLowerLeg) {
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
		if (!headMovement) {
			
			float rotation = Camera.main.transform.eulerAngles.y;
			if (Input.GetKey ("w") || Input.GetKey ("s") || Input.GetKey ("d") || Input.GetKey ("a")) {
				
				transform.eulerAngles = new Vector3 (0, rotation + 180, 0);
			}
		}
		
		if (!playerBooleanManager.hasRightLowerLeg && !playerBooleanManager.hasLeftLowerLeg) {
			turner.transform.rotation = Quaternion.Euler (180f, Camera.main.transform.eulerAngles.y + 180f, 0f);
			
		} else {
			turner.transform.rotation = Quaternion.Euler (-90f, Camera.main.transform.eulerAngles.y + 180f, 0f);
		}
		
		//linkerarm
		if (Input.GetKeyDown (KeyCode.UpArrow) && playerBooleanManager.hasLeftArmLower) {
			DetachPart (LEFTLOWERARM);
            playerBooleanManager.hasLeftArmLower = false;   
		} else if (Input.GetKeyDown (KeyCode.UpArrow) && !playerBooleanManager.hasLeftArmLower && playerBooleanManager.hasLeftUpperArm) {
            playerBooleanManager.hasLeftUpperArm = false;
			DetachPart (LEFTUPPERARM);
		}
		
		//rechterarm
		if (Input.GetKeyDown (KeyCode.DownArrow) && playerBooleanManager.hasRigthArmLower) {
			DetachPart (RIGHTLOWERARM);
            playerBooleanManager.hasRigthArmLower = false;
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && !playerBooleanManager.hasRigthArmLower && playerBooleanManager.hasRightUpperArm) {
            playerBooleanManager.hasRightUpperArm = false;
			DetachPart (RIGHTUPPERARM);
		}
		
		//linkerbeen
		if (Input.GetKeyDown (KeyCode.RightArrow) && playerBooleanManager.hasLeftLowerLeg) {
			DetachPart (LEFTLOWERLEG);
            playerBooleanManager.hasLeftLowerLeg = false;
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && !playerBooleanManager.hasLeftLowerLeg && playerBooleanManager.hasLeftUpperLeg) {
            playerBooleanManager.hasLeftUpperLeg = false;
			DetachPart (LEFTUPPERLEG);
		}
		
		//rechterbeen
		if (Input.GetKeyDown (KeyCode.LeftArrow) && playerBooleanManager.hasRightLowerLeg) {
			DetachPart (RIGHTLOWERLEG);
            playerBooleanManager.hasRightLowerLeg = false;
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && !playerBooleanManager.hasRightLowerLeg && playerBooleanManager.hasRightUpperLeg) {
            playerBooleanManager.hasRightUpperLeg = false;
			DetachPart (RIGHTUPPERLEG);
		}
		
		//hoofd
		if (Input.GetKeyDown (KeyCode.F) && playerBooleanManager.hasHead) {
            playerBooleanManager.hasHead = false;
			DetachPart (SKELETONHEAD);
			thirdPersonOrbitCamera.camOffset = new Vector3 (0f, 0f, -5f);
		}
		//if (playerBooleanManager.hasHead == true) {
		//	thirdPersonOrbitCamera.camOffset = new Vector3 (0f, 3f, -10f);
		//} 
			
	}
	
	//gooi een bepaald lichaamsdeel weg
	void DetachPart (int part)
	{
	    switch (part)
	    {
            case LEFTUPPERARM:
                Test(part, "BodyPart", "LeftUpperArm");
                break;

            case LEFTLOWERARM:
                Test(part, "BodyPart", "LeftLowerArm");
                break;

            case RIGHTUPPERARM:
                Test(part, "BodyPart", "RightUpperArm");
                break;

            case RIGHTLOWERARM:
                Test(part, "BodyPart", "RightLowerArm");
                break;

            case LEFTLOWERLEG:
                Test(part, "BodyPart", "LeftLowerLeg");
                break;

            case LEFTUPPERLEG:
                Test(part, "BodyPart", "LeftUpperLeg");
                break;

            case RIGHTLOWERLEG:
                Test(part, "BodyPart", "RightLowerLeg");
	            break;

            case RIGHTUPPERLEG:
                Test(part, "BodyPart", "RightUpperLeg");
                break;

            case SKELETONHEAD:
                Test(part, "SkullHead", "SkeletonHead");
                break;
	    }
	}

    void Test(int part, string resources, string tag)
    {
        character[part].gameObject.active = false;
        GameObject go = Instantiate(Resources.Load(resources), throwPart.transform.position, Quaternion.identity) as GameObject;
        go.tag = tag;
    }

    public void AttachPart (int part, GameObject gameObject)
	{
		character [part].gameObject.active = true;
        Throwing.currentObject = null;
        Throwing.holding = false;
        pickUpText.text = "";
        Destroy(gameObject);
    }
	
	void OnTriggerStay (Collider coll)
	{
		grounded = true;
		
		if (coll.gameObject.tag == "LeftUpperArm" && canPickUp) {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
				AttachPart (LEFTUPPERARM, coll.gameObject);
                playerBooleanManager.hasLeftUpperArm = true;
			}
		}
		
		if (coll.gameObject.tag == "LeftLowerArm" && playerBooleanManager.hasLeftUpperArm && canPickUp) {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
                playerBooleanManager.hasLeftArmLower = true;
				AttachPart (LEFTLOWERARM, coll.gameObject);	
			}
		}

		if (coll.gameObject.tag == "RightUpperArm" && canPickUp) {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
                playerBooleanManager.hasRightUpperArm = true;
				AttachPart (RIGHTUPPERARM, coll.gameObject);
			}
		}
		
		if (coll.gameObject.tag == "RightLowerArm" && playerBooleanManager.hasRightUpperArm && canPickUp) {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
                playerBooleanManager.hasRigthArmLower = true;
				AttachPart (RIGHTLOWERARM, coll.gameObject);
			}
		}

		if (coll.gameObject.tag == "LeftUpperLeg" && canPickUp) {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
				transform.position += new Vector3 (0, 1.5f, 0);
                playerBooleanManager.hasLeftUpperLeg = true;
				AttachPart (LEFTUPPERLEG, coll.gameObject);
			}
		}
		
		if (coll.gameObject.tag == "LeftLowerLeg" && playerBooleanManager.hasLeftUpperLeg && canPickUp) {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
				transform.position += new Vector3 (0, 1.5f, 0);
                playerBooleanManager.hasLeftLowerLeg = true;
				AttachPart (LEFTLOWERLEG, coll.gameObject);
			}
		}

		if (coll.gameObject.tag == "RightUpperLeg" && canPickUp) {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
				transform.position += new Vector3 (0, 1.5f, 0);
                playerBooleanManager.hasRightUpperLeg = true;
				AttachPart (RIGHTUPPERLEG, coll.gameObject);
			}
		}
		
		if (coll.gameObject.tag == "RightLowerLeg" && playerBooleanManager.hasRightUpperLeg && canPickUp) {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
				transform.position += new Vector3 (0, 1.5f, 0);
                playerBooleanManager.hasRightLowerLeg = true;
				AttachPart (RIGHTLOWERLEG, coll.gameObject);
			}
		}

		if (coll.gameObject.tag == "Wall")
			grounded = false;
		
		if (coll.gameObject.tag == "SkeletonHead") {
			pickUpText.text = "Hold 'E' to pickup";
			if (Input.GetKey ("e")) {
				headMovement = false;
				transform.position += new Vector3 (0, 0.5f, 0);
                playerBooleanManager.hasHead = true;
				AttachPart (SKELETONHEAD, coll.gameObject);
			}
		}
		
		if (coll.gameObject.tag == "Keyhole") {
			touchDoor = true;
		}
	}

    void GUIHelper(bool bodyPart, string firstPart, string secondPart, int firstQuater,
        int secondQuarter, int thirdQuarter, int fourthQuarter)
    {
        if (bodyPart)
        {
            GUI.TextArea(new Rect(firstQuater, secondQuarter, 
                thirdQuarter, fourthQuarter), firstPart);
        }
        else
        {
            GUI.TextArea(new Rect(firstQuater, secondQuarter, 
                thirdQuarter, fourthQuarter), secondPart);
        }
    }

	void OnGUI ()
	{
	
        GUIHelper(playerBooleanManager.hasLeftArmLower, "LowerLeftArm", "UpperLeftArm",
            100, 35, 95, 25);

        GUIHelper(playerBooleanManager.hasRightLowerLeg, "LowerLeftLeg", "UpperLeftLeg",
            35, 65, 95, 25);

        GUIHelper(playerBooleanManager.hasRightLowerLeg, "LowerRightLeg", "UpperRightLeg",
            175, 65, 95, 25);

        GUIHelper(playerBooleanManager.hasRightLowerLeg, "LowerRightArm", "UpperRightArm",
            100, 95, 95, 25);
		
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

	void OnTriggerExit (Collider coll)
	{
		if (coll.gameObject.tag == "Keyhole") {
			touchDoor = false;
		}
		pickUpText.text = "";
	}
}