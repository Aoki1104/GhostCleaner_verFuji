using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToResult : MonoBehaviour {
    public int HeadPNum;
    public int[] HeadPDescoveryNum;
    public int[] HeadPCaughtNum;
    
    string s;
	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("HeadPNum", HeadPNum);
        for (int i = 0; i < HeadPNum;i++)
        {
            s=Convert.ToString(i);
            PlayerPrefs.SetInt("HeadPDescoveryNum"+s, HeadPDescoveryNum[i]);
            PlayerPrefs.SetInt("HeadPCaughtNum"   +s, HeadPCaughtNum[i]);
            Debug.Log("-------------");
            Debug.Log("HeadPDescoveryNum" + PlayerPrefs.GetInt("HeadPDescoveryNum" + s));
            Debug.Log("HeadPCaughtNum"+PlayerPrefs.GetInt("HeadPCaughtNum" + s));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
