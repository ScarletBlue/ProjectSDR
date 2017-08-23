using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KimAttack : MonoBehaviour {
    enum Attack { Kim_melee = 5, kim_melee_up = 20, kim_dash = 10, kim_mine = 15 }
    Character character;

    Animator anim;

    public AudioSource KimMeleeAttackSource;
    public AudioSource KimDashAttackSource;
    public AudioSource KimMineSetSource;
    public AudioSource KimUtimateCastingSource;

    public AudioClip KimMeleeAttackClip;
    public AudioClip KimDashAttackClip;
    public AudioClip KimMineSetClip;
    public AudioClip KimutimateCastingClip;


    public KeyCode melee;
    public KeyCode skill;
    public KeyCode ultimate;

    public GameObject meleeAttack;
    public GameObject mine;
    public Transform minePosition;
    public GameObject ultimateTarget;
    public GameObject TargetSprite;
    public GameObject missile;

	[SerializeField]
    ParticleSystem ultimateParticle;
    public float meleeAttackDelay = 0.3f;
    public float meleeAtaackResetTime = 0.7f;
    public float dashAttackTime = 0.7f;
    public float skillCoolTime = 8f;

    CharacterControll CC;
    float speedTemp;
    bool isCastingUltimate = false;
    float skillDelay = 8f;
    int kimMeleeStage = 0;
    float kimMeleeDelay = 0;
    bool dashAttack = false;
    float dashAttackDelay = 0f;
    GameObject newUltimateTarget;

    float ultimateGauge = 1000;
    public float UltimateGauge { get { return Mathf.Min(1000, ultimateGauge); } set { ultimateGauge = value; } }

    void Start()
    {
        CC = GetComponent<CharacterControll>();
        speedTemp = CC.moveSpeed;
        character = CC.character;
        ultimateParticle = GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();

        KimMeleeAttackSource.clip = KimMeleeAttackClip;
        KimDashAttackSource.clip = KimDashAttackClip;
        KimMineSetSource.clip = KimMineSetClip;
        KimUtimateCastingSource.clip = KimutimateCastingClip;
       
    }

    void Update()
    {
        if (GetComponent<CharacterControll>().enabled)
        {
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
                KimMelee();
            }
            if (Input.GetKeyDown(skill) && skillDelay > skillCoolTime)
            {
                if (CC.IsOnFloor())
                {
                    Debug.Log("mine");
                    Mine(200f, Attack.kim_mine);
                    anim.SetTrigger("mine");
                    skillDelay = 0f;
                }
            }
            if (UltimateGauge == 1000 && Input.GetKeyDown(ultimate))
            {
                StartCoroutine(UltimateCasting());
                newUltimateTarget = Instantiate(ultimateTarget);
                newUltimateTarget.GetComponent<KimUltimateTarget>().CC = CC;
                newUltimateTarget.GetComponent<KimUltimateTarget>().Target = TargetSprite;
                newUltimateTarget.GetComponent<KimUltimateTarget>().missile = missile;
                anim.SetTrigger("ultimate");
                KimUtimateCastingSource.Play();
            }

            if (isCastingUltimate)
            {
                CC.moveSpeed = 0;
                CC.canJump = false;
                if (GetComponent<CharacterControll>().hit)
                {
                    CancelUltimate();
                    UltimateGauge = 0;
                    isCastingUltimate = false;
                }
            }
            else
            {
                CC.canJump = true;
                CC.moveSpeed = speedTemp;
            }
            kimMeleeDelay += Time.deltaTime;
            skillDelay += Time.deltaTime;

            if (kimMeleeDelay >= meleeAtaackResetTime)
            {
                kimMeleeStage = 0;
            }
            if (dashAttack && dashAttackDelay <= dashAttackTime)
            {
                dashAttackDelay += Time.deltaTime;
                DashMelee(100f, (int)Attack.kim_dash, new Vector2(transform.localScale.x, 0));
            }
            else if (dashAttack && dashAttackDelay >= dashAttackTime)
            {
                dashAttackDelay = 0f;
                dashAttack = false;
                CC.isDashing = false;
            }


            if (CC.hp == 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Kim_Death"))
            {
                anim.Play("Kim_Death");

            }

        }
    }


    void CancelUltimate()
    {
        newUltimateTarget.GetComponent<KimUltimateTarget>().DestroyByHit();
        Destroy(newUltimateTarget);
    }

    IEnumerator UltimateCasting()
    {
        isCastingUltimate = true;
        yield return new WaitForSeconds(2f);
        if(isCastingUltimate)
        {
            isCastingUltimate = false;
            UltimateGauge = 0;
        }
    }

    void KimMelee()
    {
        if (CC.isDashing)
        {
            dashAttack = true;
            dashAttackDelay = 0;
            anim.SetTrigger("dashMelee");
            KimDashAttackSource.Play();
        }
        else if (kimMeleeStage == 0 && kimMeleeDelay >= meleeAttackDelay)
        {
            anim.Play("Kim_Melee1");
            Melee(50f, (int)Attack.Kim_melee, new Vector2(transform.localScale.x, 0));
            kimMeleeStage++;
            kimMeleeDelay = 0;
            KimMeleeAttackSource.Play();
        }

        else if (kimMeleeStage == 1 && kimMeleeDelay >= meleeAttackDelay)
        {
            anim.Play("Kim_Melee2");
            Melee(50f, (int)Attack.Kim_melee, new Vector2(transform.localScale.x, 0));
            kimMeleeStage++;
            kimMeleeDelay = 0;
            KimMeleeAttackSource.Play();
        }

        else if (kimMeleeStage == 2 && kimMeleeDelay >= meleeAttackDelay)
        {
            anim.Play("Kim_Melee3");
            Melee(50f, (int)Attack.Kim_melee, new Vector2(transform.localScale.x, 0));
            kimMeleeStage++;
            kimMeleeDelay = 0;
            KimMeleeAttackSource.Play();
        }
        else if (kimMeleeStage == 3 && kimMeleeDelay >= meleeAttackDelay)
        {
            anim.Play("Kim_Melee4");
            Melee(100f, (int)Attack.kim_melee_up, new Vector2(transform.localScale.x, 1).normalized);
            kimMeleeStage = 0;
            kimMeleeDelay = 0;
            KimMeleeAttackSource.Play();
        }
    }

    void DashMelee(float damage, int attack, Vector2 knockBackDirection)
    {
        if (meleeAttack.GetComponent<MeleeCheck>().playersInRange.Count != 0)
        {
            foreach (GameObject player in meleeAttack.GetComponent<MeleeCheck>().playersInRange)
            {
                UltimateGauge += damage;
                player.GetComponent<CharacterControll>().Hit(damage, attack, knockBackDirection);
                dashAttack = false;
                CC.isDashing = false;
            }
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

    void Mine(float damage, Attack attack)
    {
        GameObject newMine = Instantiate(mine, minePosition.position, new Quaternion());
        newMine.GetComponent<Mine>().kim = this;
        newMine.GetComponent<Mine>().damage = damage;
        newMine.GetComponent<Mine>().attack = (int)Attack.kim_mine;
    }
}
