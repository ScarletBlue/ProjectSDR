using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public GameObject UltimatePortal;
    public GameObject UltimateTarget;

    ParticleSystem ultimateParticle;
    public float meleeAttackDelay = 0.3f;
    public float meleeAtaackResetTime = 0.7f;
    public float dashAttackTime = 0.7f;
    public float castingTime = 1f;
    public float coolTime = 6f;

    float castingDelay = 6f;
    bool isDead = false;
    bool isCasting = false;
    float ultimateGauge = 1000;
    public float UltimateGauge { get { return Mathf.Min(1000, ultimateGauge); } set { ultimateGauge = value; } }
    float speedTemp;

    GameObject newPortal;
    GameObject newFireBall;
    GameObject newUltimatePortal;
    GameObject newUltimateTarget;

    void Start ()
    {
        speedTemp = GetComponent<CharacterControll>().moveSpeed;
        character = GetComponent<CharacterControll>().character;
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
        else if(UltimateGauge <1000)
        {
            ultimateParticle.Stop();
        }

        if(Input.GetKeyDown(melee))
        {
            Melee(100f, (int)Attack.MP_melee, new Vector2(transform.localScale.x, 0));
            anim.Play("MP_Melee");
        }

        if(Input.GetKeyDown(skill) && castingDelay > coolTime && GetComponent<CharacterControll>().IsOnFloor())
        {
            FireBall(200f, 8, new Vector3(transform.localScale.x,0,0));
            StartCoroutine(Casting());
            castingDelay = 0f;
        }

        if(Input.GetKeyDown(ultimate) && UltimateGauge == 1000 && GetComponent<CharacterControll>().IsOnFloor())
        {
            Ultimate();
            StartCoroutine(Casting());
        }

        if (isCasting)
        {
            GetComponent<CharacterControll>().moveSpeed = 0;
            if(GetComponent<CharacterControll>().hit)
            {
                CancelCasting();
            }
        }
        else
        {
            GetComponent<CharacterControll>().moveSpeed = speedTemp;
        }

        if (GetComponent<CharacterControll>().hp <= 0 && isDead ==false)
        {
            isDead = true;
            anim.SetTrigger("death");
            CharacterControll characterControll = GetComponent<CharacterControll>();
            GetComponent<Rigidbody2D>().velocity = new Vector2 (0,0);
            characterControll.enabled = false;
           
        }
    }

    

    void Melee(float damage, int attack, Vector2 knockBackDirection)
    {
        if (meleeAttack.GetComponent<MeleeCheck>().playersInRange.Count != 0)
        {
            foreach (GameObject player in meleeAttack.GetComponent<MeleeCheck>().playersInRange)
            {
                UltimateGauge += damage;
                player.GetComponent<CharacterControll>().Hit(damage, attack, knockBackDirection);
            }
        }
    }

    public void CancelCasting()
    {
        if (isCasting)
        {
            if (newPortal != null)
            {
                Destroy(newFireBall);
                Destroy(newPortal);
            }
            if (newUltimatePortal != null)
            {
                Destroy(newUltimatePortal);
                Destroy(newUltimateTarget);
                StopAllCoroutines();
            }
            isCasting = false;
        }
    }

    IEnumerator Casting()
    {
        isCasting = true;
        GetComponent<CharacterControll>().isCasting = true;
        yield return new WaitForSeconds(castingTime);
        GetComponent<CharacterControll>().isCasting = false;
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

    void Ultimate()
    {
        UltimateGauge = 0;
        newUltimatePortal = Instantiate(UltimatePortal, fireBallPos.position, Quaternion.identity, this.transform);
        anim.SetTrigger("ultimate");
        List<CharacterControll> players;
        players = FindObjectsOfType<CharacterControll>().ToList();
        players.Remove(this.GetComponent<CharacterControll>());
        CharacterControll target = players[Random.Range(0, players.Count)];
        newUltimateTarget = Instantiate(UltimateTarget, target.transform.position, Quaternion.identity);
        newUltimateTarget.transform.parent = target.transform;
        StartCoroutine(UltimateHit());
    }

    IEnumerator UltimateHit()
    {
        yield return new WaitForSeconds(1);
        Destroy(newUltimatePortal);
        yield return new WaitForSeconds(2.1f);
        newUltimateTarget.GetComponentInParent<CharacterControll>().Hit(350, 10, new Vector2(-newUltimateTarget.GetComponentInParent<Transform>().transform.localScale.x, 0));
        newUltimateTarget.GetComponentInParent<CharacterControll>().hit = true;
        
        Destroy(newUltimateTarget);
    }
}
