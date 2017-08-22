using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public Text timer;
    public float maxTime = 60;

	// Use this for initialization
	void Start () {
        timer.text = ((int)maxTime).ToString();
	}
	
	// Update is called once per frame
	void Update () {
        maxTime -= Time.deltaTime;
        timer.text = ((int)maxTime).ToString();
    }
}
