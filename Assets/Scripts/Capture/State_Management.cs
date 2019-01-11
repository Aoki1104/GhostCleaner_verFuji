using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class State_Management : MonoBehaviour {

    //サーチシーンとキャプチャシーンの状態管理

    [SerializeField]int Step=0;       //0:サーチシーン 1:キャプチャシーン
                                    // Use this for initialization
    int next_step;
    private int peopleNum;          //頭民数を一時的に格納する変数
    private string sceneName;
    GameObject serial;
    Vib_Stop vib_stop;
    public GameObject captureManager;
    CaptureManagerScript capScript;
    int finished;

    void Start () {
        Step = 0; //最初はサーチシーン
        PlayerPrefs.SetInt("Step", Step);
        PlayerPrefs.Save();
        sceneName = SceneManager.GetActiveScene().name;
        finished = 0;
    }

    // Update is called once per frame
    void Update()
    {
        capScript = captureManager.GetComponent<CaptureManagerScript>();
        finished = capScript.Finished();
        if (finished != 1)
        {
            //今の状態とPlayerPrefsが持っているStepの状態が異なっている時に処理される
            /*next_step = PlayerPrefs.GetInt("Step");
            if (Step != next_step) 
            {
                Step = PlayerPrefs.GetInt("Step");
                switch (Step)
                {
                    case 0:*/
            GameObject[] Hair = GameObject.FindGameObjectsWithTag("Hair");
            GameObject[] Syougun = GameObject.FindGameObjectsWithTag("Syougun");
            GameObject[] Devil = GameObject.FindGameObjectsWithTag("Devil");
            GameObject[] Angel = GameObject.FindGameObjectsWithTag("Angel");
            GameObject[] None = GameObject.FindGameObjectsWithTag("None");
            if (sceneName == "Capture")
            {
                if (!Input.GetKey(KeyCode.Return))
                {
                    for (int i = 0; i < Hair.Length; i++)
                    {
                        Destroy(Hair[i]);
                    }
                    for (int i = 0; i < Syougun.Length; i++)
                    {
                        Destroy(Syougun[i]);
                    }
                    for (int i = 0; i < Devil.Length; i++)
                    {
                        if (Devil[i].name == "Red(Clone)")
                        {
                            peopleNum = PlayerPrefs.GetInt("Red");              //残り頭民数に足す
                            PlayerPrefs.SetInt("Red", peopleNum + 1);           //残り頭民数更新(red)
                                                                                //Debug.Log("RedBack");                               //返却(red)
                        }
                        else if (Devil[i].name == "Green(Clone)")
                        {
                            peopleNum = PlayerPrefs.GetInt("Green");            //残り頭民数に足す
                            PlayerPrefs.SetInt("Green", peopleNum + 1);         //残り頭民数更新(green)
                                                                                //Debug.Log("GreenBack");                             //返却(green)
                        }
                        else if (Devil[i].name == "Blue(Clone)")
                        {
                            peopleNum = PlayerPrefs.GetInt("Blue");             //残り頭民数に足す
                            PlayerPrefs.SetInt("Blue", peopleNum + 1);          //残り頭民数更新(blue)
                                                                                //Debug.Log("BlueBack");                              //返却(blue)
                        }
                        else
                        {
                            peopleNum = PlayerPrefs.GetInt("Black");            //残り頭民数に足す
                            PlayerPrefs.SetInt("Black", peopleNum + 1);         //残り頭民数更新(black)
                                                                                //Debug.Log("BlackBack");                             //返却(black)
                        }
                        Destroy(Devil[i]);
                    }
                    for (int i = 0; i < None.Length; i++)
                    {
                        Destroy(None[i]);
                    }
                    for (int i = 0; i < Angel.Length; i++)
                    {
                        Destroy(Angel[i]);
                    }
                    //serial = GameObject.Find("Serial");
                    //vib_stop = serial.GetComponent<Vib_Stop>();
                    //vib_stop.VibStop();
                }
            }/*
                    break;
                case 1:
                    break;
                case 2:
                    
                    break;
            }
        }*/
        }
    }
    

}