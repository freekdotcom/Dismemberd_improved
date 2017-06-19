using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.name == "Player")
        {
            coll.gameObject.GetComponent<PlayerBooleanManager>().hasKey = true;
            DestroyObject(gameObject);
        }
    }
}
