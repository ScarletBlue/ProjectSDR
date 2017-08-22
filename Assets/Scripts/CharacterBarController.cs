using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBarController : MonoBehaviour {
    float Health_percentage;
    float Ult_percentage;
    float Win_percentage;
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

	// Use this for initialization
	void Start () {
        Health_percentage = 1;
        Ult_percentage = 0;
        Win_percentage = 0;
    }
	
	// Update is called once per frame
	void Update () {
        GuageControl(Health_bar, Health_percentage, HPconstant1, HPconstant2);
        GuageControl(Ult_bar, Ult_percentage, Ultconstant1, Ultconstant2);
        GuageControl(Win_bar, Win_percentage, Winconstant1, Winconstant2);
        //Health_percentage -= Time.deltaTime / 30;
        //Ult_percentage += Time.deltaTime / 30;
        //Win_percentage += Time.deltaTime / 30;
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
