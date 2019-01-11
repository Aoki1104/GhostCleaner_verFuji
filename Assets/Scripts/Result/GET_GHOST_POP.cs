using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GET_GHOST_POP : MonoBehaviour {
    //ビッグゴーストを出現させるか？
    [SerializeField]
    bool BIG_GHOST = false;
    //ゴーストの沸きポイント
    [SerializeField]
    GameObject Parent;
	//SE
	[SerializeField]
	AudioSource Audio;
	[SerializeField]
	AudioClip SE;
    //小さいゴーストプレハブ
    [SerializeField]
    GameObject RED_GHOST;
    [SerializeField]
    GameObject GREEN_GHOST;
    [SerializeField]
    GameObject BLUE_GHOST;
    [SerializeField]
    GameObject WHITE_GHOST;
    //でかいゴーストプレハブ
    [SerializeField]
    GameObject BIG_RED_GHOST;
    [SerializeField]
    GameObject BIG_GREEN_GHOST;
    [SerializeField]
    GameObject BIG_BLUE_GHOST;
    [SerializeField]
    GameObject BIG_WHITE_GHOST;

    //ゲージ
    [SerializeField]
    Image Ghost_get;
    //捕獲数表示
    [SerializeField]
    Text Ghost_Cap_Num;
    //R,G,B,W→ゲーム中に捕まえたゴーストの数
    //time→時間はかる用
    float R,G,B,W,time,time2;
    float a=1;
    float a2 = 0;
	float count = 0;
	//ゴーストがN体解けたらSEを鳴らす
	[SerializeField]
	float SE_Switch=10;
	float SE_Trigger = 1;

    //BR,BG,BB,BW→でかいゴーストの数
    //SR,SG,SB,SW→小さいゴーストの数
    float BR,BG,BB,BW,SR,SG,SB,SW;
    //でかいゴーストを計算（25体につき1体にひとまとめにする）
    void BIG_GHOST_SET()
    {
        if (R >= 25)
        {
            BR = R / 25;
            BR = Mathf.Floor(BR);
            SR = R - (BR * 25);
        }
        if (G >= 25)
        {
            BG = G / 25;
            BG = Mathf.Floor(BG);
            SG = G - (BG * 25);
        }
        if (B >= 25)
        {
            BB = B / 25;
            BB = Mathf.Floor(BB);
            SB = B - (BB * 25);
        }
        if (W >= 25)
        {
            BW = W / 25;
            BW = Mathf.Floor(BW);
            SW = W - (BW * 25);
        }
    }
    void NOT_BIG_GHOST()
    {
        SW = W;
        SR = R;
        SG = G;
        SB = B;
    }
    //ゴーストを沸かせる　引数は沸かせるゴーストのプレハブを指定
    void Ghost_Form(GameObject Ghost)
    {
        //Instantiate( 生成するオブジェクト,  場所, 回転 );  回転はそのままなら↓
        GameObject obj = Instantiate(Ghost, new Vector2(0f, 0f), Quaternion.identity);
        obj.transform.SetParent(Parent.transform.parent);
        
        obj.transform.position = new Vector2(Parent.transform.position.x + Random.Range(-900,900) ,Parent.transform.position.y);
        obj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

    }
    //どの色の小さいゴーストを沸かせるかの判定
    void Small_GHOST_Pop()
    {
        int Color = UnityEngine.Random.Range(0, 4); // 0=Red , 1=Green , 2=Blue , 3=White
		count++;
        switch (Color)
        {
            case 0: //赤
                if (SR == 0)
                {
                    goto case 1;
                }
                else
                {
                    Ghost_Form(RED_GHOST);
                    SR--;
                }
                break;
            case 1: //緑
                if (SG == 0)
                {
                    goto case 2;
                }
                else
                {
                    Ghost_Form(GREEN_GHOST);
                    SG--;
                }
                break;
            case 2: //青
                if (SB == 0)
                {
                    goto case 3;
                }
                else
                {
                    Ghost_Form(BLUE_GHOST);
                    SB--;
                }
                break;
            case 3: //白
                if ((SR + SG + SB + SW) == 0) //ないとは思うが一応
                {
                    break;
                }
                else if (SW == 0)
                {
                    goto case 0;
                }
                else
                {
                    Ghost_Form(WHITE_GHOST);
                    SW--;
                }
                break;
        }
    }
    //どの色の大きいゴーストを沸かせるかの判定
    void Big_GHOST_Pop()
    {
        int Color = UnityEngine.Random.Range(0, 4); // 0=Red , 1=Green , 2=Blue , 3=White
        switch (Color)
        {
            case 0: //赤
                if (BR == 0)
                {
                    goto case 1;
                }
                else
                {
                    Ghost_Form(BIG_RED_GHOST);
                    BR--;
                }
                break;
            case 1: //緑
                if (BG == 0)
                {
                    goto case 2;
                }
                else
                {
                    Ghost_Form(BIG_GREEN_GHOST);
                    BG--;
                }
                break;
            case 2: //青
                if (BB == 0)
                {
                    goto case 3;
                }
                else
                {
                    Ghost_Form(BIG_BLUE_GHOST);
                    BB--;
                }
                break;
            case 3: //白
                if ((BR + BG + BB + BW) == 0) //ないとは思うが一応
                {
                    break;
                }
                else if (BW == 0)
                {
                    goto case 0;
                }
                else
                {
                    Ghost_Form(BIG_WHITE_GHOST);
                    BW--;
                }
                break;
        }
    }
    void Start () {
        Debug.Log("Ghost_POP起動");
        R = PlayerPrefs.GetFloat("R");
        G = PlayerPrefs.GetFloat("G");
        B = PlayerPrefs.GetFloat("B");
        W = PlayerPrefs.GetFloat("K");
        /*
		Debug.Log("R:" + R);
        Debug.Log("G:" + G);
        Debug.Log("B:" + B);
        Debug.Log("W:" + W);
        Debug.Log("合計：" + (R+G+B+W));
        */
        PlayerPrefs.SetFloat("IR", 0);
        PlayerPrefs.SetFloat("IG", 0);
        PlayerPrefs.SetFloat("IB", 0);
        PlayerPrefs.SetFloat("IW", 0);
        if (BIG_GHOST)
        {
            BIG_GHOST_SET();
        }
        else
        {
            NOT_BIG_GHOST();
        }
        
    }
    void Update () {
        time = time + Time.deltaTime;
        time2 = time2 + Time.deltaTime;
		if (PlayerPrefs.GetFloat ("IR") + PlayerPrefs.GetFloat ("IG") + PlayerPrefs.GetFloat ("IB") + PlayerPrefs.GetFloat ("IW") >= SE_Trigger) {
			SE_Trigger = SE_Trigger + SE_Switch;
			Audio.PlayOneShot(SE);
		}
        //ゲージを増やす
        Ghost_get.fillAmount = ((PlayerPrefs.GetFloat("IR") + PlayerPrefs.GetFloat("IG") + PlayerPrefs.GetFloat("IB") + PlayerPrefs.GetFloat("IW")) / (PlayerPrefs.GetFloat("R") + PlayerPrefs.GetFloat("G") + PlayerPrefs.GetFloat("B") + PlayerPrefs.GetFloat("K")));
        Ghost_Cap_Num.text = count + "匹";
        if (time2 >= 60 || (PlayerPrefs.GetFloat("IR") + PlayerPrefs.GetFloat("IG") + PlayerPrefs.GetFloat("IB") + PlayerPrefs.GetFloat("IW")) >= (PlayerPrefs.GetFloat("R") + PlayerPrefs.GetFloat("G") + PlayerPrefs.GetFloat("B") + PlayerPrefs.GetFloat("K")))
        {
            SceneManager.LoadScene("Result");
        }

        //色を変えていく
        Ghost_get.color = new Color((PlayerPrefs.GetFloat("IR")*1.5f) / 255, (PlayerPrefs.GetFloat("IG")*1.5f) / 255, (PlayerPrefs.GetFloat("IB")*1.5f) / 255, (PlayerPrefs.GetFloat("IR") + PlayerPrefs.GetFloat("IB") + PlayerPrefs.GetFloat("IG") + PlayerPrefs.GetFloat("IW")) / (PlayerPrefs.GetFloat("R") + PlayerPrefs.GetFloat("G") + PlayerPrefs.GetFloat("B") + PlayerPrefs.GetFloat("K")));
        
        //ゴーストを沸かせる
        if ((SR + SG + SB + SW) !=  0 && time > 0.1f)
        {
            Small_GHOST_Pop();
        }
		if ((SR + SG + SB + SW) !=  0 && time > 0.1f)
		{
			Small_GHOST_Pop();
		}
		if ((SR + SG + SB + SW) !=  0 && time > 0.1f)
		{
			Small_GHOST_Pop();
		}
		if ((SR + SG + SB + SW) !=  0 && time > 0.1f)
		{
			Small_GHOST_Pop();
			time = 0;
		}
        else if((BR + BG + BB + BW) != 0 && time > 0.5f)
        {
            Big_GHOST_Pop();
            time = 0;
        }
    }
}
