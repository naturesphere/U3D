using UnityEngine;
using System.Collections;

public class UIPauseListener : MonoBehaviour {

    public UIPanel uiPause;
    GameObject buttonPause;
    GameObject panelPause0;
    GameObject panelPause1;
    float timeLerp = 0.0f;

	// Use this for initialization
	void Start () {
        panelPause0 = GameObject.Find("UI Root/Anchor/PanelPause0");                //隐藏暂停界面
        panelPause1 = GameObject.Find("UI Root/Anchor/PanelPause1");
        panelPause0.transform.position += Vector3.left * 3;
        panelPause1.transform.position += Vector3.right * 3;
	}

    void Awake () {
        UIEventListener.Get(GameObject.Find("UI Root/Anchor/PanelGame/ButtonPause")).onClick = ButtonPauseClick;            //设置各个按钮的回调事件
        UIEventListener.Get(GameObject.Find("UI Root/Anchor/PanelPause0/ButtonResume")).onClick = ButtonResumeClick;
        UIEventListener.Get(GameObject.Find("UI Root/Anchor/PanelPause1/ButtonRestart")).onClick = ButtonRestartClick;
        UIEventListener.Get(GameObject.Find("UI Root/Anchor/PanelPause0/ButtonQuitmenu")).onClick = ButtonQuitmenuClick;


    }

	// Update is called once per frame
	void Update () {
        if (Staticer.isPause)           //当处于暂停状态时，显示暂停界面并隐藏游戏界面
        {
            panelPause0.transform.position = Vector3.Lerp(panelPause0.transform.position, Vector3.zero, Time.time - timeLerp);
            panelPause1.transform.position = Vector3.Lerp(panelPause1.transform.position, Vector3.zero, Time.time - timeLerp);
            uiPause.alpha = Mathf.Lerp(uiPause.alpha, 0f, Time.time - Staticer.timeBegin);
        }
        else {
            panelPause0.transform.position = Vector3.Lerp(panelPause0.transform.position, Vector3.left * 3, Time.time - timeLerp);
            panelPause1.transform.position = Vector3.Lerp(panelPause1.transform.position, Vector3.right * 3, Time.time - timeLerp);
            uiPause.alpha = Mathf.Lerp(uiPause.alpha, 1.0f, Time.time - Staticer.timeBegin);
        }
    }

    void ButtonPauseClick(GameObject button) {      //暂停按钮的回调事件
        timeLerp = Time.time;
        Staticer.isPause = true;
        GameObject.Find("Main Camera").GetComponent<Shoot>().enabled = false;
    }

    void ButtonResumeClick(GameObject button) {     //继续游戏按钮的回调事件
        Staticer.isPause = false;
        Staticer.timeBegin += Time.time - timeLerp;
        timeLerp = Time.time;
        GameObject.Find("Main Camera").GetComponent<Shoot>().enabled = true;
    }

    void ButtonRestartClick(GameObject button) {    //重新开始按钮的回调事件
        ButtonQuitmenuClick(button);
        UIListener.restart = true;
    }

    void ButtonQuitmenuClick(GameObject button) {   //返回主菜单按钮的回调事件
        Staticer.isIntoGame = false;
        Application.LoadLevel(Application.loadedLevelName);
    }

}
