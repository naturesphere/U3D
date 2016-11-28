using UnityEngine;
using System.Collections;

public class CreateScoreOther : MonoBehaviour {

    void OnTriggerEnter(Collider collider) {
        if (Staticer.STATUS != Staticer.NORMAL) {
            return;
        }
        if (collider.gameObject.tag == "ball") {
            Staticer.STATUS = Staticer.MISS;

        }
    }
}
