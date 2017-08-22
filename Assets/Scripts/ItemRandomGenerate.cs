using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemRandomGenerate : MonoBehaviour {

    public GameObject[] spawnPoints;
    int a = 0;
    Vector3 position;

	// Use this for initialization
	void Start () {
        a = Random.Range(0, 10);
        position = spawnPoints[a].GetComponent<Transform>().position;
        transform.position = position;
	}
}
