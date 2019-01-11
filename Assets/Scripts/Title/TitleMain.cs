using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TitleMain : MonoBehaviour {
    SceneChange script;
    public String Next_SceneName;
    void Awake()
    {
        script = GetComponent<SceneChange>();
        PlayerPrefs.SetInt("ToResultList", 0);
        CountReset();
    }	
	void Update () {
     
            script.SceneChanegeFanc(Next_SceneName);
	}
    void CountReset()
    {
        for (int i = 0; i < 2; i++)
        {
            PlayerPrefs.SetInt("TotalPop"+Convert.ToInt32(i), 0);
            PlayerPrefs.SetInt("HeadPDisNum" + Convert.ToInt32(i), 0);
            PlayerPrefs.SetInt("HeadPGetNum" + Convert.ToInt32(i), 0);
        }
    }
}
