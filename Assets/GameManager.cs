using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool isVictory;

    // Use this for initialization
    void Start () {
        isVictory = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (isVictory == true)
        {
            //EndGame();
        }
	}
}
