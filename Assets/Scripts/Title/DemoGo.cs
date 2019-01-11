using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoGo : MonoBehaviour {
    //　0：デモへ,　1：ゲームへ
    SceneChange sceneChange;
    public float titleTime = 0f, titleMaxTime = 26.5f;

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("DemoOrGame", 0);
        sceneChange = GetComponent<SceneChange>();
    }
	
	// Update is called once per frame
	void Update () {
        titleTime += Time.deltaTime;
        if (Input.anyKeyDown)
        {
            titleTime = 0f;
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayerPrefs.SetInt("DemoOrGame", 1);
        }
        if (titleTime > titleMaxTime)
        {
            titleTime = 0f;
            sceneChange.GoToDemo();
            sceneChange.SceneChanegeFanc("introduction");
        }
    }
}
