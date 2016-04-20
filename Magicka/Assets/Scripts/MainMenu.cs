using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	public GameObject mainMenuPanel;
	public GameObject mainButtons;
	public GameObject loadPanel;

	public Slider loadingSlider;
	public Text loadText;
	private int loadingProgress = 0;

	void Awake() {
	}

	// Use this for initialization
	void Start () {
		TogglePanel (true);
		loadPanel.SetActive (false);
	}


	/// Button Functions
	public void QuitGame() {
		Debug.Log ("Quit Game!");
		Application.Quit ();
	}
	public void BackButton() {
		TogglePanel (true);
	}
	public void HangarButton(){
		LoadPanelScreen ();
		StartCoroutine (LoadScreen ("Hangar"));
	}
	public void PowerPlantButton(){
		LoadPanelScreen ();
		StartCoroutine (LoadScreen ("PowerPlant"));
	}


	/// Panel Display Convenience Functions
	public void ShowMainMenu() {
		TogglePanel (false);
	}
	void LoadPanelScreen() {
		mainMenuPanel.SetActive (false);
		loadPanel.SetActive (true);
	}
	void TogglePanel(bool b) {
		mainMenuPanel.SetActive (!b);
		mainButtons.SetActive (b);
	}
	

	/// Loading Screen Coroutine
	IEnumerator LoadScreen(string s) {
		AsyncOperation aSync = Application.LoadLevelAsync (s);

		while (!aSync.isDone) {
			loadingSlider.value = loadingProgress;
			loadText.text = loadingProgress + "%";
			loadingProgress = (int)(aSync.progress * 100);

			yield return null;
		}
	}


} // end class MainMenu
