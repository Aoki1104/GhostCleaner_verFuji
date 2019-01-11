using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class Get_Ghost_Show : MonoBehaviour {
    [SerializeField]
    GameObject Parent;
    [SerializeField]
    GameObject RED_GHOST;
    [SerializeField]
    GameObject GREEN_GHOST;
    [SerializeField]
    GameObject BLUE_GHOST;
    [SerializeField]
    GameObject WHITE_GHOST;
    [SerializeField]
    GameObject BIG_RED_GHOST;
    [SerializeField]
    GameObject BIG_GREEN_GHOST;
    [SerializeField]
    GameObject BIG_BLUE_GHOST;
    [SerializeField]
    GameObject BIG_WHITE_GHOST;
    [SerializeField]
    GameObject Result_PICT_GHTST_FRONT;
    [SerializeField]
    Text Result_Capnum;

    int  loop = 0,stop=0;
    float R=0, G=0, B=0, K=0,NowTime=0;    
    Image Ghost_get;
    float RGBK;
    // Use this for initialization
    const int ConvertionConstant = 65248;
    void Start()
    {
        Ghost_get = Result_PICT_GHTST_FRONT.GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        //ストップ
        stop = 0;
        

        if (stop == 0 && RGBK == PlayerPrefs.GetFloat("R") + PlayerPrefs.GetFloat("G") + PlayerPrefs.GetFloat("B") + PlayerPrefs.GetFloat("K")  && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Title");

        }
        //spaceを押すと全部進む
        if (Input.GetKey(KeyCode.Space))
        {
            RGBK = PlayerPrefs.GetFloat("R") + PlayerPrefs.GetFloat("G") + PlayerPrefs.GetFloat("B") + PlayerPrefs.GetFloat("K");
            R = PlayerPrefs.GetFloat("R");
            G = PlayerPrefs.GetFloat("G");
            B = PlayerPrefs.GetFloat("B");
            K = PlayerPrefs.GetFloat("K");
            NowTime = 6;
            //ボタンワンプレスでタイトルに進んでしまわないように
            stop = 1;
        }
        //時間加算
        NowTime += Time.deltaTime;
        //6秒以上たったら少しずつゲージを上げていく
        if (NowTime >= 6)
        {
            Ghost_get.fillAmount = RGBK / (PlayerPrefs.GetFloat("R") + PlayerPrefs.GetFloat("G") + PlayerPrefs.GetFloat("B") + PlayerPrefs.GetFloat("K"));
            RGBK += 1f;
            if (RGBK > PlayerPrefs.GetFloat("R") + PlayerPrefs.GetFloat("G") + PlayerPrefs.GetFloat("B") + PlayerPrefs.GetFloat("K"))
            {
                RGBK = PlayerPrefs.GetFloat("R") + PlayerPrefs.GetFloat("G") + PlayerPrefs.GetFloat("B") + PlayerPrefs.GetFloat("K");
            }
            //捕獲数表示 数字を全角変換
            Result_Capnum.text = ConvertToFullWidth(Convert.ToString((Mathf.Round(RGBK)))) + "匹";
        }
        //毎ループ戻す（万が一の無限ループ防止用（なんとなく
        loop = 0;   
        //関数Ghost_pop実行　引数は発生位置をずらすためのもの
        Ghost_pop(10);
        Ghost_pop(-10);
    }


    void Ghost_pop(float pos)
    {
        //0~3の間をランダムで数値をだしそれを元に色だす　割り当て内約は一行下
        int Color = UnityEngine.Random.Range(0, 4); // 0=Red , 1=Green , 2=Blue , 3=Black
        switch (Color)
        {
            case 0:
                if (PlayerPrefs.GetFloat("R") <= R)
                {
                    goto case 1;    //赤を最大値生成していたら代わりに緑を落とす
                }
                else {
                    //Ghost_Form実行
                    Ghost_Form(RED_GHOST, pos);
                    //R値加算
                    R++;
                    break;
                }
            case 1:
                if (PlayerPrefs.GetFloat("G") <= G)
                {
                    goto case 2;    //緑を最大値生成していたら代わりに青を落とす
                }
                else {
                    //Ghost_Form実行
                    Ghost_Form(GREEN_GHOST, pos);
                    //G値加算
                    G++;
                    break;
                }
            case 2:
                if (PlayerPrefs.GetFloat("B") <= B)
                {
                    goto case 3;    //青を最大値生成していたら代わりに黒を落とす
                }
                else {
                    //Ghost_Form実行
                    Ghost_Form(BLUE_GHOST,pos);
                    //B値加算
                    B++;
                    break;
                }
            case 3:
                if (!(PlayerPrefs.GetFloat("K") <= K))
                {
                    //Ghost_Form実行
                    Ghost_Form(WHITE_GHOST, pos);
                    //K値加算
                    K++;
                }
                //全体の最大値ならGhostFormは実行せずswithを抜ける
                else if ((R + G + B + K) == (PlayerPrefs.GetFloat("R") + PlayerPrefs.GetFloat("G") + PlayerPrefs.GetFloat("B") + PlayerPrefs.GetFloat("K")))
                {
                    break;

                }else
                {
                    //保険
                    loop++;
                    if (loop < 10000)
                    {
                        break;
                    }
                    goto case 0;
                }
                break;
        }
    }
    //全角変換関数
    static public string ConvertToFullWidth(string halfWidthStr)
    {
        string fullWidthStr = null;

        for (int i = 0; i < halfWidthStr.Length; i++)
        {
            fullWidthStr += (char)(halfWidthStr[i] + ConvertionConstant);
        }

        return fullWidthStr;
    }
    //ゴーストを生成（削除はゴーストに直接アタッチされているRigid_Deleteで行っている
void Ghost_Form(GameObject Ghost,float pos)
    {
        //Instantiate( 生成するオブジェクト,  場所, 回転 );  回転はそのままなら↓
        GameObject obj = Instantiate(Ghost, new Vector2(0f,0f), Quaternion.identity);
        obj.transform.SetParent(Parent.transform.parent);
        obj.transform.position = new Vector2(1770+pos,1200);
    }
}
