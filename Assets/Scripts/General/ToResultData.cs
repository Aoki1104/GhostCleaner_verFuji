using UnityEngine;
using System;
public class ToResultData: MonoBehaviour {
    public void HeadPTotalPop(int ID)   //頭民が沸いた時その数だけカウントする ID;0,好影響 ID:1,悪影響
    {
        PlayerPrefs.SetInt("TotalPop" + Convert.ToInt32(ID), PlayerPrefs.GetInt("TotalPop" + Convert.ToInt32(ID)) + 1);        //TotalPopのリセットはタイトルで行います（もうリセットはできてます）
        //Debug.Log("TotalPop"+ Convert.ToInt32(ID)+ ";" + (PlayerPrefs.GetInt("TotalPop" + Convert.ToInt32(ID))));
    }
    public void HeadPDis(int ID)        //頭民を見つけたときカウントする ID;0,好影響 ID:1,悪影響
    {
        PlayerPrefs.SetInt("HeadPDisNum" + Convert.ToInt32(ID),PlayerPrefs.GetInt("HeadPDisNum" + Convert.ToInt32(ID)) +1);       //HeadPDisNumのリセットはタイトルで行います（もうリセットはできてます）
    }
    public void HeadPGet(int ID)  　     //頭民を捕まえたときカウントする ID;0,好影響 ID:1,悪影響
    {
        PlayerPrefs.SetInt("HeadPGetNum" + Convert.ToInt32(ID), PlayerPrefs.GetInt("HeadPGetNum" + Convert.ToInt32(ID)) + 1);  //HeadPGetNumのリセットはタイトルで行います（もうリセットはできてます）
    }
}
