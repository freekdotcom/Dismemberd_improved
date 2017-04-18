using UnityEngine;
using System.Collections;

public class TrapScript : MonoBehaviour {

	Animator animator;
	bool trapMoving;
	GameObject trapDoor;

	// Use this for initialization
	void Start () {

		trapMoving = false;
		animator = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Bones") {
			trapMoving = true;
			Traps ("Close");
		}
	}
	
	void Traps (string direction)
	{
			animator.SetTrigger(direction);
	}
}
