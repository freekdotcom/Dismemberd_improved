using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {
	AudioClip win;
	AudioSource audio;

	bool songPlayed = false;

	// Use this for initialization
	void Start () {
		win = Resources.Load<AudioClip>("Win");
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("p")) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void OnParticleCollision(GameObject other){
		if (other.gameObject.tag == "Bones") {
				audio.PlayOneShot(win, 0.2F);
				if(Application.loadedLevel == 3){
					Application.LoadLevel (0);
					songPlayed = false;
				}
				else{
					Application.LoadLevel (Application.loadedLevel + 1);
					songPlayed = false;
				}
			}
		}

	void Win()
	{
		if(!audio.isPlaying)
		{

		}
	}
}
