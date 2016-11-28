using UnityEngine;
using System.Collections;

public class GoalBoard : MonoBehaviour {

    private Vector2 offset;                     //声明偏移坐标

	// Use this for initialization
	void Start () {
        offset.x = Random.Range(0, 0.5f);       //随机初始化偏移坐标
        offset.y = Random.Range(0, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
        offset.x = Mathf.PingPong(Time.time * 0.05f, 0.5f);              //使偏移坐标往返变化
        offset.y = Mathf.PingPong(Time.time * 0.17f, 0.5f);
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);         //进行纹理偏移
        Staticer.goalX = Staticer.goalStartX + Staticer.goalWidth * offset.x / 0.5f;    //储存靶子的坐标
        Staticer.goalY = Staticer.goalStartY + Staticer.goalHeight * offset.y / 0.5f;
	}

    void OnTriggerEnter() {

    }
}
