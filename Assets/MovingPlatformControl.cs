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
        public float speed;
        public float respawnTime;
        
        public void Move()
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, destPosition, speed * Time.deltaTime);

            if((destPosition - platform.transform.position).magnitude < 0.1f && isLooping)
            {
                platform.transform.position = startPosition;
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
		
	}
	
	void Update ()
    {
        for (int i = 0; i < newPlatform.Length; i++)
        {
            newPlatform[i].Move();
        }
    }
}
