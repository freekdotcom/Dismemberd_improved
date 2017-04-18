using UnityEngine;
using System.Collections;

public class LavaSFX : MonoBehaviour {
    private AudioSource audio;
	// Use this for initialization
	void Start () {
	    audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Bones")
        {
            audio.Play();
        }
        else
        {
            //audio.Stop();
        }
    }
}
