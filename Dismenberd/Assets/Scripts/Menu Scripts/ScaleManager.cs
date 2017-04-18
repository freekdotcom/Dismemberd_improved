using UnityEngine;
using System.Collections;

public class ScaleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var height = Camera.main.orthographicSize * 2.0f;
		var width = height * Screen.width / Screen.height;
		transform.localScale = new Vector3 (width * 1.5f, height, width);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
