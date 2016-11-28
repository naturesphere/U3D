using UnityEngine;
using System.Collections;

public class DoorframeSound : MonoBehaviour {

    public AudioClip boomSound;

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "ball")
        {
            GetComponent<AudioSource>().PlayOneShot(boomSound);
        
        }
    }
}
