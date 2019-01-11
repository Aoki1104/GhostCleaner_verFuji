using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Randm_Toumin : MonoBehaviour {
    public GameObject red, blue, green, black, none;
    public GameObject[] Hair;
           ToResultData toResultData;
    int number = 0;
    float x, z;
    private bool pop = false;                               //頭民を出現させる
    [SerializeField]
    private int redPnum, greenPnum, bluePnum, blackPnum;    //各頭民の残りの出現できる数
    private int notSpan;                                    //頭民が湧かない確率
    private float notSpanRatio;                             //湧かない割合調整用
    [SerializeField]
    private int popNum;                                     //何匹発生させたか
    GameObject captureManagaer;
    CaptureManagerScript capMana;
    int finished;

    [SerializeField]
    private int num;                                        //ランダムで発生させた数値
    int countStrange;                                       //頭民数格納用
    [SerializeField]
    private string sceneName;                               //シーンの名前

    int count;                                              //

    //見える範囲 x:-14 ～ 14  z:-7 ～ 6

    private bool start = false;
	// Use this for initialization
	void Start () {
        //もらったRGBの数値をSeedRGB内でブレを生じさせる
        #region SeedRGB

        int[] rgbRnd = new int[3];
        rgbRnd[0] = PlayerPrefs.GetInt("GhostRed") + Random.Range(-20, 80);
        rgbRnd[1] = PlayerPrefs.GetInt("GhostGreen") + Random.Range(-20, 80);
        rgbRnd[2] = PlayerPrefs.GetInt("GhostBlue") + Random.Range(-20, 80);
        for(int i = 0; i < 3; i++)
        {
            if (rgbRnd[i] > 255)
            {
                rgbRnd[i] = 255;
            }else if (rgbRnd[i] < 0)
            {
                rgbRnd[i] = 0;
            }
        }
        PlayerPrefs.SetInt("Red", rgbRnd[0]);
        PlayerPrefs.SetInt("Green", rgbRnd[1]);
        PlayerPrefs.SetInt("Blue", rgbRnd[2]);

        #endregion
        notSpanRatio = 2f;
        sceneName = SceneManager.GetActiveScene().name;
        popNum = 0;
        count = 0;
    }
	
	// Update is called once per frame
	void Update () {
        captureManagaer = GameObject.Find("CaptureManager");
        capMana = captureManagaer.GetComponent<CaptureManagerScript>();

        if (count % 5 == 1 || count % 5 == 3)
        {
            notSpanRatio = 1f;
        }else if (count % 5 == 0)
        {
            notSpanRatio = 2f;
        }
        else
        {
            notSpanRatio = 3f;
        }
        #region GetRGB
                    
        //湧く数の設定
        redPnum = PlayerPrefs.GetInt("Red");
        greenPnum = PlayerPrefs.GetInt("Green");
        bluePnum = PlayerPrefs.GetInt("Blue");
        blackPnum = Random.Range((int)(redPnum + greenPnum + bluePnum) / 3, 255 - (int)((redPnum + greenPnum + bluePnum) / 3));
        notSpan = (int)((redPnum + greenPnum + bluePnum + blackPnum) * notSpanRatio);    //湧かない数

        #endregion
        if (sceneName == "Capture")
        {
            finished = capMana.Finished();
            if (finished != 1)
            {
                int step = PlayerPrefs.GetInt("Step");
                if (Input.GetKey(KeyCode.Return))
                {
                    if (start == false)
                    {
                        //配列の中に頭民をランダムでぶっこむよ
                        Hair = GameObject.FindGameObjectsWithTag("Hair");
                        toResultData = GetComponent<ToResultData>();
                        if (pop == true)
                        {

                            for (float z = -7f; z <= 12f; z += 2f)
                            {
                                for (float x = -20f; x <= 20f; x += 2f)
                                {
                                    Instantiate(Toumin_Random(1), new Vector3(x, 0, z), Quaternion.identity);
                                }
                            }
                            Debug.Log(count);
                            pop = false;
                            count++;
                        }
                    }
                }
                else
                {
                    start = false;
                    pop = true;
                    popNum = 0;
                    capMana.CAP_NUM(0);                                             //そのシーン内で捕まえた数初期化
                }
            }
        }
        else
        {
            int step = PlayerPrefs.GetInt("Tutorial_Step");
            if (step == 1)
            {
                if (start == false)
                {
                    //配列の中に頭民をランダムでぶっこむよ
                    Hair = GameObject.FindGameObjectsWithTag("Hair");
                    toResultData = GetComponent<ToResultData>();
                    if (pop == true)
                    {

                        for (float z = -7f; z <= 12f; z += 2f)
                        {
                            for (float x = -20f; x <= 20f; x += 2f)
                            {
                                Instantiate(Toumin_Random(1), new Vector3(x, 0, z), Quaternion.identity);
                            }
                        }
                        pop = false;
                    }
                }
            }
            if (step == 0)
            {
                start = false;
                pop = true;
            }
        }
	}

    GameObject Toumin_Random(int pattern)
    {
        #region GetRGB

        //湧く数の設定
        redPnum = PlayerPrefs.GetInt("Red");
        greenPnum = PlayerPrefs.GetInt("Green");
        bluePnum = PlayerPrefs.GetInt("Blue");
        blackPnum = Random.Range((int)(redPnum + greenPnum + bluePnum) / 3, 255 - (int)((redPnum + greenPnum + bluePnum) / 3));
        notSpan = (int)((redPnum + greenPnum + bluePnum + blackPnum) * notSpanRatio);    //湧かない数

        #endregion
        GameObject toumin = none;
        int sumPeople;
        sumPeople = redPnum + greenPnum + bluePnum + blackPnum + notSpan;
        num = Random.Range(0, sumPeople);

        if(num < notSpan)                                               //何も湧かない
        {
            toumin = none;
        }
        else if (num < notSpan + redPnum)                               //赤い頭民が湧く
        {
            toumin = red;
            countStrange = PlayerPrefs.GetInt("Red") - 1;               //一時的に残り数格納
            PlayerPrefs.SetInt("Red", countStrange);                    //残り頭民数更新
            toResultData.HeadPTotalPop(1);
            popNum++;                                                   //湧いた数にカウント
        }
        else if (num < notSpan + redPnum + greenPnum)                   //緑の頭民が湧く
        {
            toumin = green;
            countStrange = PlayerPrefs.GetInt("Green") - 1;             //一時的に残り数格納
            PlayerPrefs.SetInt("Green", countStrange);                  //残り頭民数更新
            toResultData.HeadPTotalPop(1);
            popNum++;                                                   //湧いた数にカウント
        }
        else if(num < notSpan + redPnum + greenPnum + bluePnum)         //青い頭民が湧く
        {
            toumin = blue;
            countStrange = PlayerPrefs.GetInt("Blue") - 1;              //一時的に残り数格納
            PlayerPrefs.SetInt("Blue", countStrange);                   //残り頭民数更新
            toResultData.HeadPTotalPop(1);
            popNum++;                                                   //湧いた数にカウント
        }
        else                                                            //黒い頭民が湧く
        {
            toumin = black;
            toResultData.HeadPTotalPop(1);
            popNum++;                                                   //湧いた数にカウント
        }
        return toumin;
    }

    //Toumin_Generationはランダム生成の試作版のため、気にしなくてよい
    void Toumin_Generation(GameObject tou,int x1,int z1,int x2,int z2) 
    {
        bool flag = false;
        int x, z;
        x = Random.Range(x1, x2 + 1);
        z = Random.Range(z1, z2 + 1);
   
        for (int i = 0; i < Hair.Length; i++)
        {
            if (flag)
            {
                i--;
                flag = false;
            }
            if(x == Hair[i].transform.position.x) {
                x = Random.Range(x1, x2 + 1);
                flag = true;
            }
            if(z == Hair[i].transform.position.z)
            {
                z = Random.Range(z1, z2 + 1);
                flag = true;
            }
        }
        Instantiate(tou, new Vector3(x, 0, z), Quaternion.identity);
    }

    public int POP_NUM()
    {
        return popNum;
    }
}
