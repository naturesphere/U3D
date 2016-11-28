using UnityEngine;
using System.Collections;

public class CheckBoard : MonoBehaviour {

    public GameObject ball;
    public GameObject player;
	// Use this for initialization
	void Update () {
        if (ball == null)
        {
            ball = GameObject.FindWithTag("ball");

        }
        if (Staticer.isCrash) {
            Staticer.isCrash = false;
            float t = Staticer.distance / ball.GetComponent<Rigidbody>().velocity.z;
            float x = Staticer.crashPoint.x + ball.GetComponent<Rigidbody>().velocity.x * t;
            float y = Staticer.crashPoint.y + ball.GetComponent<Rigidbody>().velocity.y * t - 0.5f * 9.8f * t * t;      //球到达球门时的Y坐标
            y = (y < 0) ? 0 : y;

            Staticer.endPoint = new Vector3(x, y, Staticer.endZ);
            Staticer.timeToGoal = t;
            Staticer.isSave = true;
            /*对球穿过球门时的坐标进行判断*/
            float width = Staticer.goalEndX - Staticer.goalStartX;
            float heigh = Staticer.goalEndY - Staticer.goalStartY;
            if (y >= Staticer.goalStartY + heigh / 2 && y < Staticer.goalEndY) {
                if (x >= Staticer.goalStartX && x <= Staticer.goalStartX + width / 3) {
                    Staticer.isPlay = true;
                    Staticer.animationString = "leftUpCorner";
                }
                else if (x > Staticer.goalStartX + width / 3 && x <= Staticer.goalStartX + width / 2) {
                    Staticer.isPlay = true;
                    Staticer.animationString = "leftUp";
                }
                else if (x > Staticer.goalStartX + width / 2 && x <= Staticer.goalStartX + width / 3 * 2) {
                    Staticer.isPlay = true;
                    Staticer.animationString = "rightUp";
                }
                else if (x > Staticer.goalStartX + width / 3 * 2 && x <= Staticer.goalEndX) {
                    Staticer.isPlay = true;
                    Staticer.animationString = "rightUpCorner";
                }
            }
            else if (y < Staticer.goalStartY + heigh / 2) { 
                if(x >= Staticer.goalStartX && x <= Staticer.goalStartX + width / 5 * 2){
                    Staticer.isPlay = true;
                    Staticer.animationString = "leftDown";
                }
                else if (x > Staticer.goalStartX + width / 5 * 2 && x <= Staticer.goalStartX + width / 5 * 3) {
                    Staticer.isPlay = true;
                    Staticer.animationString = "down";
                }
                else if (x > Staticer.goalStartX + width / 5 * 3 && x <= Staticer.goalEndX) {
                    Staticer.isPlay = true;
                    Staticer.animationString = "rightDown";
                }
            }
            /*对球穿过球门时的坐标进行判断*/
        }
	}

    void OnTriggerEnter() {
        if (!Staticer.isCrash&&ball!=null) {                //足球碰撞到测试板时获取足球的坐标
            Staticer.isCrash = true;

            Staticer.crashPoint = ball.transform.position;
        }
    }
}
