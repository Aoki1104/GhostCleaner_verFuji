using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CaptureManagerScript : MonoBehaviour {
	[SerializeField]private int min;								//制限時間：分
	[SerializeField]private float sec;                              //制限時間：秒
    [SerializeField]private int stop;                               //リザルトシーン移行時Webカメラを止める
    [SerializeField]private int Web_cap_ON;                         //Webカメラに先に起動して待ってもらうもの
    float releaseTime;						                        //Webカメラの表示の遅延として使うよ

    public SerialHandler serialHandler_;

    public Camera Cameras; //キャプチャシーンのカメラ
    public Camera Web_Cam; //ウェブカメラのカメラ
    public Camera Dark_Cam;

    public GameObject cursor;
    private float Result_Time;                                     //リザルト画面へ移行する待ち時間のタイマー
    private float Debug_Time;                                      //デバッグ用タイマー
    private float Delay_Time;

    private bool next_step = true;
    public Image Cursor;

    public AudioSource vacuumCleaner;                               //音源：掃除機
   
    private bool Releas;
    private string sceneName;                                       //シーンの名前

    public Text textFinish;
    public Image releaseCleaner;                                    //クリーナー離す用の画像
    public GameObject relCleaner;                                   //クリーナー離す用の画像
    float cleaneRed;
    float cleaneGreen;
    float cleaneBlue;
    float alfa;
    public GameObject releaseText;                                  //クリーナー離す用のテキスト
    public GameObject tornado;
    [SerializeField]
    private int popNum;
    [SerializeField]
    private int capNum;
    Randm_Toumin randm_Toumin;
    GameObject random_Minzoku;

    SerialHandler serialhandler;                //ここから
    GameObject serial;
    [SerializeField]
    private byte[] portNum;
    public int dir, vibStr;                     //ここまで振動用プログラム

    float vib_time;                             //振動(大)の回っている時間
    bool is_vib0, is_vib1;                      //振動しているか(1：振動(小), 2：振動(大))
	private bool Finish_SE; 
    bool vib0Vibrate;                           //一回だけ振動させる
    float changedTime;

	public AudioSource FnishSe;
	public int TimeMin,TimeSec;
	//public AudioClip SE;
    void Start () {
		//FnishSe = GetComponent<AudioSource>();
        Web_cap_ON = 0;                                             //Webカメラの設定の初期化
        stop = 0;                                                   //リザルト移行フラグの初期化
        PlayerPrefs.SetInt("Web_On", Web_cap_ON);                   
        PlayerPrefs.GetInt("Stop", stop);
        /*min = 1;					                                //制限時間セット
		sec = 0f;
        PlayerPrefs.SetInt("TimerMinute", min);                     //制限時間を格納
        PlayerPrefs.SetFloat("TimerSecond", sec);*/
        min = TimeMin;
		sec = TimeSec;
		releaseTime = 0f;                                           //Webカメラへの移行の秒数
        Result_Time = 0f;                                           //リザルトへの移行の秒数
        Releas = false;                                             //サーチデバイスを離したかのフラグ
        //Debug_Time = 0f;
        sceneName = SceneManager.GetActiveScene().name;
        textFinish.enabled = false;
        PlayerPrefs.SetInt("Stop", 0);
        //下はコメントアウトしてあるため、再度使う場合は開いてコメントアウトを外すこと
        #region ImageRelease
        /*
        cleaneRed = releaseCleaner.GetComponent<Image>().color.r;
        cleaneGreen = releaseCleaner.GetComponent<Image>().color.g;
        cleaneBlue = releaseCleaner.GetComponent<Image>().color.b;
        releaseCleaner.GetComponent<Image>().color = new Color(cleaneRed, cleaneGreen, cleaneBlue, alfa);
        alfa = 180f;
        */
        #endregion
        relCleaner.SetActive(false);
        releaseText.SetActive(false);
        
        vib_time = 0f;
        is_vib1 = false;
        is_vib0 = false;
        vib0Vibrate = false;
        changedTime = 0.2f;
		Finish_SE = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneName == "Capture")        //ゲーム本編の場合
        {
            if (is_vib0 == true)
            {
                vib_time += Time.deltaTime;
                if (vib_time > 0.3f)
                {
                    Vib0_Stop();
                }
            }
            else
            {
                vib_time = 0f;
            }
            random_Minzoku = GameObject.Find("Random_Minzoku");
            randm_Toumin = random_Minzoku.GetComponent<Randm_Toumin>();
            popNum = randm_Toumin.POP_NUM();
            //リザルトへの移行の処理
            //タイマーの処理
            //Debug_Time += Time.deltaTime;
            sec -= Time.deltaTime;
            //秒数減らす
            if (min == 0 && sec <= 0f)
            {            //制限時間を迎えたと
                stop = 1;                                               //BGMとWebカメラに終了することを知らせる
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("Stop", 1);
                textFinish.enabled = true;
                Result_Time += Time.deltaTime;
				if (Finish_SE == false) {
					Vib0_Stop ();
					Vib1_Stop ();
					FnishSe.PlayOneShot(FnishSe.clip);
					Debug.Log (FnishSe.clip);
					Finish_SE = true;
				}
                if (Result_Time > changedTime)
                {
                    if (textFinish.color.a == 0f)
                    {
                        textFinish.color = new Color(textFinish.color.r, textFinish.color.g, textFinish.color.b, 1f);
                        changedTime += 0.3f;
                    }
                    else
                    {
                        textFinish.color = new Color(textFinish.color.r, textFinish.color.g, textFinish.color.b, 0f);
                        changedTime += 0.3f;
                    }
                }
                if (Result_Time > 2f)
                {
                    
					//Debug.Log("Score:"+PlayerPrefs.GetFloat("R")+PlayerPrefs.GetFloat("G")+PlayerPrefs.GetFloat("B")+PlayerPrefs.GetFloat("K"));
                    // serialHandler_.LedLighter(0);                   //NEW!!!!9/5にLEDとの通信で追記しました
                    SceneManager.LoadScene("Result1");
                }
                
            }
            else if (sec <= 0)
            {                                       //〇 分 00秒になった時
                min--;                                                  //分を一つ下げて
                sec = 59f;                                              //秒数を60にセット
            }

        }

        if (stop != 1)
        {

            if (PlayerPrefs.GetInt("Step") == 1 || PlayerPrefs.GetInt("Step") == 2)
            {
                /*
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    PlayerPrefs.SetInt("Step", 2);
                }
                */

                if (1f - capNum / (float)popNum <= 0.2f)                    //画面上のゴーストがポップした数の2割以下のとき
                {
                    releaseText.SetActive(true);                             //クリーナー離す用の画像とテキスト表示
                    relCleaner.SetActive(true);
                }

                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    next_step = false;
                    PlayerPrefs.SetInt("Step", 1);
                }

                //サーチフェイズへの移行の処理
                if (Input.GetKeyUp(KeyCode.Return))
                {
                    PlayerPrefs.SetInt("Step", 1);
                    PlayerPrefs.Save();                                     //セーブ
                    Releas = true;                                                  //サーチデバイスを離したフラグを立てる。
                                                                                    //Debug.Log(Releas);
                    Web_cap_ON = 1;                                                 //Webカメラにサーチデバイスを離したことを知らせる
                    PlayerPrefs.SetInt("Web_On", Web_cap_ON);
                    //Webカメラを表示させるために待つ処理
                }



                if (Releas == true)
                {
                    vacuumCleaner.Stop();                                   //掃除機ストップ
                    PlayerPrefs.SetInt("Step", 0);                          //離したら探索シーンに移行
                    PlayerPrefs.Save();                                     //セーブ
                    Dark_Cam.enabled = true;
                    Cursor.enabled = false;
                    Cameras.enabled = false;                                //捕獲シーンのカメラをOFF
                    releaseText.SetActive(false);                           //クリーナー離す用の画像とテキスト消す
                    relCleaner.SetActive(false);
                }

            }
            else
            {
                releaseTime += Time.deltaTime;                              //離した時間更新
                if (releaseTime > 0.83)                                        //1秒間のディレイ
                {
                    Dark_Cam.enabled = false;                               //切り替え
                    Web_Cam.enabled = true;                                 //探索シーンのカメラをON
                    releaseTime = 0.0f;
                    Releas = false;                                         //Releasをfalseに
                }
            }


            PlayerPrefs.SetInt("TimerMinute", min);                    //制限時間の読み込み
            PlayerPrefs.SetFloat("TimerSecond", sec);
        }
        else
        {
            relCleaner.SetActive(false);
            releaseText.SetActive(false);
            cursor.SetActive(false);
            tornado.SetActive(false);
            vacuumCleaner.Stop();
        }


        /*
        if (releaseTime >= 5f) {									//立っている人がボタンを離した(5秒以上)
            PlayerPrefs.SetInt ("TimerMinute", min);				//制限時間をそれぞれに保存
            PlayerPrefs.SetFloat ("TimerSecond", sec);
            PlayerPrefs.Save ();									//セーブ
            SceneManager.LoadScene ("Search");

        } else if (Input.GetKey (KeyCode.Return) == false) {		//立っている人がボタンを離した(0秒 ~ 5秒)
            releaseTime += Time.deltaTime;							//離している時間更新
            ////Debug.Log (releaseTime);
        } else {													//ボタンを押している
            releaseTime = 0f;										//離している秒数を 0 に
        }*/
    }

    public void ReleaseOff()                                        //捕獲シーンに移った際にWebCam_Cap.csでReleasをfalseにさせる
    {
        Releas = false;
        releaseTime = 0f;
    }

    public int CAP_RETURN()                                         //そのシーンで捕まえた数出力用
    {
        return capNum;
    }

    public void CAP_NUM(int cp_num)                                 //そのシーンで捕まえた数読込用
    {
        capNum = cp_num;
    }

    public void Vib0_Start()                //振動(大)：スタート
    {
        vib_time = 0f;
        is_vib0 = true;
        serial = GameObject.Find("Serial");
        serialhandler = serial.GetComponent<SerialHandler>();
        if (vib0Vibrate == false)
        {
            serialhandler.Motor(0, 1);
            vib0Vibrate = true;
        }
    }

    public void Vib0_Stop()                 //振動(大)：ストップ
    {
        serial = GameObject.Find("Serial");
        serialhandler = serial.GetComponent<SerialHandler>();
        serialhandler.Motor_Stop(0);
        is_vib0 = false;
        vib0Vibrate = false;
    }
    
    public void Vib1_Start()                //振動(小)：スタート
    {
        serial = GameObject.Find("Serial");
        serialhandler = serial.GetComponent<SerialHandler>();
        serialhandler.Motor(1, 1);
        is_vib1 = true;
    }

    public void Vib1_Stop()                 //振動(小)：ストップ
    {
        serial = GameObject.Find("Serial");
        serialhandler = serial.GetComponent<SerialHandler>();
        serialhandler.Motor_Stop(1);
        is_vib1 = false;
    }

    public int Finished()
    {
        return stop;
    }
}