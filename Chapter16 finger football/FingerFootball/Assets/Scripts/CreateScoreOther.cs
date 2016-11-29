using UnityEngine;
using System.Collections;

public class CreateScoreOther : MonoBehaviour
{

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
        if (Staticer.STATUS != Staticer.NORMAL)
            return;
        if (collider.gameObject.tag == "ball")
            Staticer.STATUS = Staticer.MISS;
    }
}
