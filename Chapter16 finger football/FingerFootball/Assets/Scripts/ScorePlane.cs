using UnityEngine;
using System.Collections;

public class ScorePlane : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if(Staticer.STATUS==Staticer.NORMAL)
			Destroy(gameObject);
		transform.localScale=new Vector3
		(
		Mathf.MoveTowards(transform.localScale.x,0.04f,Time.deltaTime*0.05f),
		transform.localScale.y,
		Mathf.MoveTowards(transform.localScale.z,0.04f,Time.deltaTime*0.05f)
		);
	}
}
