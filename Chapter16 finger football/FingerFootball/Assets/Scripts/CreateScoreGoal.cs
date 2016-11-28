using UnityEngine;
using System.Collections;

public class CreateScoreGoal : MonoBehaviour {

    public GameObject scorePlane;
    public AudioClip sound;

    void OnTriggerEnter(Collider collider) {
        if (Staticer.STATUS != Staticer.NORMAL) {
            return;
        }
        Vector2 ballPosition = new Vector2(GameObject.FindWithTag("ball").transform.position.x, GameObject.FindWithTag("ball").transform.position.y);
        if (collider.gameObject.tag == "ball") {
            float lengthToCenterPow = Mathf.Pow(ballPosition.x - Staticer.goalX, 2) + Mathf.Pow(ballPosition.y - Staticer.goalY, 2);  //计算球与靶心的距离的平方
            if (lengthToCenterPow <= 0.058) {               //进行计分
                Staticer.STATUS = Staticer.THREEPOINT;
            }
            else if (lengthToCenterPow <= 0.23f) {
                Staticer.STATUS = Staticer.TWOPOINT;
            }
            else if (lengthToCenterPow <= 0.51f)
            {
                Staticer.STATUS = Staticer.ONEPOINT;
            }
            else {
                Staticer.STATUS = Staticer.GOAL;
            }
            Instantiate(scorePlane, collider.transform.position, new Quaternion(-0.7f, 0.0f, 0.0f, 0.7f));
            GetComponent<AudioSource>().volume = Staticer.valueSound;
            GetComponent<AudioSource>().PlayOneShot(sound);
            collider.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 1);
        }
    }
}
