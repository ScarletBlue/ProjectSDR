using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour {

    Character character;

    public GameObject meleeAttack;

	void Start ()
    {
        character = GetComponent<CharacterController>().character;
	}
	
	void Update ()
    {
		if(Input.GetKeyDown("left ctrl"))
        {
            switch(character)
            {
                case Character.KimJongUn:
                    break;

                case Character.Kurisu:
                    break;

                case Character.MelonPixie:
                    break;
            }
        }
        if(Input.GetKeyDown("left alt"))
        {
            switch (character)
            {
                case Character.KimJongUn:
                    break;

                case Character.Kurisu:
                    break;

                case Character.MelonPixie:
                    break;
            }
        }
	}
    
}
