using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextInv : MonoBehaviour {
    public TextMeshProUGUI Qtext;
    public bool color = true; //true=白 false=黒:
    float a_color;
    public float speed=1;
    bool flag_G;
    void Start()
    {
        a_color = 0;
    }
    void Update()
    {
        //テキストの透明度を変更する
        TextColor();
        if (flag_G)
            a_color -= Time.deltaTime*speed;
        else
            a_color += Time.deltaTime*speed;
        if (a_color < 0)
        {
            a_color = 0;
            flag_G = false;
        }
        else if (a_color > 1)
        {
            a_color = 1;
            flag_G = true;
        }
    }
    void TextColor()
    {
        if (color)
        {
            Qtext.color = new Color(255, 255, 255, a_color);
        }
        else
        {
            Qtext.color = new Color(0, 0, 0, a_color);
        }
        
    }
}