using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//タイトル画面で数秒待った際に流れるオープニングシーンでのみ使える機能
//ESCを押してタイトル画面に戻るスクリプト
public class Demo_Back : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            BackTitle();
        }
	}

    public void BackTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
