using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageInv : MonoBehaviour
{
    public Image Qtext;
    float a_color;
    public float speed = 1;
    bool flag_G;
    void Start()
    {
        a_color = 0;
    }
    void Update()
    {
        //テキストの透明度を変更する
        Qtext.color = new Color(255, 255, 255, a_color);
        if (flag_G)
            a_color -= Time.deltaTime * speed;
        else
            a_color += Time.deltaTime * speed;
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
}