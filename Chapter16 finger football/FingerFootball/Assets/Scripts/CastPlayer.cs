using UnityEngine;
using System.Collections;

public class CastPlayer : MonoBehaviour {

    private bool castBool;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!castBool && Staticer.isSave) {
            castBool = true;
            float vX = (Staticer.goalEndX - transform.position.x) / Staticer.timeToGoal;
            float vY = (Staticer.goalEndY - transform.position.y) / Staticer.timeToGoal - 0.5f * 9.8f * Staticer.timeToGoal;
            GetComponent<Rigidbody>().velocity = new Vector3(vX * 0.5f, vY * 0.5f, 0);
        }
	}
}
