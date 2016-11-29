using UnityEngine;
using System.Collections;

public class TipPlane : MonoBehaviour
{
    public Texture[] tipTextures;
    private Color color;
    private bool breakFlag;
	// Use this for initialization
	void Start ()
    {
        color = GetComponent<Renderer>().material.color;
        color.a = 0;
        breakFlag = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Staticer.STATUS != Staticer.NORMAL && breakFlag)
        {
            GetComponent<Renderer>().material.mainTexture = tipTextures[Staticer.STATUS];
            color.a = 1;
            breakFlag = false;
        }
        if (Staticer.STATUS == Staticer.NORMAL)
            breakFlag = true;
        color.a = Mathf.MoveTowards(color.a, 0, Time.deltaTime * 0.5f);
        GetComponent<Renderer>().material.color = color;

    }
}
