using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour {
    int p1character = 1;
    int p2character = 1;
    int p3character = 1;
    int p4character = 1;
    bool p1Choosing = true;
    bool p2Choosing = true;
    bool p3Choosing = true;
    bool p4Choosing = true;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    public Text timer;
    public int characterNum = 3;
    public int selectTime = 100;

    // Use this for initialization
    void Start () {
        p1character = 1;
        p2character = 1;
        p3character = 1;
        p4character = 1;
        p1Choosing = true;
        p2Choosing = true;
        p3Choosing = true;
        p4Choosing = true;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("Timer");
        //1p: wasd
        //2p: tfgh
        //3p: ijkl
        //4P: arrow
        //change to joysticButton later  
        if (Input.GetKeyDown(KeyCode.A) && p1Choosing)
            if (p1character > 1)
            {
                p1character--;
                p1.transform.position = new Vector3(p1.transform.position.x - 150, p1.transform.position.y, p1.transform.position.z);
            }
        if (Input.GetKeyDown(KeyCode.D) && p1Choosing)
            if (p1character < characterNum)
            {
                p1character++;
                p1.transform.position = new Vector3(p1.transform.position.x + 150, p1.transform.position.y, p1.transform.position.z);
            }
        if (Input.GetKeyDown(KeyCode.Q))
            p1Choosing = false;


        if (Input.GetKeyDown(KeyCode.F) && p2Choosing)
            if (p2character > 1)
            {
                p2character--;
                p2.transform.position = new Vector3(p2.transform.position.x - 150, p2.transform.position.y, p2.transform.position.z);
            }
        if (Input.GetKeyDown(KeyCode.H) && p2Choosing)
            if (p2character < characterNum)
            {
                p2character++;
                p2.transform.position = new Vector3(p2.transform.position.x + 150, p2.transform.position.y, p2.transform.position.z);
            }
        if (Input.GetKeyDown(KeyCode.R))
            p2Choosing = false;


        if (Input.GetKeyDown(KeyCode.J) && p3Choosing)
            if (p3character > 1)
            {
                p3character--;
                p3.transform.position = new Vector3(p3.transform.position.x - 150, p3.transform.position.y, p3.transform.position.z);
            }
        if (Input.GetKeyDown(KeyCode.L) && p3Choosing)
            if (p3character < characterNum)
            {
                p3character++;
                p3.transform.position = new Vector3(p3.transform.position.x + 150, p3.transform.position.y, p3.transform.position.z);
            }
        if (Input.GetKeyDown(KeyCode.U))
            p3Choosing = false;


        if (Input.GetKeyDown(KeyCode.LeftArrow) && p4Choosing)
            if (p4character > 1)
            {
                p4character--;
                p4.transform.position = new Vector3(p4.transform.position.x - 150, p4.transform.position.y, p4.transform.position.z);
            }
        if (Input.GetKeyDown(KeyCode.RightArrow) && p4Choosing)
            if (p4character < characterNum)
            {
                p4character++;
                p4.transform.position = new Vector3(p4.transform.position.x + 150, p4.transform.position.y, p4.transform.position.z);
            }
        if (Input.GetKeyDown(KeyCode.M))
            p4Choosing = false;
    }


    IEnumerator Timer()
    {
        for (int i = selectTime; i >= 0; i -= 1)
        {
            timer.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
    }
}
