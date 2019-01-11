using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//シリアル通信サンプルプログラム
//PキーでモーターON,OキーでOFF

//モーター制御: SerialHandler_.Motor()
//引数説明
//    第一引数: モーター番号　[ 0 or 1 ]
//    第二引数: 回転方向      [ 0 or 1: 回転方向指定 || 2: モーター停止※モーターを止める関数があるので基本使わない]
//    第三引数: 回転強度　    [ 0～255の値で回転強度を指定 ]

//モーターの駆動を止める: serialHandler_.Motor_Stop()
//引数説明
//    第一引数: モーター番号　[ 0 or 1 ]
//　大きいモーター: 0
// 　小さいモーター:1

public class Serial_Example : MonoBehaviour {

    public SerialHandler serialHandler_; //InspectorでSerialHandler.csの付いたオブジェクトを指定
    public int motorPower_ = 100;  //モーター駆動時のパワーを決める引数
    private string Scenename;
    GameObject captureManager;
    CaptureManagerScript capManaScr;
    void Start()
    {
        //シリアルポート内容を見るための実験の残骸です
        //serialHandler_.OnDataReceived += OnDataReceived;
        Scenename = SceneManager.GetActiveScene().name;
        captureManager = GameObject.Find("CaptureManager");
    }

    void Update()
    {
        if (Scenename == "Capture")
        {
            capManaScr = captureManager.GetComponent<CaptureManagerScript>();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                capManaScr.Vib1_Start();
                //Debug.Log("on");    
            }
            if (Input.GetKeyUp(KeyCode.Return))
            {
                capManaScr.Vib1_Stop();
                //Debug.Log("off");
            }
        }


        if (Scenename == "COM")
        {
			// 1: 大モーター
            // 0: 小モーター
            if (Input.GetKeyDown(KeyCode.P))
            {
                serialHandler_.Motor(1, 1);
                //Debug.Log("小モーター：on");
            }
				
            if (Input.GetKeyDown(KeyCode.I))
            {
                serialHandler_.Motor(0, 1);
                //Debug.Log("大モーター：on");
            }
            if (Input.GetKeyUp(KeyCode.L))
            {
                serialHandler_.Motor_Stop(1);
                //Debug.Log("小モーター：off");
            }
            if (Input.GetKeyUp(KeyCode.K))
            {
                serialHandler_.Motor_Stop(0);
                //Debug.Log("大モーター：off");
            }
        }
       
    }

    //シリアルポート内容を見るための実験の残骸です
    //void OnDataReceived(string message)
    //{
    //    var data = message.Split(new string[] { "\t" }, System.StringSplitOptions.None);
    //    if (data.Length < 2) return;

    //    try
    //    {

    //    }catch(System.Exception e)
    //    {
    //        //Debug.LogWarning(e.Message);
    //    }
    //}
}
