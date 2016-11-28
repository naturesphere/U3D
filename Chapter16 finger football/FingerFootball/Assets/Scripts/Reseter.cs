using UnityEngine;
using System.Collections;

public class Reseter : MonoBehaviour {
    public Texture2D button1;
    public GameObject soccerball;
    public GameObject goalie;//守门员
    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), button1))
        {
            Destroy(GameObject.FindWithTag("ball"));
            foreach (GameObject a in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(a);
            }
            Instantiate(soccerball,Staticer.ballpos,Quaternion.identity);
            Instantiate(goalie,Staticer.playerpos,Quaternion.identity);
            Staticer.STATUS = Staticer.NORMAL;
        }
    }
}
