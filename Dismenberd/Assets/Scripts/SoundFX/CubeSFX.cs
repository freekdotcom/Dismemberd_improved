using UnityEngine;
using System.Collections;

public class CubeSFX : MonoBehaviour {
	AudioClip cube;
	private AudioSource audio;

	private float distToGround;
	// Use this for initialization
	void Start () {
		distToGround = GetComponent<Collider>().bounds.extents.y;

		cube = Resources.Load<AudioClip>("Metal_Block_Drop");
		audio = GetComponent<AudioSource>();
	}

	bool IsGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up, distToGround - 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (IsGrounded ()) {
			audio.PlayOneShot(cube, 0.3f);
		}
	}
}
