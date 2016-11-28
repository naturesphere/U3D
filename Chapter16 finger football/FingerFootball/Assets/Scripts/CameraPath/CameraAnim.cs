using UnityEngine;
using System.Collections;

public class CameraAnim : MonoBehaviour {
    public GameObject player;
    public Camera camera01;
    public Camera camera02;
    public AudioClip musicBg;
    public Texture2D tipTexture;
        // Use this for initialization
	void Start () {
        Shoot shoot = (Shoot)camera02.GetComponent("Shoot");
        shoot.enabled = false;
        GameUI gu = (GameUI)camera02.GetComponent("GameUI");
        gu.enabled = false;
        GameObject.Find("Main Camera").GetComponent<UIPauseListener>().enabled = false;
	}

    void OnEnable() {
        GetComponent<AudioSource>().clip = musicBg;
        GetComponent<AudioSource>().volume = Staticer.valueMusic;
        GetComponent<AudioSource>().Play();
    }
	
	// Update is called once per frame
	void Update () {
        if ((Input.touchCount > 0||Input.GetMouseButtonUp(0)) && !Staticer.isGameOver && Staticer.isIntoGame)//发生触摸
        {
            camera02.enabled = true;//切换摄像机
            camera01.enabled = false;
            Staticer.isPause = false;
            Staticer.timeBegin = Time.time;
            GameObject.Find("Main Camera").GetComponent<UIPauseListener>().enabled = true;
            Shoot shoot = (Shoot)camera02.GetComponent("Shoot");
            shoot.enabled = true;
            GameUI gu = (GameUI)camera02.GetComponent("GameUI");
            gu.enabled = true;
            QualitySettings.vSyncCount = 0;     //设置垂直同步
            this.enabled = false;
            GetComponent<AudioSource>().Stop();

            GameObject.Find("UI Root/Camera").GetComponent<Camera>().enabled = true;
            GameObject.Find("UI Root (3D)").transform.position += Vector3.up * 10;
            GameObject.Find("UI Root (3D)/Camera").GetComponent<Camera>().enabled = false;
            Staticer.isIntoGame = false;
        }
        if (player != null)
        {
            camera01.transform.LookAt(player.transform);
        }
        else {
            player = GameObject.FindGameObjectWithTag("Player");
            camera01.transform.LookAt(player.transform);
        }

	}
    void OnGUI() {
        
    }
}
