using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnEnter : MonoBehaviour {

	public string SceneNameToLoad;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (SceneNameToLoad != "") {
				SceneManager.LoadScene (SceneNameToLoad);
			}
		}
	}
}
