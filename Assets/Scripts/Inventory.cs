using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {


	public GameObject[] inventory = new GameObject[1];

	public void AddItem(GameObject item)
	{
		bool itemAdded = false;
		for (int i = 0; i < inventory.Length;  i++)
		{
			if(inventory [i] == null)
			{
				inventory[i] = item;
				Debug.Log(item.name + " was added");
				itemAdded = true;
				item.SendMessage("DoInteraction");
				break;
			}
		}

	}
}
