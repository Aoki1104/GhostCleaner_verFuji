using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer_Battery : MonoBehaviour
{
    float MAXElectric, Electric;
    GameObject UI1, UI2; //対象のUI
    UI_Battery script1, script2; //HPのアニメーション処理を行うscript

    void Start()
    {
        PlayerPrefs.SetInt("TimerMinute", 1);
        PlayerPrefs.SetFloat("TimerSecond", 0f);
        MAXElectric = (float)(PlayerPrefs.GetInt("TimerMinute")) * 60f + PlayerPrefs.GetFloat("TimerSecond");
    }

    void Awake()
    {
        Electric = MAXElectric;
        UI1 = GameObject.Find("CurrentElectoric");
        script1 = UI1.GetComponent<UI_Battery>(); //unitychanの中にあるUnityChanScriptを取得して変数に格納する
        //Debug.Log(script1);
    }

    // Update is called once per frame
    void Update()
    {
        Electric = (float)(PlayerPrefs.GetInt("TimerMinute")) * 60f + PlayerPrefs.GetFloat("TimerSecond");
        /*
        if (Electric == 0)
        {
            Electric = MAXElectric;
        }
        */
        script1.HP_Anim(MAXElectric, Electric);



    }
}
