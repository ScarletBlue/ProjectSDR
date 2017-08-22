using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public GameObject nuclear;
    public List<GameObject> playersInRange;
    public CharacterControll CC;

    GameObject newNuclear;
    CircleCollider2D collider;

    bool isDestroyed = false;

	void Start () {
        collider = GetComponent<CircleCollider2D>();
        collider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDestroyed)
        {
            transform.Translate(-1, 0, 0);
        }
	}

    public void Destroy()
    {
        isDestroyed = true;
        GetComponent<SpriteRenderer>().enabled = false;
        collider.enabled = true;
        newNuclear = Instantiate(nuclear, transform.position, Quaternion.identity);
        StartCoroutine(DestroyThis());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character") && other.gameObject.transform.position.y >= transform.position.y -1 && other.gameObject != CC.gameObject)
        {
            playersInRange.Add(other.gameObject);
        }
        foreach(var player in playersInRange)
        {
            float distance = (player.transform.position - transform.position).magnitude;
            Vector3 direction = (player.transform.position - transform.position).normalized;
            player.GetComponent<CharacterControll>().Hit(800 - 70 * distance, Mathf.RoundToInt(13 - 1f * distance), direction);
        }
        collider.enabled = false;
    }

    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(2f);
        Destroy(newNuclear);
        Destroy(gameObject);
    }
}
