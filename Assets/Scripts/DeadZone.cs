using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {

    public GameObject[] spawnPoints;
    int a = 0;
    Vector3 position;

    void Start()
    {
        a = Random.Range(0, 10);
        position = spawnPoints[a].GetComponent<Transform>().position;
        transform.position = position;
    }

    Vector3 RandomPositionSelecter()
    {
        a = Random.Range(0, 10);
        return spawnPoints[a].GetComponent<Transform>().position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterControll otherCCScript = other.gameObject.GetComponent<CharacterControll>();
            otherCCScript.hp = 0;
        }
    }
}
