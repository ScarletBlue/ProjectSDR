using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimUltimateTarget : MonoBehaviour {

    public CharacterControll CC;

    public GameObject Target;
    public GameObject missile;

    GameObject newTarget;
    GameObject newMissile;
    int targetFloor = 1;
    int hitFloor = 0;

    RaycastHit2D hitBase;
    RaycastHit2D hitF1;
    RaycastHit2D hitF2;
    RaycastHit2D hitf3;
    RaycastHit2D hitf4;
    
    //List<RaycastHit2D> hitList;
    Dictionary<int, RaycastHit2D> hitDic;
    int maxInt = 0;
    int minInt = 0;
    int downInt = 0;

    bool isControllerable = true;

    void Start ()
    {
        newTarget = Instantiate(Target);
        hitDic = new Dictionary<int, RaycastHit2D>();
        StartCoroutine(Hit());
	}
	
	void Update ()
    {
        hitDic.Clear();
        Vector3 tempPos = transform.position;
        if (Input.GetKey(CC.key_right) && isControllerable)
        {
            transform.Translate(20 * Time.deltaTime, 0, 0);
            transform.position = new Vector3(Mathf.Min(transform.position.x, 21f), transform.position.y, transform.position.z);
        }
        if (Input.GetKey(CC.key_left) && isControllerable)
        {
            transform.Translate(-20 * Time.deltaTime, 0, 0);
            transform.position = new Vector3(Mathf.Max(transform.position.x, -13f), transform.position.y, transform.position.z);
        }
        hitBase = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("base"));
        hitF1 = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("f1"));
        hitF2 = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("f2"));
        hitf3 = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("f3"));
        hitf4 = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("f4"));

        if(hitBase)
        {
            //hitList.Insert(0,hitBase);
            hitDic.Add(0,hitBase);
        }
        if(hitF1)
        {
            //hitList.Insert(1, hitF1);
            hitDic.Add(1,hitF1);
        }
        if(hitF2)
        {
            //hitList.Insert(2,hitF2);
            hitDic.Add(2,hitF2);
        }
        if (hitf3)
        {
            //hitList.Insert(3, hitf3);
            hitDic.Add(3,hitf3);
        }
        if(hitf4)
        {
            //hitList.Insert(4,hitf4);
            hitDic.Add(4,hitf4);
        }

        for(int i = 0; i <= 4; i++)
        {
            if(hitDic.ContainsKey(i))
            {
                maxInt = i;
            }
        }

        for(int i = 4; i >= 0; i--)
        {
            if(hitDic.ContainsKey(i))
            {
                minInt = i;
            }
        }
        for(int i = 0; i <= targetFloor; i++)
        {
            if(hitDic.ContainsKey(i))
            {
                downInt = i;
            }
        }
        if(downInt < targetFloor)
        {
            targetFloor = Mathf.Max(0,downInt);
        }
        if(minInt > targetFloor)
        {
            transform.position = tempPos;
            return;
        }


        if(Input.GetKeyDown(CC.key_up) && isControllerable)
        {
            targetFloor++;
            targetFloor = Mathf.Clamp(targetFloor, minInt, maxInt);
            if(!hitDic.ContainsKey(targetFloor))
            {
                findMax();
            }
        }
        if(Input.GetKeyDown(CC.key_down) && isControllerable)
        {
            targetFloor--;
            targetFloor = Mathf.Clamp(targetFloor, minInt, maxInt);
            if(!hitDic.ContainsKey(targetFloor))
            {
                findMin();
            }
        }
        newTarget.transform.position = hitDic[targetFloor].point;
        if (newMissile != null && (newMissile.transform.position - newTarget.transform.position).magnitude < 1f)
        {
            newMissile.GetComponent<Missile>().Destroy();
            Destroy(newTarget);
            Destroy(gameObject);
        }
    }

    void findMax()
    {
        targetFloor++;
        if(hitDic.ContainsKey(targetFloor) || targetFloor == maxInt)
        {
            return;
        }
        else
        {
            findMax();
        }
    }
    void findMin()
    {
        targetFloor--;
        if(hitDic.ContainsKey(targetFloor) || targetFloor == minInt)
        {
            return;
        }
        else
        {
            findMin();
        }
    }

    public void DestroyByHit()
    {
        Destroy(newTarget);
    }

    IEnumerator Hit()
    {
        yield return new WaitForSeconds(2f);
        isControllerable = false;
        newMissile = Instantiate(missile, newTarget.transform.position + new Vector3(50, 50, 0), Quaternion.identity);
        newMissile.transform.eulerAngles = new Vector3(0, 0, 45);
        newMissile.GetComponent<Missile>().CC = CC;
    }
}
