using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{

	public GameObject currentInterObj = null;
	public Inventory currentInterObjScriptInventory = null;
    public VictoryControl victoryControl;
	[SerializeField]
	CharacterBarController currentInterObjScriptCBC1;
	[SerializeField]
	CharacterBarController currentInterObjScriptCBC2;
	[SerializeField]
	CharacterBarController currentInterObjScriptCBC3;
	[SerializeField]
	CharacterBarController currentInterObjScriptCBC4;

	private void OnTriggerStay2D(Collider2D other)
	{

		if (other.CompareTag("Player") && other.gameObject.name == "KimJongUn")
		{
			currentInterObj = other.gameObject;
			currentInterObjScriptInventory = currentInterObj.GetComponent<Inventory>();

			Debug.Log(currentInterObjScriptCBC1.Win_percentage);

			if (currentInterObjScriptInventory.itemAdded)
			{
				if (currentInterObjScriptCBC1.Win_percentage >= 1.0f)
				{
                    victoryControl.setVictory(other.gameObject);
					

				}

			}
		}
		else if (other.CompareTag("Player") && other.gameObject.name == "Melonpixie")
		{
			currentInterObj = other.gameObject;
			currentInterObjScriptInventory = currentInterObj.GetComponent<Inventory>();

			Debug.Log(currentInterObjScriptCBC2.Win_percentage);
			if (currentInterObjScriptInventory.itemAdded)
			{
				if (currentInterObjScriptCBC2.Win_percentage >= 1.0f)
				{
                    victoryControl.setVictory(other.gameObject);
                }

			}
		}
		else if (other.CompareTag("Player") && other.gameObject.name == "Cat")
		{
			currentInterObj = other.gameObject;
			currentInterObjScriptInventory = currentInterObj.GetComponent<Inventory>();

			Debug.Log(currentInterObjScriptCBC3.Win_percentage);
			if (currentInterObjScriptInventory.itemAdded)
			{
				if (currentInterObjScriptCBC3.Win_percentage >= 1.0f)
				{
                    victoryControl.setVictory(other.gameObject);

                }

			}
		}

		else if (other.CompareTag("Player") && other.gameObject.name == "Bunny")
		{
			currentInterObj = other.gameObject;
			currentInterObjScriptInventory = currentInterObj.GetComponent<Inventory>();

			Debug.Log(currentInterObjScriptCBC4.Win_percentage);
			if (currentInterObjScriptInventory.itemAdded)
			{
				if (currentInterObjScriptCBC4.Win_percentage >= 1.0f)
				{
                    victoryControl.setVictory(other.gameObject);

                }

			}
		
		}
	}
}
