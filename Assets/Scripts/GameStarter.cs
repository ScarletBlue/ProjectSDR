using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour {

    public AudioSource StartWhistleLongSource;
    public AudioSource StartWhistleShortSource;

    public AudioClip StartWhistleLongClip;
    public AudioClip StartWhistleShortClip;

    public GameObject Kim;
    public GameObject MP;
    public GameObject Cat;
    public GameObject Bunny;

    public bool gameStop = false;
    void Start()
    {
        StartWhistleLongSource.clip = StartWhistleLongClip;
        StartWhistleShortSource.clip = StartWhistleShortClip;
    }

    private void Update()
    {
        if (!gameStop)
        {
            gameStop = true;
            GameStartAfterThreeSecond();

        }

    }
    public void GameStartAfterThreeSecond()
    {
        Kim.GetComponent<CharacterControll>().enabled = false;
        MP.GetComponent<CharacterControll>().enabled = false;
        Cat.GetComponent<CharacterControll>().enabled = false;
        Bunny.GetComponent<CharacterControll>().enabled = false;
        StartCoroutine(GameStart());
    }
           
    public IEnumerator GameStart()
    {
        Debug.Log("0개부름");
        yield return new WaitForSeconds(1.0f);
        StartWhistleShortSource.Play();
        Debug.Log("1개부름");
        yield return  new WaitForSeconds(1.0f);
        StartWhistleShortSource.Play();
        Debug.Log("2개부름");
        yield return  new WaitForSeconds(1.0f);
        StartWhistleLongSource.Play();
        Debug.Log("3개부름");

        Kim.GetComponent<CharacterControll>().enabled = true;
        MP.GetComponent<CharacterControll>().enabled = true;
        Cat.GetComponent<CharacterControll>().enabled = true;
        Bunny.GetComponent<CharacterControll>().enabled = true;
        yield return null;
    }

}
