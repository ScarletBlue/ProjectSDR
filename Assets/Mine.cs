using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    public KimAttack kim;

    public float damage;
    public Attack attack;

    bool isExploded = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && other.gameObject != kim.gameObject)
        {
            other.GetComponent<CharacterController>().Hit(damage, attack, (other.transform.position - transform.position).normalized);
            kim.UltimateGauge += damage;
            GetComponent<Animator>().SetTrigger("mine");
            isExploded = true;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(Destroy());
        }
    }
    
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
