using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlashManager : MonoBehaviour
{
    public GameObject flashPlane;   //声明闪光板对象
    List<Vector3> randomPoints;     //随机位置列表
    int timer = 0;
	// Use this for initialization
	void Start ()
    {
        randomPoints = new List<Vector3>();
        for(int i=-22;i<=23;i+=5)
        {
            randomPoints.Add(new Vector3(i, 10, 35));
        }
        for(int i=-20;i<=20;i+=5)
        {
            randomPoints.Add(new Vector3(i, 9, 33));
        }
        for(int i=-18;i<=17;i+=5)
        {
            randomPoints.Add(new Vector3(i, 7, 32));
        }
        for(int i=-19;i<=16;i+=5)
        {
            randomPoints.Add(new Vector3(i, 3.5f, 30));
        }
        for(int i=-17;i<=13;i+=5)
        {
            randomPoints.Add(new Vector3(i, 3, 29));
        }
        for(int i=-19;i<=16;i+=5)
        {
            randomPoints.Add(new Vector3(i, 2.5f, 28));
        }
        for(int i=-17;i<=13;i+=5)
        {
            randomPoints.Add(new Vector3(i, 1.6f, 28));
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(timer>3)
        {
            GameObject.Destroy(Instantiate(flashPlane, randomPoints[(int)Random.Range(0, randomPoints.Count)], 
                flashPlane.transform.rotation), 0.1f);
            timer = 0;
        }
        timer++;
	}
}
