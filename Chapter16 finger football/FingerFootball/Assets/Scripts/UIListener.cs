﻿using UnityEngine;
using System.Collections;

public class UIListener : MonoBehaviour {

    GameObject mainPanelHandle;             //控制主菜单旋转的对象
    GameObject panelHandle;                 //控制其他菜单旋转的对象
    GameObject panelTip;                    //提示框对象
    GameObject panelTipReset;
    GameObject facePanel;                   //存储当前其他界面对象
    UIPanel panelMain;                      //主菜单
    public UIInput inputPlayer;                     //玩家信息输入框
    public UISlider sliderMusic;                    //背景音乐滑杆
    public UISlider sliderSound;                    //声音滑杆
    public AudioClip musicBg;               //背景音乐声音片段
    public static bool restart = false;
    bool tipShow;

	void Start () {
        mainPanelHandle = GameObject.Find("UI Root (3D)/Anchor/MainPanelHandle");           //获取各个界面对象
        panelHandle = GameObject.Find("UI Root (3D)/Anchor/PanelHandle");
        panelTip = GameObject.Find("UI Root (3D)/Anchor/PanelTip");
        panelTipReset = GameObject.Find("UI Root (3D)/Anchor/PanelTipReset");
        panelHandle.transform.position += Vector3.down * 10;            //隐藏主菜单界面和其他菜单界面
        panelHandle.transform.Rotate(Vector3.down * 90);
        GameObject.Find("UI Root (3D)/Anchor/PanelTouchTip").transform.position += Vector3.up * 10;         //隐藏闪烁面板
        panelTip.transform.localScale = Vector3.zero;                   //隐藏提示框
        panelTipReset.transform.localScale = Vector3.zero;
        if (Staticer.stringPlayer.Length == 0 && PlayerPrefs.GetString("Player").Length == 0) {                            //并对其进行判断和初始化
            Staticer.stringPlayer = "Player";
            Staticer.valueMusic = 0.5f;
            Staticer.valueSound = 0.5f;
            PlayerPrefs.SetString("Player", Staticer.stringPlayer);
            PlayerPrefs.SetFloat("Music", Staticer.valueMusic);
            PlayerPrefs.SetFloat("Sound", Staticer.valueSound);
        }
        Staticer.stringPlayer = PlayerPrefs.GetString("Player");        //储存设置界面参数
        Staticer.valueMusic = PlayerPrefs.GetFloat("Music");
        Staticer.valueSound = PlayerPrefs.GetFloat("Sound");
        inputPlayer.label.text = Staticer.stringPlayer;
        sliderMusic.value = Staticer.valueMusic;
        sliderSound.value = Staticer.valueSound;
        for (int i = 0; i < Staticer.players.Length; i++) {
            Staticer.players[i] = PlayerPrefs.GetString("Player" + i);
            Staticer.scores[i] = PlayerPrefs.GetInt("Score" + i);
            if (Staticer.players[i].Length != 0) {                        //显示历史记录界面
                GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelHistory/LabelNO" + i + "/LabelPlayer").GetComponent<UILabel>().text = Staticer.players[i];
                GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelHistory/LabelNO" + i + "/LabelScore").GetComponent<UILabel>().text = Staticer.scores[i] + "";
            } else {
                GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelHistory/LabelNO" + i + "/LabelPlayer").GetComponent<UILabel>().text = "";
                GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelHistory/LabelNO" + i + "/LabelScore").GetComponent<UILabel>().text = "";
            }
        }
        GetComponent<AudioSource>().clip = musicBg;
        GetComponent<AudioSource>().volume = Staticer.valueMusic;
        GetComponent<AudioSource>().Play();
        if (restart) {
            ButtonStartClick(null);
            restart = false;
        }
        inputPlayer.GetComponent<UISprite>().enabled = false;
    }
    void Awake() {
        UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/MainPanelHandle/Panel/ButtonStart")).onClick = ButtonStartClick;       //设置各个按钮的回调方法
        UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/MainPanelHandle/Panel/ButtonHistory")).onClick = ButtonHistoryClick;
        UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/MainPanelHandle/Panel/ButtonOpition")).onClick = ButtonOpitionClick;
        UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/MainPanelHandle/Panel/ButtonAbout")).onClick = ButtonAboutClick;
        UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/MainPanelHandle/Panel/ButtonExit")).onClick = ButtonExitClick;
        UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelHistory/ButtonBack")).onClick = ButtonBackClick;
        UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelOpition/ButtonBack")).onClick = OpitionBackClick;
        UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelOpition/ButtonReset")).onClick = ButtonResetClick;
        UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelOpition/SliderMusic")).onDrag = SliderMusicClick;
        UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelAbout/ButtonBack")).onClick = ButtonBackClick;
        UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelTip/ButtonOK")).onClick = ButtonOKClick;
        UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelTip/ButtonCacel")).onClick = ButtonCacelClick;
        UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelTipReset/ButtonYes")).onClick = ButtonYesClick;
        UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelTipReset/ButtonNO")).onClick = ButtonNOClick;
    }
    void ButtonStartClick(GameObject button) {              //开始游戏按钮回调事件
        GameObject.Find("UI Root (3D)/Anchor/PanelTouchTip").transform.position += Vector3.down * 10;
        switchPanel(mainPanelHandle.transform.GetChild(0), false);
        mainPanelHandle.transform.position += Vector3.down * 10;
        StartCoroutine(touchWaiter());
    }
    void ButtonHistoryClick(GameObject button) {            //历史记录按钮回调事件
        facePanel = GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelHistory");
        StartCoroutine(panelUnfold(facePanel));
    }
    void ButtonOpitionClick(GameObject button) {            //设置按钮回调事件
        facePanel = GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelOpition");
        StartCoroutine(panelUnfold(facePanel));
        inputPlayer.GetComponent<UISprite>().enabled = true;
    }
    void ButtonAboutClick(GameObject button) {              //关于按钮回调事件
        facePanel = GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelAbout");
        StartCoroutine(panelUnfold(facePanel));
    }
    void ButtonExitClick(GameObject button) {               //退出按钮回调事件

        switchPanel(mainPanelHandle.transform.GetChild(0), false);
        StartCoroutine(tipUnfold(panelTip, mainPanelHandle.transform.GetChild(0).transform));
    }
    void ButtonBackClick(GameObject button) {               //返回按钮回调事件
        StartCoroutine(panelFold(facePanel));
    }
    void OpitionBackClick(GameObject button) {              //设置界面返回按钮回调事件
        StartCoroutine(panelFold(facePanel));
        Staticer.stringPlayer = inputPlayer.label.text;     //储存玩家信息声音音量值
        Staticer.valueMusic = sliderMusic.value;
        Staticer.valueSound = sliderSound.value;
        PlayerPrefs.SetString("Player", Staticer.stringPlayer);
        PlayerPrefs.SetFloat("Music", Staticer.valueMusic);
        PlayerPrefs.SetFloat("Sound", Staticer.valueSound);
        inputPlayer.GetComponent<UISprite>().enabled = false;
    }
    void ButtonResetClick(GameObject button) {              //重设数据按钮回调事件
        tipShow = true;
        StartCoroutine(tipUnfold(panelTipReset, GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelOpition").transform));
    }
    void ButtonOKClick(GameObject button) {                 //确定按钮回调事件
        Application.Quit();
    }
    void ButtonCacelClick(GameObject button)
    {              //取消按钮回调事件
        switchPanel(mainPanelHandle.transform.GetChild(0), true);
        StartCoroutine(tipFold(panelTip, mainPanelHandle.transform.GetChild(0).transform));
    }
    void ButtonYesClick(GameObject button) {                //是按钮回调事件
        Staticer.stringPlayer = "Player";
        Staticer.valueMusic = 0.5f;
        Staticer.valueSound = 0.5f;
        PlayerPrefs.SetString("Player", Staticer.stringPlayer);
        PlayerPrefs.SetFloat("Music", Staticer.valueMusic);
        PlayerPrefs.SetFloat("Sound", Staticer.valueSound);
        Staticer.players = new string[8];
        Staticer.scores = new int[8];
        for (int i = 0; i < Staticer.players.Length; i++) {
            PlayerPrefs.SetString("Player" + i, "");
            PlayerPrefs.SetInt("Score" + i, 0);
            GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelHistory/LabelNO" + i + "/LabelPlayer").GetComponent<UILabel>().text = "";
            GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelHistory/LabelNO" + i + "/LabelScore").GetComponent<UILabel>().text = "";
        }
        inputPlayer.label.text = Staticer.stringPlayer;
        sliderMusic.value = Staticer.valueMusic;
        sliderSound.value = Staticer.valueSound;
        StartCoroutine(tipFold(panelTipReset, GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelOpition").transform));
        SliderMusicClick(null, Vector2.zero);
        tipShow = false;
    }
    void ButtonNOClick(GameObject button) {             //否按钮回调事件
        tipShow = false;
        StartCoroutine(tipFold(panelTipReset, GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelOpition").transform));
    }
    void SliderMusicClick(GameObject slider, Vector2 v) {   //背景音乐音量滑杆回调事件
        GetComponent<AudioSource>().volume = GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelOpition/SliderMusic").GetComponent<UISlider>().value;
    }
    void switchPanel(Transform panel, bool flag) {      //面板开关控制器
        foreach (Transform mainButton in panel){
            if (mainButton.GetComponent<Collider>() != null)
            {
                mainButton.GetComponent<Collider>().enabled = flag;
            }
        }
    }
    IEnumerator panelUnfold(GameObject clickPanel) {        //展开子菜单协同程序
        switchPanel(mainPanelHandle.transform.GetChild(0), false);
        clickPanel.transform.position += Vector3.up * 10;
        while (mainPanelHandle.transform.rotation.eulerAngles.y < 90)
        {
            mainPanelHandle.transform.Rotate(Vector3.up * Time.deltaTime * 500);
            panelHandle.transform.Rotate(Vector3.up * Time.deltaTime * 500);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        mainPanelHandle.transform.rotation = new Quaternion(0, 0.7f, 0, 0.7f);
        panelHandle.transform.rotation = new Quaternion(0, 0, 0, 1);

    }
    IEnumerator panelFold(GameObject clickPanel) {          //回缩子菜单协同程序
        if (tipShow) {
            yield break;
        }
        while (mainPanelHandle.transform.rotation.eulerAngles.y > 0 && mainPanelHandle.transform.rotation.eulerAngles.y < 180) {
            mainPanelHandle.transform.Rotate(Vector3.down * Time.deltaTime * 500);
            panelHandle.transform.Rotate(Vector3.down * Time.deltaTime * 500);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        clickPanel.transform.position += Vector3.down * 10;
        switchPanel(mainPanelHandle.transform.GetChild(0), true);
        mainPanelHandle.transform.rotation = new Quaternion(0, 0, 0, 1);
        panelHandle.transform.rotation = new Quaternion(0, -0.7f, 0, 0.7f);

    }
    IEnumerator tipUnfold(GameObject panelTip, Transform closePanel) {                               //展开提示框协同程序
        int i = 0;
        while (i < 5) {
            panelTip.transform.localScale += new Vector3(0.002f, 0.002f, 0.002f);
            i++;
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator tipFold(GameObject panelTip, Transform openPanel) {                                 //回缩提示框协同程序
        int i = 0;
        while (i < 5) {
            panelTip.transform.localScale -= new Vector3(0.002f, 0.002f, 0.002f);
            i++;
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator touchWaiter() {
        yield return new WaitForSeconds(0.5f);
        Staticer.isIntoGame = true;
        StartCoroutine(twinklePanel());
    }
    IEnumerator twinklePanel() {
        UIPanel panel = GameObject.Find("UI Root (3D)/Anchor/PanelTouchTip").GetComponent<UIPanel>();
        bool add = false;
        while (Staticer.isIntoGame) {
            if (add) {
                panel.alpha += 0.1f;
                if (panel.alpha >= 1.0f) {
                    add = false;
                }
            } else {
                panel.alpha -= 0.1f;
                if (panel.alpha <= 0.0f) {
                    add = true;
                }
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}