using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSysyem : MonoBehaviour {

    GameManager gameManager;

    public int healthPoint;
    public float victoryPoint;
    public float ultimatePoint;
    public int healthPointMax;
    public float victoryPointMax;
    public float ultimatePointMax;

    void Start () {
        healthPointMax = 1000;
        victoryPointMax = 100;
        ultimatePointMax = 100;
        healthPoint = 100;
        victoryPoint = 0;
        ultimatePoint = 0;
	}
	
	void Update () {
        if (victoryPoint >= 100)
        {
            gameManager.isVictory = true;
            //gameManeger에있는 승리 효과 발생
        }

        if (ultimatePoint >= 100)
        {
            ultimatePoint = 100;
        }

        if (healthPoint <= 0)
        {
            healthPoint = 0;
            //this.Die();
        }
	}
}
