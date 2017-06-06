using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	public static LevelController main;

	public Transform eventSystemPrefab;

	public KeyCode pauseKey;

	public Image pauseScreen;

	bool isPaused;
	public bool GetIsPaused() {
		return isPaused;
	}

	void Awake() {
		if (LevelController.main != null) {
			Destroy (gameObject);
		}

		main = this;
	}

	// Use this for initialization
	void Start () {
		MakeSureEventSystemIsPresent ();

		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (pauseKey)) {
			if (isPaused) {
				isPaused = false;
				Time.timeScale = 1;
				pauseScreen.gameObject.SetActive(false);
			} 
			else {
				isPaused = true;
				Time.timeScale = 0;
				pauseScreen.gameObject.SetActive(true);
			}
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			RestartLevel ();
		}
	}

	void MakeSureEventSystemIsPresent() {
		if (GameObject.FindGameObjectWithTag ("EventSystem") == null) {
			Instantiate (eventSystemPrefab);
			Debug.Log ("Event System Added.");
		} else {
			Debug.Log ("Event System Present");
		}
	}

	void RestartLevel() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
