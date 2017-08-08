using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool isVictory;

    void Start () {
        isVictory = false;
    }
	
	void Update () {
        if (isVictory == true)
        {
            //EndGame();
        }
	}
}
