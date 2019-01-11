using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Color_Decision : MonoBehaviour {
    //必ず先にスクリプト「RGBToHSV」を読み込み、RGB値をHSV値に変換してから実行してください。


    //テキスト
    
    [SerializeField]
    Text Result_Color_Text;
    [SerializeField]
    Text Result_Color_Description;
    [SerializeField]
    Text Orange_Text;   //オレンジ専用のテキスト

    //イメージ
    //小さいほう
    [SerializeField]
    Image Result_Aura_Color1;
    //大きいほう
    [SerializeField]
    Image Result_Aura_Color2;

    float R, G, B;
    //色相
    float Color_H;
    //判別した色名
    string col;
   
    void Start () {
        Orange_Text.enabled = false;
        R = PlayerPrefs.GetFloat("R") * 1.5f;
        G = PlayerPrefs.GetFloat("G") * 1.5f;
        B = PlayerPrefs.GetFloat("B") * 1.5f;
        if (R >= 255)
        {
            R = 255;
        }
        if (G >= 255)
        {
            G = 255;
        }
        if (B >= 255)
        {
            B = 255;
        }
        //色相を取得
        Color_H = PlayerPrefs.GetFloat("H");
        Color_Dec(Color_H);
        
        //色の説明テキスト
        TextAsset Color_Des_textfile = Resources.Load("Result/ColorDescription/" + col, typeof(TextAsset)) as TextAsset; //Resourcesフォルダから対象テキストを取得
        //読み込みエラー確認
        if (Color_Des_textfile == null)
        {
            Debug.LogError("問題が発生しています。Scene:Result 問題のスクリプト:Color_Decision\nオーラ色の説明を格納したファイルを開けません。スクリプト「Color_Decision」における対象テキストの取得を行っている箇所の参照リンク先を修正してください。\n又は松谷にご連絡くだっしゃーい");
        }

        //リザルト画面をオーラ色に対応するモノに変える
        Result_Color_Change(col, Color_Des_textfile.text, Color_H);

       
    }
	void Update () {
        
	}
    //色の判定
    void Color_Dec(float H)
    {
        
        //赤
        if (H > 319 || H < 24)
        {
            Result_Color_Text.color = new Color(0, 0, 0);
            Result_Change_Png(255, 0, 0);
            Debug.Log("Color:赤色");
            col = "赤";
        }
        //橙
        else if (H < 44)
        {
            Orange_Text.enabled = true;
            Result_Color_Text.color = new Color(0, 0, 0);
            Result_Change_Png(230, 121, 40);
            Debug.Log("Color:橙色");
            
            col = "橙";
            
        }
        //黄
        else if (H < 68)
        {
            Result_Color_Text.color = new Color(0, 0, 0);
            Result_Change_Png(255, 255, 0);
            Debug.Log("Color:黄色");
            col = "黄";
        }
        //緑
        else if (H < 175)
        {
            Result_Color_Text.color = new Color(0, 0, 0);
            Result_Change_Png(0, 255, 0);
            Debug.Log("Color:緑色");
            col = "緑";
        }
        //水
        else if (H < 198)
        {
            Result_Color_Text.color = new Color(0, 0, 0);
            Result_Change_Png(0, 255, 255);
            Debug.Log("Color:水色");
            col = "水";
        }
        //青
        else if (H < 264)
        {
            Result_Color_Text.color = new Color(0, 0, 0);
            Result_Change_Png(0, 0, 255);
            Debug.Log("Color:青色");
            col = "青";
        }
        //紫 H<318
        else
        {
            Result_Color_Text.color = new Color(1, 1, 1);
            Result_Change_Png(255, 0,255);
            Debug.Log("Color:紫色");
            col = "紫";
        }

        if(PlayerPrefs.GetFloat("V") >=220 && PlayerPrefs.GetFloat("S") <= 10)
        {
            Result_Color_Text.color = new Color(0, 0, 0);
            Result_Change_Png(255, 255, 255);
            Debug.Log("Color:白色");
            col = "白";
        }
        
        if (PlayerPrefs.GetFloat("V") < 220 && PlayerPrefs.GetFloat("S") <= 10)
        {
            Result_Color_Text.color = new Color(1, 1, 1);
            Result_Change_Png(192,192,192);
            Debug.Log("Color:灰色");
            col = "灰";
        }
        if (PlayerPrefs.GetFloat("V") < 60)
        {
            Result_Color_Text.color = new Color(1, 1, 1);
            Result_Change_Png(0, 0, 0);
            Debug.Log("Color:黒色");
            col = "黒";
        }
    }

    //9/7アオキ追加　画像の色を変えるだけの関数
    void Result_Change_Png(int R,int G,int B)
    {
       
        Result_Aura_Color1.color = new Color(R / 255f, G / 255f, B / 255f);
        Result_Aura_Color2.color = new Color(R / 255f, G / 255f, B / 255f);
    }

    void Result_Color_Change(string color,string Destext,float H)
    {
        #region 旧版の画像の色を変える処理
        /*
        //画像の色を変える
        

        Result_Aura_Man.color = new Color(R / 255.0f, G / 255.0f, B / 255.0f); 
        Result_Aura_Man_Back.color = new Color(R / 255.0f, G / 255.0f, B / 255.0f); 
        Result_Color_Text_Back.color = new Color(R / 255.0f, G / 255.0f, B / 255.0f);                                                                                */
        #endregion
        //色に対応したテキストの内容を変える
        Result_Color_Text.text = color;
        Result_Color_Description.text = Destext;
    }
}
