using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoxesManager : MonoBehaviour {

	float timer = 6f;
	float kaas = 110;
	public GameObject[] boxes;
	public Text text;

	// Use this for initialization
	void Start () {
		text.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (PlateScript.canSink);

		if (PlateScript.canSink == true) {
			text.enabled = true;
			text.text = "time left: " + timer;
			timer -= Time.deltaTime;
			if(timer < 0)
			{
				text.enabled = false;
				boxes[0].transform.position = new Vector3(boxes[0].transform.position.x, kaas, boxes[0].transform.position.z);
				boxes[1].transform.position = new Vector3(boxes[1].transform.position.x, kaas, boxes[1].transform.position.z);
				boxes[2].transform.position = new Vector3(boxes[2].transform.position.x, kaas, boxes[2].transform.position.z);
				boxes[3].transform.position = new Vector3(boxes[3].transform.position.x, kaas, boxes[3].transform.position.z);
				boxes[4].transform.position = new Vector3(boxes[4].transform.position.x, kaas, boxes[4].transform.position.z);

				timer += Time.deltaTime;
				if(timer > 5)
				{
					PlateScript.canSink = false;
				}
			}
		}
	}
}
