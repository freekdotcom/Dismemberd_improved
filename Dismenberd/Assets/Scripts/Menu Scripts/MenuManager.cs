using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour {
	public Button PlayButton;
	public Button ControlsButton;
	public Button ExitGame;
	public Text controlstext;

	public Button level1;
	public Button level2;
	public Button level3;

	AsyncOperation op;

	// Use this for initialization
	void Start(){
		level1.gameObject.SetActive(false);
		level2.gameObject.SetActive(false);
		level3.gameObject.SetActive(false);
	}

	public void NextLevel(string levelname)
	{
		op = Application.LoadLevelAsync( levelname );
		op.allowSceneActivation = false;
		//Debug.Log (op.isDone);
	}

	public void ExitLevel()
	{
		Application.Quit ();
	}

	public void LevelSelect(){
		PlayButton.gameObject.SetActive(false);
		ExitGame.gameObject.SetActive(false);

		level1.gameObject.SetActive(true);
		level2.gameObject.SetActive(true);
		level3.gameObject.SetActive(true);
	}

	// Update is called once per frame
	void Update(){


		if (op != null) {
			Debug.Log (op.progress);
			if (op.progress >= 0.89) {
				op.allowSceneActivation = true;

			}
		}
	}
}
