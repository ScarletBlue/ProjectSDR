using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAttack : MonoBehaviour {

    Animator anim;
    public AudioSource CatAttackSource;
    public AudioSource CatSkillSource;
    public AudioSource CatUtimateSource;

    public AudioClip CatAttackClip;
    public AudioClip CatSkillClip;
    public AudioClip CatUtimateClip;

    public KeyCode melee;
    public KeyCode skill;
    public KeyCode ultimate;

    public GameObject meleeCheck;
    public GameObject skillObject;
    public Transform skillPos;
    public GameObject ultimateCollider;
    public float meleeAttackDelay = 0.3f;
    public Transform catUltimatePos1;
    public Transform catUltimatePos2;
    public GameObject ultimateObject;

    ParticleSystem ultimateParticle;
    public float meleeAtaackResetTime = 0.7f;
    public float dashAttackTime = 0.7f;
    public float skillCastingTime = 0.7f;
    public float coolTime = 6f;

    float skillCoolTime = 6f;
    bool isCasting = false;
    float ultimateGauge = 1000;
    public float UltimateGauge { get { return Mathf.Min(1000, ultimateGauge); } set { ultimateGauge = value; } }
    float speedTemp;
    bool isSkillShooting;

    GameObject newSkill;
    GameObject newUltimate1;
    GameObject newUltimate2;

    void Start ()
    {
        speedTemp = GetComponent<CharacterControll>().moveSpeed;
        ultimateParticle = GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
        CatAttackSource.clip = CatAttackClip;
        CatSkillSource.clip = CatSkillClip;
        CatUtimateSource.clip = CatUtimateClip;

    }
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<CharacterControll>().enabled)
        {
            skillCoolTime += Time.deltaTime;
            if (UltimateGauge == 1000 && !ultimateParticle.isPlaying)
            {
                ultimateParticle.Play();
            }
            else if (UltimateGauge < 1000)
            {
                ultimateParticle.Stop();
            }

            if (Input.GetKeyDown(melee))
            {
                Melee(100f, 7, new Vector2(transform.localScale.x, 0));
                anim.Play("Cat_Melee");
                CatAttackSource.Play();
            }
            if (Input.GetKeyDown(skill) && skillCoolTime > coolTime && GetComponent<CharacterControll>().IsOnFloor())
            {
                skillCoolTime = 0;
                StartCoroutine(SkillCasting());
                anim.Play("Cat_Skill");
                CatSkillSource.Play();
            }
            if (Input.GetKeyDown(ultimate) && UltimateGauge == 1000)
            {
                StartCoroutine(UltimateCastingDelay());
                anim.Play("Cat_Ultimate");
                CatUtimateSource.Play();
            }

            if (isCasting)
            {
                GetComponent<CharacterControll>().moveSpeed = 0;
                GetComponent<CharacterControll>().isCasting = true;
                if (GetComponent<CharacterControll>().hit)
                {
                    CancelCasting();
                    StopAllCoroutines();
                }
            }
            else
            {
                GetComponent<CharacterControll>().moveSpeed = speedTemp;
                GetComponent<CharacterControll>().isCasting = false;
            }
        }
    }

    IEnumerator UltimateCastingDelay()
    {
        yield return new WaitForSeconds(0.15f);
        Ultimate();
    }

    void Ultimate()
    {
        UltimateGauge = 0;
        newUltimate1 = Instantiate(ultimateObject, catUltimatePos1.position, Quaternion.identity);
        newUltimate2 = Instantiate(ultimateObject, catUltimatePos2.position, Quaternion.identity);
        if (this.transform.localScale.x <= 0)
        {
            newUltimate1.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (this.transform.localScale.x >0)
        {
            newUltimate2.transform.localScale = new Vector3(-1, 1, 1);
        }

        if (ultimateCollider.GetComponent<MeleeCheck>().playersInRange.Count != 0)
        {
            foreach (GameObject player in ultimateCollider.GetComponent<MeleeCheck>().playersInRange)
            {
                int direction = (int)((player.transform.position.x - transform.position.x) / Mathf.Abs(player.transform.position.x - transform.position.x));
                player.GetComponent<CharacterControll>().Hit(250f, 20, new Vector2(direction, 0));
            }
        }

        StartCoroutine(UltimateAnimation());
    }

    void Melee(float damage, int attack, Vector2 knockBackDirection)
    {
        if (meleeCheck.GetComponent<MeleeCheck>().playersInRange.Count != 0)
        {
            foreach (GameObject player in meleeCheck.GetComponent<MeleeCheck>().playersInRange)
            {
                UltimateGauge += damage;
                player.GetComponent<CharacterControll>().Hit(damage, attack, knockBackDirection);
            }
        }
    }

    void CancelCasting()
    {
        if (isCasting)
        {
            if (newSkill != null)
            {
                Destroy(newSkill);
            }
            isCasting = false;
        }
    }

    void Skill()
    {
        
        foreach (var player in newSkill.GetComponent<MeleeCheck>().playersInRange)
        {
            player.GetComponent<CharacterControll>().hit = true;
            player.GetComponent<CharacterControll>().Hit(150f, 10, new Vector2(transform.localScale.x,0));
        }
    }

    IEnumerator SkillCasting()
    {
        isCasting = true;
        yield return new WaitForSeconds(skillCastingTime);
        newSkill = Instantiate(skillObject, skillPos.position, Quaternion.identity, this.transform);
        yield return new WaitForSeconds(0.1f);
        Skill();
        yield return new WaitForSeconds(0.9f);
        isCasting = false;
        if (newSkill != null)
        {
            Destroy(newSkill);
        }
    }

    IEnumerator UltimateAnimation()
    {
        yield return new WaitForSeconds(1f);

        if (newUltimate1 != null && newUltimate2 != null)
        {
            Destroy(newUltimate1);
            Destroy(newUltimate2);
        } 
        
    }
}
