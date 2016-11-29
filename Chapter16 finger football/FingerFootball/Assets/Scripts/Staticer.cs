using UnityEngine;
using System.Collections;

public class Staticer : MonoBehaviour
{
    public static bool isPlay = false;
    public static string animationString;
    public static bool isCrash = false;
    public static Vector3 crashPoint = new Vector3();
    public static readonly Vector3 startPoint = new Vector3(-0.25f, 0.359084f, 14.46677f);
    public const float endZ = 24.0f;
    public static Vector3 endPoint = new Vector3();
    public const float goalStartX = -2.526555f;
    public const float goalStartY = 0.2716196f;
    public const float goalEndX = 1.986407f;
    public const float goalEndY = 1.54676f;
    public const float goalWidth = goalEndX - goalStartX;
    public const float goalHeight = goalEndY - goalStartY;
    public const float distance = 7.4f;
    public static float goalX;
    public static float goalY;
    public static float timeToGoal;
    public static bool isSave;
    public static Vector3 ballpos = new Vector3(0, 0.35f, 14.4f);
    public static Vector3 playerpos = new Vector3(-0.25f, 0.75f, 23);
    public static bool resetflag = false;
    public const int NORMAL = -1;
    public const int MISS = 0;
    public const int SAVED = 1;
    public const int GOAL = 2;
    public const int ONEPOINT = 3;
    public const int TWOPOINT = 4;
    public const int THREEPOINT = 5;
    public static int STATUS = NORMAL;
    public static float times;
    public static int score;
    public static float timeBegin;
    public static bool isPause = false;
    public static string stringPlayer = "";
    public static float valueMusic;
    public static float valueSound;
    public static string[] players = new string[8];
    public static int[] scores = new int[8];
    public static bool isGameOver;
    public static Vector3 cameraPos1 = new Vector3(-4.66f, 1.11f, 10f);
    public static Vector3 cameraRot1 = new Vector3(0.3f, 18, 3);
    public static Vector3 ballPos1 = new Vector3(-3.95f, 0.368f, 12.03f);
    public static Vector3 cameraPos2 = new Vector3(0f, 1f, 7.6f);
    public static Vector3 cameraRot2 = new Vector3(0f, -0.4f, 0);
    public static Vector3 ballPos2 = new Vector3(0, 0.4f, 9.7f);
    public static Vector3 cameraPos3 = new Vector3(3f, 1.11f, 10f);
    public static Vector3 cameraRot3 = new Vector3(0f, -15, 0);
    public static Vector3 ballPos3 = new Vector3(2.483883f, 0.4f, 11.89304f);
    public static bool isIntoGame = false;
    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
