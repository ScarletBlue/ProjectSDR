using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour {

	public void DoInteraction()
	{
		gameObject.SetActive(false);
		//Destroy(gameObject);
	}
}
