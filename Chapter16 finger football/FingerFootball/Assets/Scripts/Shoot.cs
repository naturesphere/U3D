using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    private float starX = 0;
    private float starY = 0;
    private float moveX;
    public int touchtimes = 0;
    public GameObject soccerball;
    public GameObject MyCamera;
    public GameObject ballParticle;
    private float resettime = 0;
    private bool resetflag = false;
    public GameObject goalie;
    public GameObject goaliePrefab;
    public GameObject soccerPrefab;

    private Vector2 ballpos;
    private Vector2 touchpos;
    private Vector2 beginpos;
    private Vector2 shootpos;
    private bool shootflag = false;
    private float touchtime;
    private float D = Mathf.Pow(Screen.height / 10, 2);
    private bool jishi = false;
    public GameObject cube;
    private float touchDx;
    private bool huahu;
    private float curveForce;
    private bool posflag;
	// Use this for initialization
	void Start ()
    {
        starX = 0;
        starY = 0;
        resetflag = false;
        touchtime = 0;
        ballpos = new Vector2(GetComponent<Camera>().worldtoviewportPoint(soccerball.transform.position).x * 
            Screen.width, GetComponent<Camera>().WorldToViewPortPoint(soccerball.transform.position).y * Screen.height);
        shootflag = false;
        staticer.animationString = null;
        BoxCollider b = goalie.GetComponentInChildren<BoxCollider>();
        b.size = new Vector3(0.2f, 0.1f, 0.1f);
        touchtimes = 0;
        jishi = false;
        touchDx = 0;
        huahu = false;
        curveForce = 0;
        posflag = false;
        CameraFollow cf = (CameraFollow)GetComponent("CameraFollow");//摄像机跟随脚本
        cf.enabled = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (soccerball == null)
        {
            soccerball = GameObject.FindWithTag("ball");
            Start();
        }

        if (particle != null)
            Particle.transform.position = soccerball.transform.position;

        foreach (Touch t in Input.touches)
        {
            touchpos = t.position;
            if (t.phase == TouchPhase.Began)
            {
                touchtime = 0;
                if (Mathf.Pow(touchpos.x - ballpos.x, 2) + Mathf.Pow(touchpos.y - ballpos.y, 2) > 6000)
                {
                    shootflag = false;
                }
                else
                {
                    beginpos = touchpos;
                    shootflag = true;
                    jishi = true;
                }
            }
            if (t.phase == TouchPhase.Moved)
            {
                if (shootflag)
                {
                    if (jishi)
                        touchtime += Time.deltaTime;
                    float L = Mathf.Pow(t.position.x - beginpos.x, 2) + Mathf.Pow(t.position.y - beginpos.y, 2);
                    if (L > D)//滑动距离大于屏幕高度的四分之一
                    {
                        jishi = false;
                        float V = D / touchtime;
                        if (V > 100000)//滑动速度大于阈值
                        {
                            shootflag = true;
                            float dX = t.position.x - beginpos.x;//得到矢量速度
                            float dY = t.position.y - beginpos.y;
                            //Debug.Log("dX="+dX+" dY="+dY);
                            float T = 14 / Mathf.Clamp((V - 100000) / 100000 + 13, 15, 17);//球飞行时间
                            Vector2 endpoint = endPoint(dX, dY);
                            //cube.transform.position=new Vector3(endpoint.x,endpoint.y,23.8f);
                            float Vx = Mathf.Clamp((endpoint.x - Staticer.ballPos2.x) / T, -3, 3);
                            float Vy = Mathf.Clamp((endpoint.y - Staticer.ballPos2.y + 1 / 2 * 9.8f * T * T) / T + 2.5f, 4f, 6f);
                            float Vz = Mathf.Clamp((V - 100000) / 100000 + 13, 15, 17);
                            //Debug.Log("~~~1"+endpoint);
                            Debug.Log("~~~1" + (endpoint.y - Staticer.ballPos2.y + 1 / 2 * 9.8f * T * T) / T);
                            Debug.Log("~~~2 Vx=" + Vx + "Vy=" + Vy + " Vz=" + Vz + " T=" + T);
                            if (Vx > 2.5f || Vy < 5f)
                            {
                                if (Random.Range(0, 10) < 9)
                                {
                                    Vy = Random.Range(4.5f, 5.5f);
                                }
                            }
                            soccerball.GetComponent<Rigidbody>().velocity = MyCamera.transform.TransformDirection(new Vector3(Vx, Vy, Vz));//发球
                            shootflag = false;
                            resetflag = true;

                            CameraFollow cf = (CameraFollow)GetComponent("CameraFollow");
                            cf.enabled = true;

                            BallParticle.ball = soccerball;//粒子
                            Particle = Instantiate(ballParticle, soccerball.transform.position, soccerball.transform.rotation) as GameObject;
                        }
                    }
                }
                if (resetflag)//球已经发射
                {
                    if (!posflag)
                    {
                        moveX = t.position.x;//只记录手指当前位置
                        posflag = true;
                    }
                    Vector2 movepos = t.position;//记录手指当前位置
                    touchDx = Mathf.Max(Mathf.Abs(movepos.x - beginpos.x), touchDx);//记录下最远位置的绝对值
                    if (Mathf.Abs(movepos.x - beginpos.x) < touchDx - 10)//开始画弧（10是阈值）
                    {
                        huahu = true;
                        if (moveX != 0 && moveX < beginpos.x)//向右划
                        {
                            Debug.Log("~~~right");
                            curveForce = Mathf.Max(-5, (movepos.x - beginpos.x - touchDx) / 60);
                        }
                        else //向左划
                        {
                            Debug.Log("~~~left");
                            curveForce = Mathf.Min(5, (movepos.x - beginpos.x + touchDx) / 60);
                        }
                        Debug.Log("~~~huahu" + curveForce + " mx=" + moveX + " bx=" + beginpos.x);
                    }
                }
            }
        }
        if (huahu)//打弧线球
        {
            soccerball.GetComponent<Rigidbody>().AddForce(new Vector3(curveForce, 0, 0), ForceMode.Force);
        }
        if(resetflag)//射门结束后
        {
            resettime += Time.deltaTime;
            if(resettime>=2)//等待两秒重置
            {
                Staticer.animationString = null;
                resettime = 0;//重置标志位
                resetflag = false;
                Destroy(goalie);
                Destroy(GameObject.FindWithTag("ball"));//删除球
                Destroy(particle);
                switch((int)Random.Range(0,3)/1)//摆放摄像机
                {
                    case 0:
                        MyCamera.transform.position = Staticer.cameraPos1;
                        MyCamera.transform.rotation = Quaternion.Euler(Staticer.cameraRot1);
                        soccerball = (GameObject)Instantiate(soccerPrefab, Staticer.ballPos1, Quaternion.identity);//实例化球
                        break;
                    case 1:
                        MyCamera.transform.position = Staticer.cameraPos2;
                        MyCamera.transform.rotation = Quaternion.Euler(Staticer.cameraRot2);
                        soccerball = (GameObject)Instantiate(soccerPrefab, Staticer.ballPos2, Quaternion.identity);//实例化球
                        break;
                    case 2:
                        MyCamera.transform.position = Staticer.cameraPos3;
                        MyCamera.transform.rotation = Quaternion.Euler(Staticer.cameraRot3);
                        soccerball = (GameObject)Instantiate(soccerPrefab, Staticer.ballPos3, Quaternion.identity);//实例化球
                        break;
                    default:
                        break;
                }

                goalie = (GameObject)Instantiate(goaliePrefab, Staticer.playerpos, Quaternion.identity);
                Staticer.STATUS = Staticer.NORMAL;
                Staticer.endPoint = Staticer.playerpos;//重置守门员坐标
                Start();//重新自动发球
            }
        }
    }
    public Vector2 endPoint(float dX, float dY)
    {
        Vector2 endpoint;
        endpoint.y = dY / 80;
        endpoint.x = dX / 80;
        return endpoint;
    }
}
