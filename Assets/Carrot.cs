using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour {

    public BunnyAttack BA;

    public bool isFlying = true;
    public bool isHit = false;
	void Start () {
		
	}
	
	void Update () {

        if (isFlying)
        {
            transform.Translate(-20 * transform.localScale.x * Time.deltaTime, 0, 0);
        }
        if (isHit)
        {
            Destroy(gameObject);
        }
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && other.gameObject != BA.gameObject)
        {
            isFlying = false;
            other.GetComponent<CharacterControll>().hit = true;
            other.GetComponent<CharacterControll>().Hit(50f, 7, new Vector2(BA.transform.localScale.x,0));
            BA.UltimateGauge += 75;
            GetComponent<Animator>().SetTrigger("hit");
        }
    }
}
