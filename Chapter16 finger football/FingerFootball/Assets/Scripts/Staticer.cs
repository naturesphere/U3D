using UnityEngine;
using System.Collections;

public static class Staticer {
    public static bool isPlay = false;          //播放动画标志位
    public static string animationString;       //当前播放的动画片段名
    public static bool isCrash = false;         //是否与测试板相撞
    public static Vector3 crashPoint = new Vector3();   //与测试板撞击的坐标
    public static readonly Vector3 startPoint = new Vector3(-0.25f, 0.359084f, 14.46677f);      //足球起始位置点
    public const float endZ = 24.0f;
    public static Vector3 endPoint = new Vector3();     //存储球穿过球门时的坐标
    public const float goalStartX = -2.526555f;      //球门左下角坐标
    public const float goalStartY = 0.2716196f;
    public const float goalEndX = 1.986407f;         //球门右上角坐标
    public const float goalEndY = 1.54676f;
    public const float goalWidth = goalEndX - goalStartX;
    public const float goalHeight = goalEndY - goalStartY;
    public const float distance = 7.4f;         //测试板与球门的距离
    public static float goalX;         //球穿过球门时的X坐标,该值储存了靶子的中心坐标，需要在动态纹理中进行赋值
    public static float goalY;         //球穿过球门时的Y坐标
    public static float timeToGoal;             //球从检测板到球门所需的时间
    public static bool isSave;

    public static Vector3 ballpos = new Vector3(0,0.35f,14.4f);//球的位置
    public static Vector3 playerpos = new Vector3(-0.25f,0.75f,23);//守门员的位置
    public static bool resetflag = false;

    public const int NORMAL = -1;               //默认状态值
    public const int MISS = 0;                  //偏离球门的状态值
    public const int SAVED = 1;                 //撞击门将的状态值
    public const int GOAL = 2;                  //穿过球门但没击中靶子的状态值
    public const int ONEPOINT = 3;              //击中靶子外环的状态值
    public const int TWOPOINT = 4;              //击中靶子中环的状态值
    public const int THREEPOINT = 5;            //击中靶子内环的状态值
    public static int STATUS = NORMAL;          //当前状态值

    public static float times;                  //当前剩余时间
    public static int score;                    //当前分数
    public static float timeBegin;              //计时起始点

    public static bool isPause = false;         //是否处于暂停状态

    public static string stringPlayer = "";          //设置界面中的三个参数
    public static float valueMusic;
    public static float valueSound;
    public static string[] players = new string[8]; //历史记录前八名的玩家名字和成绩
    public static int[] scores = new int[8];
    public static bool isGameOver;



    public static Vector3 cameraPos1 = new Vector3(-4.66f, 1.11f, 10f);//球在左侧的位置
    public static Vector3 cameraRot1 = new Vector3(0.3f, 18, 3);
    public static Vector3 ballPos1 = new Vector3(-3.95f, 0.368f, 12.03f);

    public static Vector3 cameraPos2 = new Vector3(0, 1, 7.6f);//球在中间的位置
    public static Vector3 cameraRot2 = new Vector3(0, -0.4f, 0);
    public static Vector3 ballPos2 = new Vector3(0, 0.4f, 9.7f);

    public static Vector3 cameraPos3 = new Vector3(3f, 1.11f, 10);//球在右边的位置
    public static Vector3 cameraRot3 = new Vector3(0, -15, 0);
    public static Vector3 ballPos3 = new Vector3(2.483883f, 0.4f, 11.89304f);

    public static bool isIntoGame = false;
}
