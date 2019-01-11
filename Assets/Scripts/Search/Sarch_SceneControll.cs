using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sarch_SceneControll : MonoBehaviour {

    //チュートリアルだと使わないやつ
    /*
     private int min;           //ゲーム残り分数    
     private float sec;         //ゲームの残り秒数
     */

    int step=0; //状態管理 : 0:サーチ 1:キャプチャ

    private bool start = false;

    //シーン以降のためのカメラ宣言
    public Camera Cameras; //キャプチャシーンのカメラ
    public Camera Web_Cam; //ウェブカメラのカメラ

    private double Camera_count=0.0f;    //シーン以降のカウント


    // Use this for initialization
    void Start () {
        Web_Cam.enabled = true;        //探索シーンのカメラをオフ
        Cameras.enabled = false;         //捕獲シーンのカメラをオン
    }
	
	// Update is called once per frame
	void Update () {

        step = PlayerPrefs.GetInt("Tutorial_Step");
        //エンターを押してキャプチャシーンへの以降
        if (Input.GetKeyDown(KeyCode.Return) && start ==false && step==0)
        {
            PlayerPrefs.SetInt("Tutorial_Step", 1);
            PlayerPrefs.Save();

            start = true;
        }
        if(start == true)
        {
            Camera_count += Time.deltaTime;

            if (0.3 < Camera_count)
            {
                Web_Cam.enabled = false;        //探索シーンのカメラをオフ
                Cameras.enabled = true;         //捕獲シーンのカメラをオン
                this.gameObject.SetActive(false);
            }
        }

	}
}
