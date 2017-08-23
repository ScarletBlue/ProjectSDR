using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBarController : MonoBehaviour {
    float Health_percentage;
    float Ult_percentage;
    public float Win_percentage;
    float HPconstant1 = -24.4f;
    float HPconstant2 = 77.6f;
    float Ultconstant1 = -35.2f;
    float Ultconstant2 = 80f;
    float Winconstant1 = -19.1f;
    float Winconstant2 = 74.6f;
    Vector3 HealthBarLocalTransform;
    Vector3 UltBarLocalTransform;
    Vector3 WinBarLocalTransform;

    public GameObject Health_bar;
    public GameObject Ult_bar;
    public GameObject Win_bar;

    public float MaxHP = 1000;
    public GameObject character;
    CharacterControll CC;
    public Inventory inventory;
    // Use this for initialization
    void Start () {
        //  0<= percentage <=1
        Health_percentage = 1;
        Ult_percentage = 0;
        Win_percentage = 0;
        CC = character.GetComponent<CharacterControll>();
        Health_percentage = CC.hp / MaxHP;
        Debug.Log(CC.hp);
        inventory = character.GetComponent<Inventory>();

    }
	
	// Update is called once per frame
	void Update () {
        GuageControl(Health_bar, Health_percentage, HPconstant1, HPconstant2);
        GuageControl(Ult_bar, Ult_percentage, Ultconstant1, Ultconstant2);
        GuageControl(Win_bar, Win_percentage, Winconstant1, Winconstant2);
        if (CC.hp >= 0)
            Health_percentage = CC.hp / MaxHP;
        else if (CC.hp < 0)
            Health_percentage = 0f;
        if (inventory.itemAdded == true && Win_percentage <= 1f)
            Win_percentage += 0.05f * Time.deltaTime;
        else if (Win_percentage >= 1f)
            Win_percentage = 1f;
        if (Ult_percentage <= 1f)
            Ult_percentage = CC.ult / 1000;
        else if (Ult_percentage >= 1f)
            Ult_percentage = 1f;

    }

    void GuageControl(GameObject bar, float percentage, float constant1, float constant2)
    {

        Vector3 barScale = bar.transform.localScale;
        barScale.x = percentage;
        bar.transform.localScale = barScale;

        Vector3 barPosition = bar.transform.localPosition;
        barPosition.x = constant1 + constant2 * percentage;
        bar.transform.localPosition = barPosition;
        return;
    }
}
