using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryControl : MonoBehaviour {

    public GameObject Kim;
    public GameObject MP;
    public GameObject Cat;
    public GameObject Bunny;
    public GameObject victoryUI;

	public SSBCamera ssbCamera;

    private void Start()
    {
        victoryUI.gameObject.SetActive(false);
    }

	public void setVictory(GameObject winner)
    {
		//ssbCamera.victoryCondition = true;
		ssbCamera.ZoomToWinner(winner);
        
        Kim.GetComponent<CharacterControll>().enabled = false;
        Kim.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        MP.GetComponent<CharacterControll>().enabled = false;
        MP.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Cat.GetComponent<CharacterControll>().enabled = false;
        Cat.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Bunny.GetComponent<CharacterControll>().enabled = false;
        Bunny.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        //UIPopUp;
        victoryUI.gameObject.SetActive(true);
    }
}
