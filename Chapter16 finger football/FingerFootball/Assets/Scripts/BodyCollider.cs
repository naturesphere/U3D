using UnityEngine;
using System.Collections;

public class BodyCollider : MonoBehaviour {

    private float Xspace;
    private float Yspack;
    private bool isCalculate;
    public static bool breakFlag;

	// Use this for initialization
	void Start () {
        Debug.Log(transform.name);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Staticer.isPlay || breakFlag) {
            breakFlag = true;
            if (!isCalculate) {
                float temp = Staticer.timeToGoal / Time.deltaTime;
                Xspace = (Staticer.goalEndX - transform.position.x) / temp;
                Yspack = (Staticer.goalEndY - transform.position.y) / temp;
                this.GetComponent<Rigidbody>().velocity = Vector3.right;
            }
        }
        this.transform.position = Staticer.endPoint;
	}
}
