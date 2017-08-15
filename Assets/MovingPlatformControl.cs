using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovingPlatformControl : MonoBehaviour {
    
    [System.Serializable]
    public class MovingPlatform
    {
        public GameObject platform;
        public Vector3 startPosition;
        public Vector3 destPosition;

        public bool isLooping;
        public bool ismovingBnF;
        public float speed;
        public float respawnTime;

        public void MakePlatform()
        {
            GameObject newPlatform = Instantiate(platform);
            platform = newPlatform;
            platform.transform.position = startPosition;
        }
        
        public void Move()
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, destPosition, speed * Time.deltaTime);

            if((destPosition - platform.transform.position).magnitude < 0.1f && isLooping)
            {
                if (!ismovingBnF)
                {
                    platform.transform.position = startPosition;
                }
                else
                {
                    Vector3 temp = destPosition;
                    destPosition = startPosition;
                    startPosition = temp;
                }
            }
            else if((destPosition - platform.transform.position).magnitude < 0.1f && !isLooping)
            {
                Destroy(platform);
            }
        }
    }

    public MovingPlatform[] newPlatform;

	void Start ()
    {
        for (int i = 0; i < newPlatform.Length; i++)
        {
            newPlatform[i].MakePlatform();
        }
    }
	
	void Update ()
    {
        for (int i = 0; i < newPlatform.Length; i++)
        {
            newPlatform[i].Move();
        }
    }
}
