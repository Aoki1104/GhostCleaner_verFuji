using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WebCam_Cap : MonoBehaviour
{
    [SerializeField]
    private int min;
    [SerializeField]
    private float sec;                              //制限時間：秒

    public Camera Capture_Camera;    //キャプチャシーンのカメラ
    public Camera Sarch_Camera; //サーチシーンのカメラ
    public Camera Dark_Cam;     //キャプチャシーンとサーチシーンの移行時に間に挟むカメラ

    public Image kami, yubi,head;
    public Text Advice;
    private bool start = false;
    float Debug_Time = 0.0f; //デバッグ用のタイマー
    public GameObject capMana,controller;                      //キャプチャーマネージャー
    CaptureManagerScript capScript;                 //CaptureManagerScript
    public float Image_Desplay_time;
    float time;
    int finished;

    void Start()
    {
        PlayerPrefs.SetInt("Step", 0);
        Dark_Cam.enabled = false;
        Capture_Camera.enabled = false;      //捕獲シーンのカメラをオフ
        controller.SetActive(false);
        Sarch_Camera.enabled = true;        //初回はサーチシーンをON
        finished = 0;
    }

    void Update()
    {
        
        Debug_Time += Time.deltaTime;           //デバッグ用のタイマー
        int step = PlayerPrefs.GetInt("Step");  //状態管理： 0: サーチ 1:キャプチャ
        capScript = capMana.GetComponent<CaptureManagerScript>();       //CaptureManagerScriptの読み取り
        finished = capScript.Finished();
        if (finished != 1)
        {
            if (step == 0)
            {
                //頭に当ててねのテキストと人のイメージ画像の表示遅延処理
                time += Time.deltaTime;
                if (time >= Image_Desplay_time)
                {
                    head.enabled = true;
                    Advice.enabled = true;
                }
                
                kami.enabled = true;
                //yubi.enabled = true;
                controller.SetActive(false);
                
                //Enterキーでキャプチャ
                if (Input.GetKey(KeyCode.Return))
                {
                   
                    time = 0; //頭に当ててねのテキストと人のイメージ画像の表示遅延処理　時間のリセット
                    PlayerPrefs.SetInt("Step", 1);              //捕獲への移行フラグオン
                    PlayerPrefs.Save();
                    head.enabled = false;
                    Advice.enabled = false;
                    kami.enabled = false;
                    //  yubi.enabled = false;
                    Sarch_Camera.enabled = false;        //探索シーンのカメラをオフ
                    Dark_Cam.enabled = false;
                    Capture_Camera.enabled = true;         //捕獲シーンのカメラをオン
                    capScript.ReleaseOff();                                         //CaptureManagerScriptのReleasをfalseに
                    AudioSource vacuum = capMana.GetComponent<AudioSource>();
                    vacuum.Play();                                                  //あぁ～！掃除機の音ォ～！！
                }
            }
        }
        else
        {
            kami.enabled = false;
            head.enabled = false;
            Advice.enabled = false;
        }
    }
}