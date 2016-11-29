using UnityEngine;
using System.Collections;

public class BallParticle : MonoBehaviour
{
    public static GameObject ball;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (ball == null)
            Destroy(gameObject);
        else
            transform.position = ball.transform.position;
	}
}
