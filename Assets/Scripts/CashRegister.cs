using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour {

    public GameObject currentInterObj = null;
    public Inventory currentInterObjScriptInventory = null;
	public CharacterBarController currentInterObjScriptCBC;

    private void OnTriggerStay2D(Collider2D other)
    {


		if (other.CompareTag("Player") && other.gameObject.name == "KimJongUn")
		{
			currentInterObj = other.gameObject;
			currentInterObjScriptInventory = currentInterObj.GetComponent<Inventory>();

			Debug.Log(currentInterObjScriptCBC.Win_percentage );

			if (currentInterObjScriptInventory.itemAdded )
			{
				if(currentInterObjScriptCBC.Win_percentage >= 1.0f)
				{

				}
					//setVictory(other);
			}
		}
	}
}
