using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    public KimAttack kim;

    public float damage;
    public int attack;

    bool isExploded = false;
    bool isReady = false;

    void Start()
    {
        StartCoroutine(Delay());
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && other.gameObject != kim.gameObject && isReady)
        {
            other.GetComponent<CharacterControll>().Hit(damage, attack, (other.transform.position - transform.position).normalized);
            kim.UltimateGauge += damage * 1.5f;
            GetComponent<Animator>().SetTrigger("mine");
            isExploded = true;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.5f);
        isReady = true;
    }
    
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
