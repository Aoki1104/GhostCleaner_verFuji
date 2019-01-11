using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour {
    float sec,min;
    string s1,s2;
    public GameObject timer;
    Text timerText;
	// Use this for initialization
	void Start () {
        
        timerText = timer.GetComponent<Text>();
        //GetComponent<Text>().text = ((int)time).ToString();
        
	}
	
	// Update is called once per frame
	void Update () {
        sec = PlayerPrefs.GetFloat("TimerSecond");
        if (Mathf.Round(sec) >= 10)
        {
            s1 = Convert.ToString(Mathf.Round(sec));
        }
        else
        {
            s1 = "0" + Convert.ToString(Mathf.Round(sec));
        }
        min = PlayerPrefs.GetInt("TimerMinute");
        if (min >= 10)
        {
            s2 = Convert.ToString(Mathf.Round(min));
        }
        else
        {
            s2 = "0" + Convert.ToString(Mathf.Round(min));
        }
        timerText.text = s2 + ":" + s1;
	}
}
