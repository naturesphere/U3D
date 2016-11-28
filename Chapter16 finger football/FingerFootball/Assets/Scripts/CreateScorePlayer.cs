using UnityEngine;
using System.Collections;

public class CreateScorePlayer : MonoBehaviour {

    public GameObject disscorePlane;

    void OnCollisionStay (Collision collision) {
        if (Staticer.STATUS != Staticer.NORMAL) {
            return;
        }
        if (collision.gameObject.tag == "ball") {
            Instantiate(disscorePlane, collision.gameObject.transform.position, new Quaternion(-0.7f, 0.0f, 0.0f, 0.7f));
            Staticer.STATUS = Staticer.SAVED;
        }
    }
}
