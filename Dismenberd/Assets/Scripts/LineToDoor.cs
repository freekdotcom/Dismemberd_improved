using UnityEngine;
using System.Collections;

public class LineToDoor : MonoBehaviour {

	public GameObject pressurePlate;
	public Material notPressed;
	public Material pressed;
	Renderer renderer;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		PlateScript plate = pressurePlate.GetComponent<PlateScript>();
		bool open = plate.doorOpens;

		if (open) {
			renderer.material = pressed;
		} else {
			renderer.material = notPressed;
		}
	}
}
