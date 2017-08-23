using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    public AudioSource FireballExplosionSource;
    public AudioSource FireballFireSource;
    public AudioClip FireballExplosionClip;
    public AudioClip FireballFireClip;
    public MelonPixieAttack MPA;

    public float damage;
    public int attack;

    public float speed;
    public Vector3 direction;

    void Start ()
    {
        FireballExplosionSource.clip = FireballExplosionClip;
        FireballFireSource.clip = FireballFireClip;
        FireballFireSource.Play();
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
            FireballExplosionSource.Play();
            Destroy(gameObject);
        }
    }

    void Fly()
    {
        transform.Translate(direction.x * speed * Time.deltaTime, 0, 0);
    }
}
