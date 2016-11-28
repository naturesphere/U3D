using UnityEngine;
using System.Collections;

public class Logicer : MonoBehaviour {

    private int[] scores = { 0, 1, 2, 3, 4, 5 };        //用于计算分数和时间的奖励
    private int[] times = { 1, 2, 3 };
    private bool isSave;
    private float beginTime;

	// Use this for initialization
	void Start () {
        beginTime = Time.time;
        Staticer.score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Staticer.times = 60 - Time.time + beginTime;
        if (Staticer.STATUS == Staticer.NORMAL) {
            isSave = false;
        }
        if (isSave) {
            return;
        }
        if (Staticer.STATUS != Staticer.NORMAL) {
            Staticer.score += scores[Staticer.STATUS];
            if (Staticer.STATUS >= Staticer.ONEPOINT) {
                Staticer.times += times[Staticer.STATUS - Staticer.ONEPOINT];
            }
            isSave = true;
        }
	}
}
