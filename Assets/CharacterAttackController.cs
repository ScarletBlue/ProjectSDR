using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Attack { Kim_melee = 5, kim_melee_up = 20, kim_dash = 10, kim_mine = 15, Pixie_melee}

public class CharacterAttackController : MonoBehaviour {

    Character character;

    Animator anim;

    public GameObject meleeAttack;
    public GameObject mine;
    public Transform minePosition;

    ParticleSystem ultimateParticle;
    public float meleeAttackDelay = 0.3f;
    public float meleeAtaackResetTime = 0.7f;
    public float dashAttackTime = 0.7f;

    public Text ultimateGaugeText; // 테스트용

    int kimMeleeStage = 0;
    float kimMeleeDelay = 0;
    bool dashAttack = false;
    float dashAttackDelay = 0f;

    float ultimateGauge;
    public float UltimateGauge { get { return Mathf.Min(1000,ultimateGauge); } set { ultimateGauge = value; } }

	void Start ()
    {
        character = GetComponent<CharacterController>().character;
        ultimateParticle = GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        if(UltimateGauge == 1000 && !ultimateParticle.isPlaying)
        {
            ultimateParticle.Play();
        }
        if (ultimateGaugeText != null) // 테스트용
        {
            ultimateGaugeText.text = "ultimate : " + UltimateGauge + "/1000";
        }
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
                    if (GetComponent<CharacterController>().IsOnFloor())
                    {
                        //Mine(200f, Attack.kim_mine);
                    }
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
        if(dashAttack && dashAttackDelay <= dashAttackTime)
        {
            dashAttackDelay += Time.deltaTime;
            DashMelee(100f, Attack.kim_dash, new Vector2(transform.localScale.x,0));
        }
        else if(dashAttack && dashAttackDelay >= dashAttackTime)
        {
            dashAttackDelay = 0f;
            dashAttack = false;
            GetComponent<CharacterController>().isDashing = false;
        }
	}

    void KimMelee()
    {
        if(GetComponent<CharacterController>().isDashing)
        {
            dashAttack = true;
            dashAttackDelay = 0;
            anim.SetTrigger("dashMelee");
        }
        else if(kimMeleeStage == 0 && kimMeleeDelay >= meleeAttackDelay)
        {
            anim.Play("Kim_Melee1");
            Melee(50f, Attack.Kim_melee,new Vector2(transform.localScale.x,0));
            kimMeleeStage++;
            kimMeleeDelay = 0;
        }

        else if(kimMeleeStage == 1 && kimMeleeDelay >= meleeAttackDelay)
        {
            anim.Play("Kim_Melee2");
            Melee(50f, Attack.Kim_melee, new Vector2(transform.localScale.x, 0));
            kimMeleeStage++;
            kimMeleeDelay = 0;
        }

        else if(kimMeleeStage ==2 && kimMeleeDelay >= meleeAttackDelay)
        {
            anim.Play("Kim_Melee3");
            Melee(50f, Attack.Kim_melee, new Vector2(transform.localScale.x, 0));
            kimMeleeStage++;
            kimMeleeDelay = 0;
        }
        else if (kimMeleeStage == 3 && kimMeleeDelay >= meleeAttackDelay)
        {
            anim.Play("Kim_Melee4");
            Melee(100f, Attack.kim_melee_up, new Vector2(transform.localScale.x, 1).normalized);
            kimMeleeStage = 0;
            kimMeleeDelay = 0;
        }
    }

    void DashMelee(float damage, Attack attack, Vector2 knockBackDirection)
    {
        if (meleeAttack.GetComponent<MeleeCheck>().playersInRange.Count != 0)
        {
            foreach (GameObject player in meleeAttack.GetComponent<MeleeCheck>().playersInRange)
            {
                UltimateGauge += damage;
                player.GetComponent<CharacterController>().Hit(damage, attack, knockBackDirection);
                dashAttack = false;
                GetComponent<CharacterController>().isDashing = false;
            }
        }
    }

    void Melee(float damage, Attack attack, Vector2 knockBackDirection)
    {
        if(meleeAttack.GetComponent<MeleeCheck>().playersInRange.Count != 0)
        {
            foreach(GameObject player in meleeAttack.GetComponent<MeleeCheck>().playersInRange)
            {
                UltimateGauge += damage;
                player.GetComponent<CharacterController>().Hit(damage, attack, knockBackDirection);
            }
        }
    }
}
