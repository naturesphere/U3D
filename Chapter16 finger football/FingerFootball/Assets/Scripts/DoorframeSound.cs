using UnityEngine;
using System.Collections;

public class DoorframeSound : MonoBehaviour
{
	public AudioClip boomSound;
	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag=="ball")
			GetComponent<AudioSource>().PlayOneShot(boomSound);
	}
}
