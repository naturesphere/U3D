using UnityEngine;
using System.Collections;

public class CreatScorePlane : MonoBehaviour
{
	public GameObject scorePlane;
	public GameObject disscorePlane;
	private bool breakFlag;
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
		if(breakFlag)
			return;
		breakFlag=true;
		if(collision.gameObject.tag=="Player")
		{
			Instantiate(disscorePlane,transform.position,new Quaternion(-0.7f,0.0f,0.0f,0.7f));
		}
		else if(collision.gameObject.tag=="GoalBoard")
		{
			Instantiate(scorePlane,transform.position,new Quaternion(-0.7f,0.0f,0.0f,0.7f));
		}
		else
			breakFlag=false;
	}
}
