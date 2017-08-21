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
    public GameObject fireBall;
    public Transform fireBallPos;
    public GameObject fireBallPortal;

    ParticleSystem ultimateParticle;
    public float meleeAttackDelay = 0.3f;
    public float meleeAtaackResetTime = 0.7f;
    public float dashAttackTime = 0.7f;
    public float castingTime = 1f;
    public float coolTime = 6f;

    float castingDelay = 6f;
    bool isCasting = false;
    float ultimateGauge;
    public float UltimateGauge { get { return Mathf.Min(1000, ultimateGauge); } set { ultimateGauge = value; } }
    float speedTemp;

    GameObject newPortal;
    GameObject newFireBall;

    void Start ()
    {
        speedTemp = GetComponent<CharacterController>().speed;
        character = GetComponent<CharacterController>().character;
        ultimateParticle = GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        castingDelay += Time.deltaTime;
        if (UltimateGauge == 1000 && !ultimateParticle.isPlaying)
        {
            ultimateParticle.Play();
        }

        if(Input.GetKeyDown(melee))
        {
            Melee(100f, (int)Attack.MP_melee, new Vector2(transform.localScale.x, 0));
            anim.Play("MP_Melee");
        }

        if(Input.GetKeyDown(skill) && castingDelay > 6f)
        {
            FireBall(200f, 8, new Vector3(transform.localScale.x,0,0));
            StartCoroutine(Casting());
            castingDelay = 0f;
        }

        if (isCasting)
        {
            GetComponent<CharacterController>().speed = 0;
            if(GetComponent<CharacterController>().hit)
            {
                CancelCasting();
            }
        }
        else
        {
            GetComponent<CharacterController>().speed = speedTemp;
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

    public void CancelCasting()
    {
        if (isCasting)
        {
            Destroy(newFireBall);
            Destroy(newPortal);
        }
    }

    IEnumerator Casting()
    {
        isCasting = true;
        yield return new WaitForSeconds(castingTime);
        isCasting = false;
    }

    void FireBall(float damage, int knockBackConst, Vector3 direction)
    {
        newPortal = Instantiate(fireBallPortal, fireBallPos.position, Quaternion.identity);
        newFireBall = Instantiate(fireBall, fireBallPos.position, Quaternion.identity);
        newFireBall.GetComponent<FireBall>().MPA = this;
        newFireBall.GetComponent<FireBall>().damage = damage;
        newFireBall.GetComponent<FireBall>().attack = knockBackConst;
        newFireBall.GetComponent<FireBall>().direction = direction;
        newFireBall.transform.localScale = new Vector3(-direction.x, 1, 1);
        newFireBall.GetComponent<FireBall>().enabled = false;
    }
}
