using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


public class SarchScene_Migration : MonoBehaviour
{
    [SerializeField]
    private int min;
    [SerializeField]
    private float sec;                              //制限時間：秒

    public Camera Cameras; //キャプチャシーンのカメラ
    public Camera Web_Cam; //ウェブカメラのカメラ
    public Camera Dark_Cam;

    private bool start = false;
    float Debug_Time = 0.0f; //デバッグ用のタイマー
    void Start()
    {
        Web_Cam.enabled = true;
    }

    void Update()
    {
        Debug_Time += Time.deltaTime; //デバッグ用のタイマー
        int step = PlayerPrefs.GetInt("Step");                      //状態管理： 0: サーチ 1:キャプチャ



        if (start == true)
        {
            //特定のボタンを押した時に現在の平均RGBをr,g,bに保存してCaptureシーンへ
            if (Input.GetKeyDown(KeyCode.Return))
            {
                PlayerPrefs.SetInt("Tutorial_Step", 1);              //捕獲への移行フラグオン
                PlayerPrefs.Save();
                Web_Cam.enabled = false;        //探索シーンのカメラをオフ
                Dark_Cam.enabled = false;
                Cameras.enabled = true;         //捕獲シーンのカメラをオン
            }
        }

    }
}