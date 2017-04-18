using UnityEngine;
using System.Collections;

public class DoorSFX : MonoBehaviour {
	AudioClip door;
	private AudioSource audio;
	// Use this for initialization
	void Start () {
		door = Resources.Load<AudioClip>("Door");
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySound()
	{
		audio.PlayOneShot (door, 0.1f);
	}
}
