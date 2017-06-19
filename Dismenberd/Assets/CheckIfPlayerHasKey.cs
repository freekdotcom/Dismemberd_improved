using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfPlayerHasKey : MonoBehaviour {

    public GameObject destroy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.name == "Player")
        {
            if(coll.gameObject.GetComponent<PlayerBooleanManager>().hasKey)
            {
                DestroyObject(destroy);
            }
        }
    }
}
