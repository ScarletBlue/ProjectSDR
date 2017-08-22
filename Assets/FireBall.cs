using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    public MelonPixieAttack MPA;

    public float damage;
    public int attack;

    public float speed;
    public Vector3 direction;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        Fly();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && other.gameObject != MPA.gameObject)
        {
            other.GetComponent<CharacterControll>().Hit(damage, attack, direction);
            MPA.UltimateGauge += 300;
            Destroy(gameObject);
        }
    }

    void Fly()
    {
        transform.Translate(direction.x * speed * Time.deltaTime, 0, 0);
    }
}
