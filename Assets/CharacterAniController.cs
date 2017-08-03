using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAniController : MonoBehaviour {

    enum States {idle, walk, jump, fall, attack, hit}

    Animator anim;

    States currentState = States.idle;
    
	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {

	}
}
