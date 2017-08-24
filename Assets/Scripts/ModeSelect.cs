using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelect : MonoBehaviour {

	// Use this for initialization
	public void goToMain () {
        SceneManager.LoadScene("main");
    }
	
	// Update is called once per frame
	public void goToCredit () {
        SceneManager.LoadScene("Credit");
    }
}