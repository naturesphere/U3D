using UnityEngine;
using System.Collections;

public class CheckBoard : MonoBehaviour
{
    public GameObject ball;             //足球游戏对象
    public GameObject player;           //守门员游戏对象
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(ball==null)
        {
            ball = GameObject.FindWithTag("ball");
        }
        if(Staticer.isCrash)
        {
            Staticer.isCrash = false;
            float t = Staticer.distance / ball.GetComponent<Rigidbody>().velocity.z;
            float x = Staticer.crashPoint.x + ball.GetComponent<Rigidbody>().velocity.x * t;
            float y = Staticer.crashPoint.y + ball.GetComponent<Rigidbody>().velocity.y * t - 0.5f * 9.8f * t * t;
            y = (y < 0) ? 0 : y;
            Staticer.endPoint = new Vector3(x, y, Staticer.endZ);
            Staticer.timeToGoal = t;
            Staticer.isSave = true;
            float width = Staticer.goalEndX - Staticer.goalStartX;
            float heigh = Staticer.goalEndY - Staticer.goalStartY;
            if (y >= Staticer.goalStartY + heigh / 2 && y < Staticer.goalEndY)
            {
                if (x >= Staticer.goalStartX && x <= Staticer.goalStartX + width / 3)//判断落点区域
                {
                    Staticer.isPlay = true;
                    Staticer.animationString = "leftUpCorner";
                }
                else if (x > Staticer.goalStartX + width / 3 && x <= Staticer.goalStartX + width / 2)
                {
                    Staticer.isPlay = true;
                    Staticer.animationString = "leftUp";
                }
                else if (x > Staticer.goalStartX + width / 2 && x <= Staticer.goalStartX + width / 3 * 2)
                {
                    Staticer.isPlay = true;
                    Staticer.animationString = "rightUp";
                }
                else if (x > Staticer.goalStartX + width / 3 * 2 && x <= Staticer.goalStartX)
                {
                    Staticer.isPlay = true;
                    Staticer.animationString = "rightUpCorner";
                }
            }
            else if (y < Staticer.goalStartY + heigh / 2)
            {
                if (x >= Staticer.goalStartX && x <= Staticer.goalStartX + width / 5 * 2)
                {
                    Staticer.isPlay = true;
                    Staticer.animationString = "leftDown";
                }
                else if (x > Staticer.goalStartX + width / 5 * 2 && x <= Staticer.goalStartX + width / 5 * 3)
                {
                    Staticer.isPlay = true;
                    Staticer.animationString = "down";
                }
                else if (x > Staticer.goalStartX + width / 5 * 3 && x <= Staticer.goalEndX)
                {
                    Staticer.isPlay = true;
                    Staticer.animationString = "rightDown";
                }
            }
        }
	}

    void OnTriggerEnter()
    {
        if(!Staticer.isCrash&&ball!=null)
        {
            Staticer.isCrash = true;
            Staticer.crashPoint = ball.transform.position;
        }
    }
}
