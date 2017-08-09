using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateNewMovingFlatform : MonoBehaviour {

    public GameObject movingFlatform;
    public float maxRespwanTime;
    public float minRespwanTime;

    void Start ()
    {
        StartCoroutine(MakeMovingTile());
    }

    public IEnumerator MakeMovingTile()
    {
        while (true)
        {
            Vector3 spawnPosition = transform.position;
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(movingFlatform, transform.position, transform.rotation);
            yield return new WaitForSeconds(Random.Range(maxRespwanTime,minRespwanTime));
        }
    }
}
