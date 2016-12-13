using UnityEngine;
using System.Collections;

public class UIListener : MonoBehaviour
{
	GameObject mainPanelHandle;
	GameObject panelHandle;
	GameObject panelTip;
	GameObject panelTipReset;
	GameObject facePanel;
	UIPanel paneMain;
	public UIInput inputPlayer;
	public UISlider sliderMusic;
	public UISlider sliderSound;
	public AudioClip musicBg;
	public static bool restart=false;
	bool tipShow;
	// Use this for initialization
	void Start ()
	{
		mainPanelHandle=GameObject.Find("UI Root (3D)/Anchor/MainPanelHandle");
		panelHandle=GameObject.Find("UI Root (3D)/Anchor/PanelHandle");
		panelTip=GameObject.Find("UI Root (3D)/Anchor/PanelTip");
		panelTipReset=GameObject.Find("UI Root (3D)/Anchor/PanelTipReset");
		panelHandle.transform.position+=Vector3.down*10;
		panelHandle.transform.Rotate(Vector3.down*90);
		GameObject.Find("UI Root (3D)/Anchor/PanelTouchTip").transform.position+=Vector3.up*10;
		panelTip.transform.localScale=Vector3.zero;
		panelTipReset.transform.localScale=Vector3.zero;
		if(Staticer.stringPlayer.Length==0&&PlayerPrefs.GetString("Player").Length==0)
		{
			Staticer.stringPlayer="Player";
			Staticer.valueMusic=0.5f;
			Staticer.valueSound=0.5f;
			PlayerPrefs.SetString("Player",Staticer.stringPlayer);
			PlayerPrefs.SetFloat("Music",Staticer.valueMusic);
			PlayerPrefs.SetFloat("Sound",Staticer.valueSound);
		}
		Staticer.stringPlayer=PlayerPrefs.GetString("Player");
		Staticer.valueMusic=PlayerPrefs.GetFloat("Music");
		Staticer.valueSound=PlayerPrefs.GetFloat("Sound");
		inputPlayer.label.text=Staticer.stringPlayer;
		sliderMusic.value=Staticer.valueMusic;
		sliderSound.value=Staticer.valueSound;
		for(int i=0;i<Staticer.players.Length;i++)
		{
			Staticer.players[i]=PlayerPrefs.GetString("Player"+i);
			Staticer.scores[i]=PlayerPrefs.GetInt("Score"+i);
			if(Staticer.players[i].Length!=0)
			{
				GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelHistory/LabelNO"+
				i+"/LabelPlayer").GetComponent<UILabel>().text=Staticer.players[i];
				GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelHistory/LabelNO"+
				i+"/LabelScore").GetComponent<UILabel>().text=Staticer.scores[i]+"";
			}
			else
			{
				GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelHistory/LabelNO"+
				i+"/LabelPlayer").GetComponent<UILabel>().text="";
				GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelHistory/LabelNO"+
				i+"/LabelScore").GetComponent<UILabel>().text="";
			}
		}
		GetComponent<AudioSource>().clip=musicBg;
		GetComponent<AudioSource>().volume=Staticer.valueMusic;
		GetComponent<AudioSource>().Play();
		if(restart)
		{
			ButtonStartClick(null);
			restart=false;
		}
		inputPlayer.GetComponent<UISprite>().enabled=false;
	}

	void Awake()
	{
		UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/MainPanelHandle/Panel/ButtonStart")).onClick=ButtonStartClick;
		UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/MainPanelHandle/Panel/ButtonHistory")).onClick=ButtonHistoryClick;
		UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/MainPanelHandle/Panel/ButtonOpition")).onClick=ButtonOpitionClick;
		UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/MainPanelHandle/Panel/ButtonAbout")).onClick=ButtonAboutClick;
		UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/MainPanelHandle/Panel/ButtonExit")).onClick=ButtonExitClick;
		UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelHistory/ButtonBack")).onClick=ButtonBackClick;
		UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelOpition/ButtonBack")).onClick=OpitionBackClick;
		UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelOpition/ButtonReset")).onClick=ButtonResetClick;
		UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelOpition/SliderMusic")).onDrag=SliderMusicClick;
		UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelAbout/ButtonBack")).onClick=ButtonBackClick;
		UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelTip/ButtonOK")).onClick=ButtonOKClick;
		UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelTip/ButtonCacel")).onClick=ButtonCacelClick;
		UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelTipReset/ButtonYes")).onClick=ButtonYesClick;
		UIEventListener.Get(GameObject.Find("UI Root (3D)/Anchor/PanelTipReset/ButtonNO")).onClick=ButtonNOClick;
	}

	void ButtonStartClick(GameObject button)
	{
		GameObject.Find("UI Root (3D)/Anchor/PanelTouchTip").transform.position+=Vector3.down*10;
		switchPanel(mainPanelHandle.transform.GetChild(0),false);
		mainPanelHandle.transform.position+=Vector3.down*10;
		StartCoroutine(touchWaiter());
	}

	void ButtonHistoryClick(GameObject button)
	{
		facePanel=GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelHistory");
		StartCoroutine(panelUnfold(facePanel));
	}

	void ButtonOpitionClick(GameObject button)
	{
		facePanel=GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelOpition");
		StartCoroutine(panelUnfold(facePanel));
		inputPlayer.GetComponent<UISprite>().enabled=true;
	}

	void ButtonAboutClick(GameObject button)
	{
		facePanel=GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelAbout");
		StartCoroutine(panelUnfold(facePanel));
	}

	void ButtonExitClick(GameObject button)
	{
		switchPanel(mainPanelHandle.transform.GetChild(0),false);
		StartCoroutine(tipUnfold(panelTip,mainPanelHandle.transform.GetChild(0).transform));
	}

	void ButtonBackClick(GameObject button)
	{
		StartCoroutine(panelFold(facePanel));
	}

	void OpitionBackClick(GameObject button)
	{
		StartCoroutine(panelFold(facePanel));
		Staticer.stringPlayer=inputPlayer.label.text;
		Staticer.valueMusic=sliderMusic.value;
		Staticer.valueSound=sliderSound.value;
		PlayerPrefs.SetString("Player",Staticer.stringPlayer);
		PlayerPrefs.SetFloat("Music",Staticer.valueMusic);
		PlayerPrefs.SetFloat("Sound",Staticer.valueSound);
		inputPlayer.GetComponent<UISprite>().enabled=false;
	}

	void ButtonResetClick(GameObject button)
	{
		tipShow=true;
		StartCoroutine(tipUnfold(panelTipReset,GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelOpition").transform));
	}

	void ButtonOKClick(GameObject button)
	{
		Application.Quit();
	}

	void ButtonCacelClick(GameObject button)
	{
		switchPanel(mainPanelHandle.transform.GetChild(0),true);
		StartCoroutine(tipFold(panelTip,mainPanelHandle.transform.GetChild(0).transform));
	}

	void ButtonYesClick(GameObject button)
	{
		Staticer.stringPlayer="Player";
		Staticer.valueMusic=0.5f;
		Staticer.valueSound=0.5f;
		PlayerPrefs.SetString("Player",Staticer.stringPlayer);
		PlayerPrefs.SetFloat("Music",Staticer.valueMusic);
		PlayerPrefs.SetFloat("Sound",Staticer.valueSound);
		Staticer.players=new string[8];
		Staticer.scores=new int[8];
		for(int i=0;i<Staticer.players.Length;i++)
		{
			PlayerPrefs.SetString("Player"+i,"");
			PlayerPrefs.SetInt("Score"+i,0);
			GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelHistory/LabelNO"+i+"/LabelPlayer").GetComponent<UILabel>().text="";
			GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelHistory/LabelNO"+i+"/LabelScore").GetComponent<UILabel>().text="";
		}
		inputPlayer.label.text=Staticer.stringPlayer;
		sliderMusic.value=Staticer.valueMusic;
		sliderSound.value=Staticer.valueSound;
		StartCoroutine(tipFold(panelTipReset,GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelOpition").transform));
		SliderMusicClick(null,Vector2.zero);
		tipShow=false;
	}

	void ButtonNOClick(GameObject button)
	{
		tipShow=false;
		StartCoroutine(tipFold(panelTipReset,GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelOpition").transform));
	}

    void SliderMusicClick(GameObject slider, Vector2 v)
    {
        GetComponent<AudioSource>().volume = GameObject.Find("UI Root (3D)/Anchor/PanelHandle/PanelOpition/SliderMusic").GetComponent<UISlider>().value;
    }

	void switchPanel(Transform panel, bool flag)
	{
		foreach(Transform mainButton in panel)
		{
			if(mainButton.GetComponent<Collider>()!=null)
			{
				mainButton.GetComponent<Collider>().enabled=false;
			}
		}
	}

	IEnumerator panelUnfold(GameObject clickPanel)
	{
		switchPanel(mainPanelHandle.transform.GetChild(0),false);
		clickPanel.transform.position+=Vector3.up*10;
		while(mainPanelHandle.transform.rotation.eulerAngles.y<90)
		{
			mainPanelHandle.transform.Rotate(Vector3.up*Time.deltaTime*500);
			panelHandle.transform.Rotate(Vector3.up*Time.deltaTime*500);
			yield return new WaitForSeconds(Time.deltaTime);
		}
		mainPanelHandle.transform.rotation=new Quaternion(0,0.7f,0,0.7f);
		panelHandle.transform.rotation=new Quaternion(0,0,0,1);
	}

	IEnumerator panelFold(GameObject clickPanel)
	{
		if(tipShow)
			yield break;
		while(mainPanelHandle.transform.rotation.eulerAngles.y>0
		&& mainPanelHandle.transform.rotation.eulerAngles.y<180)
		{
			mainPanelHandle.transform.Rotate(Vector3.down*Time.deltaTime*500);
			panelHandle.transform.Rotate(Vector3.down*Time.deltaTime*500);
			yield return new WaitForSeconds(Time.deltaTime);
		}
		clickPanel.transform.position+=Vector3.down*10;
		switchPanel(mainPanelHandle.transform.GetChild(0),true);
		mainPanelHandle.transform.rotation=new Quaternion(0,0,0,1);
		panelHandle.transform.rotation=new Quaternion(0,-0.7f,0,0.7f);
	}

	IEnumerator tipUnfold(GameObject panelTip, Transform closePanel)
	{
		int i=0;
		while(i<5)
		{
			panelTip.transform.localScale+=new Vector3(0.002f,0.002f,0.002f);
			i++;
			yield return new WaitForSeconds(0.02f);
		}
	}

	IEnumerator tipFold(GameObject panelTip,Transform openPanel)
	{
		int i=0;
		while(i<5)
		{
			panelTip.transform.localScale-=new Vector3(0.002f,0.002f,0.002f);
			i++;
			yield return new WaitForSeconds(0.02f);
		}
	}

	IEnumerator touchWaiter()
	{
		yield return new WaitForSeconds(0.5f);
		Staticer.isIntoGame=true;
		StartCoroutine(twinklePanel());
	}

	IEnumerator twinklePanel()
	{
		UIPanel panel=GameObject.Find("UI Root (3D)/Anchor/PanelTouchTip").GetComponent<UIPanel>();
		bool add=false;
		while(Staticer.isIntoGame)
		{
			if(add)
			{
				panel.alpha+=0.1f;
				if(panel.alpha>=1.0f)
					add=false;
			}
			else
			{
				panel.alpha-=0.1f;
				if(panel.alpha<=0.0f)
					add=true;
			}
			yield return new WaitForSeconds(0.05f);
		}
	}
	// Update is called once per frame
	void Update ()
	{

	}
}
