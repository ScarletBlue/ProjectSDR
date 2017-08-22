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
    int[] cursor;
    bool p1Choosing;
    bool p2Choosing;
    bool p3Choosing;
    bool p4Choosing;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    [SerializeField]
    private GameObject[] characterList;
    public int characterNum = 8;


    //timer
    public float selectTime = 30f;
    public float readyTime = 5f;
    public Text timer;
    float leftTime;
    int leftTimeint;
    bool moveToNextScene;

    // Use this for initialization
    void Start () {
        p1character = 0;
        p2character = 1;
        p3character = 2;
        p4character = 3;
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

    bool isOccupied(int num)
    {
        if (p1character == num)
            return true;
        if (p2character == num)
            return true;
        if (p3character == num)
            return true;
        if (p4character == num)
            return true;
        return false;
    }

    void playerMove()
    {
        //1p: wasd
        //2p: tfgh
        //3p: ijkl
        //4P: arrow
        //change to joysticButton later  

        if (p1Choosing)
        {
            if (InputManager.P1LeftButton() && p1character > 0 && !isOccupied(p1character - 1))
                p1character--;
            if (InputManager.P1RightButton() && p1character < characterNum - 1 && !isOccupied(p1character + 1))
                p1character++;
            if (InputManager.P1UpButton() && p1character > 3 && !isOccupied(p1character - 4))
                p1character -= 4;
            if (InputManager.P1DownButton() && p1character < characterNum - 4 && !isOccupied(p1character + 4))
                p1character += 4;
            p1.transform.position = characterList[p1character].transform.position;
            if (InputManager.P1AButton())
            {
                p1Choosing = false;
                PlayerPrefs.SetInt("p1", p1character);
                Debug.Log(p1character);
            }
        }

        if (p2Choosing)
        {
            if (InputManager.P2LeftButton() && p2character > 0 && !isOccupied(p2character - 1))
                p2character--;
            if (InputManager.P2RightButton() && p2character < characterNum - 1 && !isOccupied(p2character + 1))
                p2character++;
            if (InputManager.P2UpButton() && p2character > 3 && !isOccupied(p2character - 4))
                p2character -= 4;
            if (InputManager.P2DownButton() && p2character < characterNum - 4 && !isOccupied(p2character + 4))
                p2character += 4;
            p2.transform.position = characterList[p2character].transform.position;
            if (InputManager.P2AButton())
            {
                p2Choosing = false;
                PlayerPrefs.SetInt("p2", p2character);
                Debug.Log(p2character);
            }
        }

        if (p3Choosing)
        {
            if (InputManager.P3LeftButton() && p3character > 0 && !isOccupied(p3character - 1))
                p3character--;
            if (InputManager.P3RightButton() && p3character < characterNum - 1 && !isOccupied(p3character + 1))
                p3character++;
            if (InputManager.P3UpButton() && p3character > 3 && !isOccupied(p3character - 4))
                p3character -= 4;
            if (InputManager.P3DownButton() && p3character < characterNum - 4 && !isOccupied(p3character + 4))
                p3character += 4;
            p3.transform.position = characterList[p3character].transform.position;
            if (InputManager.P3AButton())
            {
                p3Choosing = false;
                PlayerPrefs.SetInt("p3", p3character);
                Debug.Log(p3character);
            }
        }

        if (p4Choosing)
        {
            if (InputManager.P4LeftButton() && p4character > 0 && !isOccupied(p4character - 1))
                p4character--;
            if (InputManager.P4RightButton() && p4character < characterNum - 1 && !isOccupied(p4character + 1))
                p4character++;
            if (InputManager.P4UpButton() && p4character > 3 && !isOccupied(p4character - 4))
                p4character -= 4;
            if (InputManager.P4DownButton() && p4character < characterNum - 4 && !isOccupied(p4character + 4))
                p4character += 4;
            p4.transform.position = characterList[p4character].transform.position;
            if (InputManager.P4AButton())
            {
                p4Choosing = false;
                PlayerPrefs.SetInt("p4", p4character);
                Debug.Log(p4character);
            }
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
            if (moveToNextScene == false && leftTime <= 0.0f)
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
				SceneManager.LoadScene("TestCharacterSelect");
            }
        }
    }
}
