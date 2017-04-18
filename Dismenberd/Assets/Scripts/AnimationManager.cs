using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {

	Animator animator;
	Throwing throwing;
	Movement movement;
	float idleTimer = 0;

	void Start () {
		animator = GetComponent<Animator> ();
		movement = GetComponent<Movement> ();
		throwing = GetComponent<Throwing>();
		//animator.SetBool ("IdleShort", false);

	}
	
	// Update is called once per frame
	void Update () {

		
		//mist beide onderarmen
		if (movement.hasLeftArmLower || movement.hasRigthArmLower) {
			throwing.canThrow = true;
			movement.canPickUp = true;
		} else {
			throwing.canThrow = false;
			movement.canPickUp = false;
		}

		if (!Input.anyKey) {
			animator.SetBool ("Running", false);
			animator.SetBool ("Skipping", false);
			animator.SetBool("Crawling", false);
			idleTimer += Time.deltaTime;
		}
		//heeft alle benen
		if (Input.GetKey ("w") || Input.GetKey ("s") || Input.GetKey ("d") || Input.GetKey ("a")) {
			animator.SetBool ("Running", true);
			animator.SetBool ("Skipping", false);
			animator.SetBool("Crawling", false);
			movement.speed =4f;
		} 
		//heeft 1 van zijn onderbenen niet
		if ((!movement.hasLeftLowerLeg || !movement.hasRightLowerLeg) 
			&& (Input.GetKey ("w") || Input.GetKey ("s") || Input.GetKey ("d") || Input.GetKey ("a"))) {
				animator.SetBool ("Skipping", true);
				movement.speed = 4f;
				animator.SetBool("Running", false);
				animator.SetBool("Crawling", false);
		}

		//heeft linker of rechterbeen helemaal niet
		if((!movement.hasLeftLowerLeg && !movement.hasLeftUpperLeg) || (!movement.hasRightLowerLeg && !movement.hasRightUpperLeg)){
			animator.SetBool("Skipping", true);
			animator.SetBool("Running", false);
			animator.SetBool("Crawling", false);
			movement.speed = 4f;
		}
		//heeft bovenbeen links en rechts en de rest niet
		if (!movement.hasLeftLowerLeg && !movement.hasRightLowerLeg) {
			animator.SetBool ("Crawling", true);
			animator.SetBool("Skipping", false);
			animator.SetBool("Running", false);
			movement.speed = 4f;
		}

		//heeft hele linkerbeen niet en onderbeen rechts niet
		if(!movement.hasLeftLowerLeg && !movement.hasLeftUpperLeg && !movement.hasRightLowerLeg){
			animator.SetBool ("Crawling", true);
			animator.SetBool("Skipping", false);
			animator.SetBool("Running", false);
			movement.speed = 3f;
		}

		//heeft rechterbeen niet en onderbeen links niet
		if(!movement.hasRightLowerLeg && !movement.hasRightUpperLeg && !movement.hasLeftLowerLeg){
			animator.SetBool ("Crawling", true);
			animator.SetBool("Skipping", false);
			animator.SetBool("Running", false);
			movement.speed = 3f;
		}
		//heeft geen benen meer
		if (!movement.hasRightLowerLeg && !movement.hasRightUpperLeg && !movement.hasLeftLowerLeg && !movement.hasLeftUpperLeg) {
			animator.SetBool ("Crawling", true);
			animator.SetBool("Skipping", false);
			animator.SetBool("Running", false);
			movement.speed = 3f;			
			}
			
		}





		/////////////////////////////////////////////////////////////////////////////
		/// 
		/// 
		/// nieuwe character


		//idle animations
		/*if (idleTimer >= 15) {
			animator.SetBool ("IdleShort", true);
		}
		/*if (idleTimer >= 20) {
			animator.SetBool("IdleShort", false);
			animator.SetBool("IdleLong", true);
		}  */

		/*if(Input.anyKey)
		{
			idleTimer = 0;
		}

		if(Input.GetKeyDown ("w") || Input.GetKeyDown ("a") || Input.GetKeyDown ("s") || Input.GetKeyDown ("d")){
			animator.SetBool ("IdleShort", false);
			animator.SetBool("walk", true);
		}
		if (!Input.anyKey) {
			animator.SetBool("walk", false);
			animator.SetBool("CrawlingBoth", false);
			animator.SetBool("CrawlingRight", false);
			animator.SetBool("CrawlingLeft", false);
			animator.SetBool("skippingRight", false);
			animator.SetBool("SkippingLeft", false);
			idleTimer += Time.deltaTime;
			Debug.Log(idleTimer);
		}

		//Mist een Stuk linkerbeen, gaat nu hinkelen
		if (!movement.hasLeftLowerLeg) {
			animator.SetBool("walk", false);
			animator.SetBool("SkippingLeft", true);
			animator.SetBool ("IdleShort", false);
			movement.speed = 6f;
		}
		//Mist een Stuk rechterbeen, gaat nu hinkelen
		if (!movement.hasRightLowerLeg) {
			animator.SetBool("walk", false);
			animator.SetBool("skippingRight", true);
			animator.SetBool ("IdleShort", false);
			movement.speed = 6f;
		}

		if (!movement.hasRightLowerLeg && !movement.hasLeftLowerLeg) {
			animator.SetBool("CrawlingBoth", true);
			animator.SetBool("skippingRight", false);
			animator.SetBool("SkippingLeft", false);
			movement.speed = 4.5f;
		}

		if ((!movement.hasRightLowerLeg && !movement.hasLeftLowerLeg) && (!movement.hasLeftArmLower)) {
			animator.SetBool("CrawlingBoth", false);
			animator.SetBool("CrawlingRight", false);
			animator.SetBool("CrawlingLeft", true);
			movement.speed = 3f;
		}

		if ((!movement.hasRightLowerLeg && !movement.hasLeftLowerLeg) && (!movement.hasRigthArmLower)) {
			animator.SetBool("CrawlingBoth", false);
			animator.SetBool("CrawlingLeft", false);
			animator.SetBool("CrawlingRight", true);
			movement.speed = 3f;
		}

		if (movement.hasLeftArmLower || movement.hasRigthArmLower) {
			throwing.canThrow = true;
			movement.canPickUp = true;
		}
		if (throwing.hasThrown = true && movement.hasLeftArmLower && !movement.hasRigthArmLower ) {
			animator.SetBool ("walk", false);
			animator.SetBool ("skippingRight", false);
			animator.SetBool ("SkippingLeft", false);
			animator.SetBool ("ThrowLeftBoth", true);
		}

		if (throwing.hasThrown = true && !movement.hasLeftArmLower && movement.hasRigthArmLower ) {
			animator.SetBool ("walk", false);
			animator.SetBool ("skippingRight", false);
			animator.SetBool ("SkippingLeft", false);
			animator.SetBool ("ThrowRightBoth", true);
		}

		if (Throwing.holding == true && Throwing.currentObject.tag == "Box") {
			animator.SetBool ("walk", false);
			animator.SetBool ("skippingRight", false);
			animator.SetBool ("SkippingLeft", false);
			animator.SetBool ("HeavyLifting", true);
		}

		if (throwing.hasThrownBox == true) {
			animator.SetBool ("HeavyLifting", false);
			animator.SetBool ("HeavyLiftingDrop", true);
		}*/
	}

