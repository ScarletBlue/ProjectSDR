using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelonPixieAttack : MonoBehaviour {

    enum Attack { MP_melee = 5}

    Character character;

    Animator anim;

    public KeyCode melee;
    public KeyCode skill;
    public KeyCode ultimate;

    public GameObject meleeAttack;

    ParticleSystem ultimateParticle;
    public float meleeAttackDelay = 0.3f;
    public float meleeAtaackResetTime = 0.7f;
    public float dashAttackTime = 0.7f;
    
    float ultimateGauge;
    public float UltimateGauge { get { return Mathf.Min(1000, ultimateGauge); } set { ultimateGauge = value; } }

    void Start ()
    {
        character = GetComponent<CharacterController>().character;
        ultimateParticle = GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (UltimateGauge == 1000 && !ultimateParticle.isPlaying)
        {
            ultimateParticle.Play();
        }

        if(Input.GetKeyDown(melee))
        {
            Melee(100f, (int)Attack.MP_melee, new Vector2(transform.localScale.x, 0));
            anim.Play("MP_Melee");
        }
    }

    void Melee(float damage, int attack, Vector2 knockBackDirection)
    {
        if (meleeAttack.GetComponent<MeleeCheck>().playersInRange.Count != 0)
        {
            foreach (GameObject player in meleeAttack.GetComponent<MeleeCheck>().playersInRange)
            {
                UltimateGauge += damage;
                player.GetComponent<CharacterController>().Hit(damage, attack, knockBackDirection);
            }
        }
    }
}
