using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameResultData : MonoBehaviour {

    public Text BadCapText;
    public Text ResultText1;
    public Text ResultText2;
    int point=0; //評価のためのポイント 悪い頭民を捕まえる：2p 良い頭民を捕まえる:-3p 良い頭民を見つける:1p 
    void Start () {
        Point();
        ResultTextChange1();
        BadCapText.text  = "　　　　　　：" + PlayerPrefs.GetInt("HeadPGetNum1") + "匹";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void ResultTextChange1()
    {
        if ((PlayerPrefs.GetInt("TotalPop0")+ PlayerPrefs.GetInt("TotalPop1")) != 0) {

            if ((PlayerPrefs.GetInt("TotalPop0") - PlayerPrefs.GetInt("HeadPGetNum0")) < (PlayerPrefs.GetInt("TotalPop1") - PlayerPrefs.GetInt("HeadPGetNum1")))
            {
                ResultText1.text = "あなたの頭は荒れています…";
                ResultTextChange2(2);
                
            }
            else
            {
                ResultText1.text = "あなたの頭は平和です！";
                ResultTextChange2(1);
            }
        }
        else
        {
            ResultText1.text = "頭民の存在を確認できなかった…";
            ResultTextChange2(0);
        }
    }
    void ResultTextChange2(int True)
    {
        string s = "";
        if (point <= 35) //ポイントが25以下なら
        {
            if (True == 1)                                                                      //トゥルーエンドなら
            {
                s = "しかし,";
            }
            ResultText2.text = s+"もう少し頑張りましょう！";
        }
        else if (point <= 85)    // pointが36～85なら
        {
            if (True == 2)                                                                      //バッドエンドなら
            {
                s = "しかし,";
            }
            ResultText2.text = s+"なかなかの成果でした！";
        }else if (point>= 86)   //pointが86以上なら
        {
            if (True == 2)                                                                      //バッドエンドなら
            {
                s = "しかし,";
            }
            ResultText2.text = s+"とても良い成果でした！ お疲れ様でした！";
        }
    }
    void Point()
    {
        point += ((PlayerPrefs.GetInt("HeadPGetNum1") * 2)+(PlayerPrefs.GetInt("HeadPDisNum0") * 1)) - (PlayerPrefs.GetInt("HeadPGetNum0")*3);
        Debug.Log("Point:" + point);
    }
}
