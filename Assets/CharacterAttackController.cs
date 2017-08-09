using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Attack { Kim_melee = 5, kim_melee_up = 20, Pixie_melee}

public class CharacterAttackController : MonoBehaviour {

    Character character;

    Animator anim;

    public GameObject meleeAttack;
    public float meleeAttackDelay = 0.3f;
    public float meleeAtaackResetTime = 0.7f;

    int kimMeleeStage = 0;
    float kimMeleeDelay = 0;

	void Start ()
    {
        character = GetComponent<CharacterController>().character;
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
		if(Input.GetKeyDown("left ctrl"))
        {
            switch(character)
            {
                case Character.KimJongUn:
                    KimMelee();
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
        kimMeleeDelay += Time.deltaTime;
        if(kimMeleeDelay >= meleeAtaackResetTime)
        {
            kimMeleeStage = 0;
        }
	}

    void KimMelee()
    {
        if(kimMeleeStage == 0 && kimMeleeDelay >= meleeAttackDelay)
        {
            anim.Play("Kim_Melee1");
            Melee(5f, Attack.Kim_melee,new Vector2(transform.localScale.x,0));
            kimMeleeStage++;
            kimMeleeDelay = 0;
        }

        else if(kimMeleeStage == 1 && kimMeleeDelay >= meleeAttackDelay)
        {
            anim.Play("Kim_Melee2");
            Melee(5f, Attack.Kim_melee, new Vector2(transform.localScale.x, 0));
            kimMeleeStage++;
            kimMeleeDelay = 0;
        }

        else if(kimMeleeStage ==2 && kimMeleeDelay >= meleeAttackDelay)
        {
            anim.Play("Kim_Melee3");
            Melee(5f, Attack.Kim_melee, new Vector2(transform.localScale.x, 0));
            kimMeleeStage++;
            kimMeleeDelay = 0;
        }
        else if (kimMeleeStage == 3 && kimMeleeDelay >= meleeAttackDelay)
        {
            anim.Play("Kim_Melee4");
            Melee(5f, Attack.kim_melee_up, new Vector2(transform.localScale.x, 1));
            kimMeleeStage = 0;
            kimMeleeDelay = 0;
        }
    }

    void KimDash()
    {

    }

    void Melee(float damage, Attack attack, Vector2 knockBackDirection)
    {
        if(meleeAttack.GetComponent<MeleeCheck>().playersInRange.Count != 0)
        {
            foreach(GameObject player in meleeAttack.GetComponent<MeleeCheck>().playersInRange)
            {
                player.GetComponent<CharacterController>().Hit(damage, attack, knockBackDirection);
            }
        }
    }
}
