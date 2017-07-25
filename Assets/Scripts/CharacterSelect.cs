using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour {
    int p1character;
    int p2character;
    int p3character;
    int p4character;
    bool p1Choosing;
    bool p2Choosing;
    bool p3Choosing;
    bool p4Choosing;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    public int characterNum = 3;

    public float selectTime = 30f;
    public float readyTime = 5f;
    public Text timer;
    float leftTime;
    int leftTimeint;
    bool moveToNextScene;

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
        leftTime = selectTime;
        moveToNextScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
        timerMove();
    }


    void playerMove()
    {
        //1p: wasd
        //2p: tfgh
        //3p: ijkl
        //4P: arrow
        //change to joysticButton later  
        if (InputManager.P1LeftButton() && p1Choosing)
            if (p1character > 1)
            {
                p1character--;
                p1.transform.position = new Vector3(p1.transform.position.x - 150, p1.transform.position.y, p1.transform.position.z);
            }
        if (InputManager.P1RightButton() && p1Choosing)
            if (p1character < characterNum)
            {
                p1character++;
                p1.transform.position = new Vector3(p1.transform.position.x + 150, p1.transform.position.y, p1.transform.position.z);
            }
        if (InputManager.P1CrossButton())
        {
            p1Choosing = false;
            PlayerPrefs.SetInt("p1", p1character);
        }

        if (InputManager.P2LeftButton() && p2Choosing)
            if (p2character > 1)
            {
                p2character--;
                p2.transform.position = new Vector3(p2.transform.position.x - 150, p2.transform.position.y, p2.transform.position.z);
            }
        if (InputManager.P2RightButton() && p2Choosing)
            if (p2character < characterNum)
            {
                p2character++;
                p2.transform.position = new Vector3(p2.transform.position.x + 150, p2.transform.position.y, p2.transform.position.z);
            }
        if (InputManager.P2CrossButton())
        {
            p2Choosing = false;
            PlayerPrefs.SetInt("p2", p2character);
        }

        if (InputManager.P3LeftButton() && p3Choosing)
            if (p3character > 1)
            {
                p3character--;
                p3.transform.position = new Vector3(p3.transform.position.x - 150, p3.transform.position.y, p3.transform.position.z);
            }
        if (InputManager.P3RightButton() && p3Choosing)
            if (p3character < characterNum)
            {
                p3character++;
                p3.transform.position = new Vector3(p3.transform.position.x + 150, p3.transform.position.y, p3.transform.position.z);
            }
        if (InputManager.P3CrossButton())
        {
            p3Choosing = false;
            PlayerPrefs.SetInt("p3", p3character);
        }

        if (InputManager.P4LeftButton() && p4Choosing)
            if (p4character > 1)
            {
                p4character--;
                p4.transform.position = new Vector3(p4.transform.position.x - 150, p4.transform.position.y, p4.transform.position.z);
            }
        if (InputManager.P4RightButton() && p4Choosing)
            if (p4character < characterNum)
            {
                p4character++;
                p4.transform.position = new Vector3(p4.transform.position.x + 150, p4.transform.position.y, p4.transform.position.z);
            }
        if (InputManager.P4CrossButton())
        {
            p4Choosing = false;
            PlayerPrefs.SetInt("p4", p4character);
        }
    }

    void timerMove()
    {
        if (leftTime > 0)
        {
            leftTime -= Time.deltaTime;
            leftTimeint = (int)leftTime;
            timer.text = leftTimeint.ToString();
        }
        else
        {
            if (moveToNextScene == false)
            {
                p1Choosing = false;
                PlayerPrefs.SetInt("p1", p1character);
                p2Choosing = false;
                PlayerPrefs.SetInt("p2", p2character);
                p3Choosing = false;
                PlayerPrefs.SetInt("p3", p3character);
                p4Choosing = false;
                PlayerPrefs.SetInt("p4", p4character);
                leftTime = readyTime;
                moveToNextScene = true;
            }
            else
            {
                //move to next scene;
            }
        }
    }
}
