using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTileController : MonoBehaviour
{
    public float Speed;
    public float movingDistance;
    private float originY;
    private void Start()
    {
        originY = transform.position.y;   
    }
    void Update()
    {
        transform.Translate(0, Speed , 0);

        if (transform.position.y -originY > movingDistance)
        {
            Destroy(this.gameObject);
        }
    }
}