using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryControl : MonoBehaviour {

    public GameObject Kim;
    public GameObject MP;
    public GameObject Cat;
    //public GameObject Bunny;
    public GameObject victoryUI;

    private void Start()
    {
        victoryUI.gameObject.SetActive(false);
    }

	public void setVictory(GameObject winner)
    {
        //cameraZoomIn();
        
        Kim.GetComponent<CharacterControll>().enabled = false;
        MP.GetComponent<CharacterControll>().enabled = false;
        Cat.GetComponent<CharacterControll>().enabled = false;
        //Bunny.GetComponent<CharacterControll>().enabled = false;

        //UIPopUp;
        victoryUI.gameObject.SetActive(true);
    }
}
