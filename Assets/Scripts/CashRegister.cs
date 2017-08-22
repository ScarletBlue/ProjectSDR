using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour {

    public GameObject currentInterObj = null;
    public Inventory currentInterObjScriptInventory = null;
    public CharacterBarController currentInterObjScriptCBC = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentInterObj = other.gameObject;
        currentInterObjScriptInventory = currentInterObj.GetComponent<Inventory>();
        currentInterObjScriptCBC = currentInterObj.GetComponent<CharacterBarController>();
        


        if (currentInterObjScriptInventory.itemAdded)
        {
            if (currentInterObjScriptCBC.Win_percentage == 1)
            {
                //setVictory(other);
            }
        }
    }
}
