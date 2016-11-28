using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour {

    public UILabel labelBest;
    public UILabel labelFace;
    public UILabel labelTime;
    public UIPanel uiPause;
    public UILabel labelScore;
    bool disabled;


	// Use this for initialization
	void Start () {
        labelBest.text = Staticer.scores[0] + "";
        labelTime.text = 60 + "";
        GameObject.Find("UI Root/Anchor/PanelOver").transform.position += Vector3.left * 3;
        Staticer.timeBegin = Time.time;
        disabled = false;
	}

    void Awake() {                                  //设置重新开始按钮和回到主菜单按钮的回调事件
        UIEventListener.Get(GameObject.Find("UI Root/Anchor/PanelOver/ButtonRetry")).onClick = RetryClick;
        UIEventListener.Get(GameObject.Find("UI Root/Anchor/PanelOver/ButtonQuitmenu")).onClick = QuitmenuClick;
    }

	// Update is called once per frame
	void Update () {
        if (disabled) {
            return;
        }
        labelFace.text = (int)Staticer.score + "";
        Staticer.times = (int)(60 - Time.time + Staticer.timeBegin);
        if (Staticer.times < 0 && !Staticer.isPause) {          //游戏结束时的操作
            GameObject.Find("Main Camera").GetComponent<Shoot>().enabled = false;
            GameObject.Find("UI Root/Anchor/PanelOver").transform.position += Vector3.right * 3;
            labelScore.text = labelFace.text;
            if (PlayerPrefs.GetInt("Score0") < Staticer.score) {     //更高记录
                GameObject.Find("UI Root/Anchor/PanelOver/SpriteScore").transform.position += Vector3.left * 3;
            } else {
                GameObject.Find("UI Root/Anchor/PanelOver/SpriteNewscore").transform.position += Vector3.left * 3;
            }
            saveScore(Staticer.score);
            uiPause.enabled = false;
            disabled = true;
            Camera camera01 = GameObject.Find("Camera01").GetComponent<Camera>();
            camera01.enabled = true;
            //camera01.GetComponent<CameraAnim>().enabled = true;
            ((CameraAnim)camera01.GetComponent("CameraAnim")).enabled = true;
            GameObject.Find("UI Root/Camera").GetComponent<Camera>().depth = 1;
            Staticer.isGameOver = true;
            GameObject.Find("UI Root/Anchor/PanelGame/ButtonPause").GetComponent<Collider>().enabled = false;
        } else {
            labelTime.text = Staticer.times + "";           //更新时间板
        }
	}

    void RetryClick(GameObject button) {                    //重新开始按钮回调事件
        Staticer.isGameOver = false;
        GameObject.Find("UI Root/Anchor/PanelGame/ButtonPause").GetComponent<Collider>().enabled = true;
        Application.LoadLevel(Application.loadedLevelName);
        UIListener.restart = true;
    }

    void QuitmenuClick(GameObject button) {                 //返回主菜单按钮回调事件
        Staticer.isGameOver = false;
        GameObject.Find("UI Root/Anchor/PanelGame/ButtonPause").GetComponent<Collider>().enabled = true;
        Application.LoadLevel(Application.loadedLevelName);
        
    }
    void saveScore(int faceScore) {                         //进行数据的保存

        Record[] historyRecord = new Record[9];
        historyRecord[0] = new Record();
        historyRecord[0].player = Staticer.stringPlayer;
        historyRecord[0].score = Staticer.score;
        for (int i = 1; i < historyRecord.Length; i++) {
            historyRecord[i] = new Record();
            historyRecord[i].player = Staticer.players[i - 1];
            historyRecord[i].score = Staticer.scores[i - 1];
        }

        for (int i = 0; i < historyRecord.Length; i++) {             //对这九个成绩进行排序
            for (int j = 1; j < historyRecord.Length - i; j++) {
                if (historyRecord[j].score > historyRecord[j - 1].score) {
                    Record temp = historyRecord[j];
                    historyRecord[j] = historyRecord[j - 1];
                    historyRecord[j - 1] = temp;
                }
            }
        }
        for (int i = 0; i < historyRecord.Length - 1; i++)          //储存这九个成绩中的前八个
        {
            PlayerPrefs.SetString("Player" + i, historyRecord[i].player);
            PlayerPrefs.SetInt("Score" + i, historyRecord[i].score);
            Staticer.players[i] = historyRecord[i].player;
            Staticer.scores[i] = historyRecord[i].score;
        }
    }
    class Record {                  //带玩家名和分数值的储存单位
        public int score;
        public string player;
        public Record() {
            score = 0;
            player = "Player";
        }
    }
}
