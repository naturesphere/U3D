﻿using UnityEngine;
using System.Collections;

public class CreatScorePlayer : MonoBehaviour
{
    public GameObject disscorePlane;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnCollisionStay(Collision collision)
    {
        if (Staticer.STATUS != Staticer.NORMAL)
            return;
        if(collision.gameObject.tag=="ball")
        {
            Instantiate(disscorePlane, collision.gameObject.transform.position, 
                new Quaternion(-0.7f, 0.0f, 0.0f, 0.7f));
            Staticer.STATUS = Staticer.SAVED;
        }
    }
}
