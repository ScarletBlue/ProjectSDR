using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryControl : MonoBehaviour {

    public GameObject Kim;
    public GameObject MP;
    public GameObject Cat;
    public GameObject Bunny;
    public GameObject victoryUI;
    public Text winText;
    string winnerName;
	public SSBCamera ssbCamera;
    public AudioSource endSoundSource;
    public AudioClip endSoundClip;
    private void Start()
    {
        victoryUI.gameObject.SetActive(false);
        endSoundSource.clip = endSoundClip;
    }

	public void setVictory(GameObject winner)
    {
		//ssbCamera.victoryCondition = true;
		ssbCamera.ZoomToWinner(winner);
        winnerName = winner.name;
        winText.GetComponent<Text>().text = winnerName + " Wins!";
        
        Kim.GetComponent<CharacterControll>().enabled = false;
        Kim.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        MP.GetComponent<CharacterControll>().enabled = false;
        MP.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Cat.GetComponent<CharacterControll>().enabled = false;
        Cat.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Bunny.GetComponent<CharacterControll>().enabled = false;
        Bunny.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        victoryUI.gameObject.SetActive(true);

        endSoundSource.Play();
    }
}
