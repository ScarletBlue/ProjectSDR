using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCheck : MonoBehaviour {

   // [System.NonSerialized]
    public List<GameObject> playersInRange;
    void Awake()
    {
    }
    void Start()
    {
        playersInRange = new List<GameObject>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character") && other.tag == "Player")
        {
            playersInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character") && other.tag == "Player")
        {
            playersInRange.Remove(other.gameObject);
        }
    }

}
