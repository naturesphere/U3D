﻿using UnityEngine;
using System.Collections;

public class TipPlane : MonoBehaviour
{

    public Texture[] tipTextures;
    private Color color;
    private bool breakFlag;

    void Start()
    {
        color = GetComponent<Renderer>().material.color;
        color.a = 0;
        breakFlag = true;
    }

    void Update()
    {
        if (Staticer.STATUS != Staticer.NORMAL && breakFlag)
        {                   //当状态值发生变化时替换这显示对应纹理图
            GetComponent<Renderer>().material.mainTexture = tipTextures[Staticer.STATUS];
            color.a = 1;
            breakFlag = false;
        }
        if (Staticer.STATUS == Staticer.NORMAL)
        {
            breakFlag = true;
        }
        color.a = Mathf.MoveTowards(color.a, 0, Time.deltaTime * 0.5f);     //渐变消失
        GetComponent<Renderer>().material.color = color;
    }

}