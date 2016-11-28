using UnityEngine;
using System.Collections;

public class CreateScorePlane : MonoBehaviour {

    public GameObject scorePlane;
    public GameObject disscorePlane;
    private bool breakFlag;

    void Start() {
        Debug.Log(this.transform.name);
    }

    void OnCollisionStay(Collision collision) {
        if (breakFlag) {
            return;
        }
        breakFlag = true;
        if (collision.gameObject.tag == "Player") {
            Instantiate(disscorePlane, transform.position, new Quaternion(-0.7f, 0.0f, 0.0f, 0.7f));        //在撞击的位置克隆相应预制件
        }
        else if (collision.gameObject.tag == "GoalBoard")
        {
            Instantiate(scorePlane, transform.position, new Quaternion(-0.7f, 0.0f, 0.0f, 0.7f));        //在撞击的位置克隆相应预制件
        }
        else {
            breakFlag = false;
        }

    }
}
