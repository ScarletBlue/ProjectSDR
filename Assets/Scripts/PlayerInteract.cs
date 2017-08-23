using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {


	public GameObject currentInterObj = null;
	public InteractionObject currentInterObjScript = null;
	public Inventory inventory;
	private void Update()
	{
		if (Input.GetButtonDown("Interact") && currentInterObj)
		{
			//check to see if object is stored
			if(currentInterObjScript.inventory)
			{
				inventory.AddItem(currentInterObj);
			}


		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("interObject"))
		{
			Debug.Log(other.name);
			currentInterObj = other.gameObject;
			currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();

		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("interObject"))
		{
			if (other.gameObject == currentInterObj)
			{
				currentInterObj = null;
			}

		}
	}

}
