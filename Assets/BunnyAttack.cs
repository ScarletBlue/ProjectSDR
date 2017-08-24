using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyAttack : MonoBehaviour {

    Character character;

    Animator anim;
    public AudioSource bunnySkillTeleportSource;
    public AudioSource bunnyMeleeAttackSource;
    public AudioClip bunnySkillTeleporitClip;
    public AudioClip bunnyMeleeAttackclip;

    public KeyCode melee;
    public KeyCode skill;
    public KeyCode ultimate;

    public GameObject meleeAttack;
    public GameObject jeondoBunny;
    public GameObject carrot;
    public Sprite UltimateSprite;

    ParticleSystem ultimateParticle;
    public float meleeAttackDelay = 0.3f;
    public float meleeAtaackResetTime = 0.7f;
    public float castingTime = 1f;
    public float coolTime = 6f;

    float meleeDamage = 100f;
    bool isUltimate = false;

    float castingDelay = 6f;

    GameObject newJeondoBunny;

    bool isCasting = false;
    float ultimateGauge = 1000;
    public float UltimateGauge { get { return Mathf.Min(1000, ultimateGauge); } set { ultimateGauge = value; } }
    float speedTemp;

    void Start()
    {
        speedTemp = GetComponent<CharacterControll>().moveSpeed;
        character = GetComponent<CharacterControll>().character;
        ultimateParticle = GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
        bunnySkillTeleportSource.clip = bunnySkillTeleporitClip;
        bunnyMeleeAttackSource.clip = bunnyMeleeAttackclip;
    }

    // Update is called once per frame
    void Update()
    {
        if(isUltimate)
        {
            UltimateGauge = 0;
        }
        if (GetComponent<CharacterControll>().enabled)
        {
            castingDelay += Time.deltaTime;
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
                Melee(meleeDamage, 5, new Vector2(transform.localScale.x, 0));
                anim.SetTrigger("melee");
                bunnyMeleeAttackSource.Play();
            }

            if (Input.GetKeyDown(skill) && castingDelay > coolTime && GetComponent<CharacterControll>().IsOnFloor() && !isUltimate)
            {
                StartCoroutine(Skill());
                StartCoroutine(Casting());
                bunnySkillTeleportSource.Play();
                castingDelay = 0f;

            }

            if (Input.GetKeyDown(ultimate) && UltimateGauge == 1000 && GetComponent<CharacterControll>().IsOnFloor())
            {
                StartCoroutine(Ultimate());
            }

            if (isCasting)
            {
                GetComponent<CharacterControll>().moveSpeed = 0;
                if (GetComponent<CharacterControll>().hit)
                {
                    CancelCasting();
                    StopAllCoroutines();
                    if(newJeondoBunny != null)
                    {
                        Destroy(newJeondoBunny);
                    }
                }
            }
            else
            {
                GetComponent<CharacterControll>().moveSpeed = speedTemp;
            }

        }

        updateUltGauge();
    }

    void updateUltGauge()
    {
        gameObject.GetComponent<CharacterControll>().ult = UltimateGauge;
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

    IEnumerator Skill()
    {
        newJeondoBunny = Instantiate(jeondoBunny, transform.position - new Vector3(1.5f, 0, 0) * transform.localScale.x, Quaternion.identity, transform);
        isCasting = true;
        GetComponent<CharacterControll>().isCasting = true;
        yield return new WaitForSeconds(0.25f);
        Instantiate(carrot, transform.position - new Vector3(1, 0, 0)*transform.localScale.x, Quaternion.identity, transform).GetComponent<Carrot>().BA = this;
        yield return new WaitForSeconds(0.25f);
        Instantiate(carrot, transform.position - new Vector3(1, 0, 0) * transform.localScale.x, Quaternion.identity, transform).GetComponent<Carrot>().BA = this;
        yield return new WaitForSeconds(0.25f);
        Instantiate(carrot, transform.position - new Vector3(1, 0, 0) * transform.localScale.x, Quaternion.identity, transform).GetComponent<Carrot>().BA = this;
        yield return new WaitForSeconds(0.25f);
        GetComponent<CharacterControll>().isCasting = false;
        isCasting = false;
        Destroy(newJeondoBunny);
    }

    IEnumerator Ultimate()
    {
        UltimateGauge = 0;
        anim.SetBool("Ultimate", true);
        Sprite tempSprite = GetComponent<SpriteRenderer>().sprite;
        GetComponent<SpriteRenderer>().sprite = UltimateSprite;
        isUltimate = true;
        float tempSpeed = GetComponent<CharacterControll>().moveSpeed;
        GetComponent<CharacterControll>().moveSpeed *= 1.7f;
        meleeDamage = 200f;
        anim.SetBool("isUWalking", true);
        yield return new WaitForSeconds(6f);
        anim.SetBool("Ultimate", false);
        GetComponent<SpriteRenderer>().sprite = tempSprite;
        isUltimate = false;
        GetComponent<CharacterControll>().moveSpeed = tempSpeed;
        meleeDamage = 100f;
        anim.SetBool("isUWalking", false);
    }

    public void CancelCasting()
    {
        if (isCasting)
        {
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
    
}
