using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTileController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("onFloor");
        other.transform.parent = transform;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.transform.parent = null;
    }
}