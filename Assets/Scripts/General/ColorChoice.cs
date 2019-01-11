using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChoice : MonoBehaviour {

    /*
            キーボードの数字キーに対応している色
    0:赤
    1:橙
    2:黄
    3:緑(原色)
    4:青
    5:紫
    6:水
    7:灰
    8:黒
    9:白
    */

    [SerializeField]
    private bool hairOrGhost;

    /*
    false : 髪の色指定
    true : ゴースト出現割合指定 
    */

	// Use this for initialization
	void Start () {
        hairOrGhost = true;
        //髪の色とゴースト出現割合の初期設定
        #region FirstSet
        PlayerPrefs.SetFloat("HairRed", 0f);
        PlayerPrefs.SetFloat("HairGreen", 0f);
        PlayerPrefs.SetFloat("HairBlue", 0f);
        PlayerPrefs.SetInt("GhostRed", Random.Range(0, 255));
        PlayerPrefs.SetInt("GhostGreen", Random.Range(0, 255));
        PlayerPrefs.SetInt("GhostBlue", Random.Range(0, 255)); 
        #endregion
    }
	
	// Update is called once per frame
	void Update () {
        #region HairOrGhost
        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Plus))         //プラスを押したら
        {
            hairOrGhost = true;                             //出現割合選択に
            Debug.Log("ModeGhost");
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus))       //マイナスを押したら
        {
            hairOrGhost = false;                            //髪の色選択に
            Debug.Log("ModeHair");
        }
        #endregion
        
        #region HairChoice
        if (!hairOrGhost)                                   //髪の色の選択
        {
            if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))          //赤
            {
                PlayerPrefs.SetFloat("HairRed", 255f);
                PlayerPrefs.SetFloat("HairGreen", 0f);
                PlayerPrefs.SetFloat("HairBlue", 0f);
                Debug.Log("Pushed 0");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))     //橙
            {
                PlayerPrefs.SetFloat("HairRed", 255f);
                PlayerPrefs.SetFloat("HairGreen", 183f);
                PlayerPrefs.SetFloat("HairBlue", 76f);
                Debug.Log("Pushed 1");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))     //黄
            {
                PlayerPrefs.SetFloat("HairRed", 255f);
                PlayerPrefs.SetFloat("HairGreen", 255f);
                PlayerPrefs.SetFloat("HairBlue", 0f);
                Debug.Log("Pushed 2");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))     //緑(原色)
            {
                PlayerPrefs.SetFloat("HairRed", 0f);
                PlayerPrefs.SetFloat("HairGreen", 255f);
                PlayerPrefs.SetFloat("HairBlue", 0f);
                Debug.Log("Pushed 3");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))     //青
            {
                PlayerPrefs.SetFloat("HairRed", 0f);
                PlayerPrefs.SetFloat("HairGreen", 0f);
                PlayerPrefs.SetFloat("HairBlue", 255f);
                Debug.Log("Pushed 4");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))     //紫
            {
                PlayerPrefs.SetFloat("HairRed", 128f);
                PlayerPrefs.SetFloat("HairGreen", 0f);
                PlayerPrefs.SetFloat("HairBlue", 128f);
                Debug.Log("Pushed 5");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))     //水
            {
                PlayerPrefs.SetFloat("HairRed", 0f);
                PlayerPrefs.SetFloat("HairGreen", 255f);
                PlayerPrefs.SetFloat("HairBlue", 255f);
                Debug.Log("Pushed 6");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))     //灰
            {
                PlayerPrefs.SetFloat("HairRed", 128f);
                PlayerPrefs.SetFloat("HairGreen", 128f);
                PlayerPrefs.SetFloat("HairBlue", 128f);
                Debug.Log("Pushed 7");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))     //黒
            {
                PlayerPrefs.SetFloat("HairRed", 0f);
                PlayerPrefs.SetFloat("HairGreen", 0f);
                PlayerPrefs.SetFloat("HairBlue", 0f);
                Debug.Log("Pushed 8");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))     //白
            {
                PlayerPrefs.SetFloat("HairRed", 255f);
                PlayerPrefs.SetFloat("HairGreen", 255f);
                PlayerPrefs.SetFloat("HairBlue", 255f);
                Debug.Log("Pushed 9");
            }
        }
        #endregion

        #region GhostChoice
        if (hairOrGhost)                                    //ゴーストの出現割合の選択
        {
            if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))          //赤
            {
                PlayerPrefs.SetInt("GhostRed", 255);
                PlayerPrefs.SetInt("GhostGreen", 0);
                PlayerPrefs.SetInt("GhostBlue", 0);
                Debug.Log("Pushed 0");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))     //橙
            {
                PlayerPrefs.SetInt("GhostRed", 255);
                PlayerPrefs.SetInt("GhostGreen", 183);
                PlayerPrefs.SetInt("GhostBlue", 76);
                Debug.Log("Pushed 1");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))     //黄
            {
                PlayerPrefs.SetInt("GhostRed", 255);
                PlayerPrefs.SetInt("GhostGreen", 255);
                PlayerPrefs.SetInt("GhostBlue", 0);
                Debug.Log("Pushed 2");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))     //緑(原色)
            {
                PlayerPrefs.SetInt("GhostRed", 0);
                PlayerPrefs.SetInt("GhostGreen", 255);
                PlayerPrefs.SetInt("GhostBlue", 0);
                Debug.Log("Pushed 3");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))     //青
            {
                PlayerPrefs.SetInt("GhostRed", 0);
                PlayerPrefs.SetInt("GhostGreen", 0);
                PlayerPrefs.SetInt("GhostBlue", 255);
                Debug.Log("Pushed 4");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))     //紫
            {
                PlayerPrefs.SetInt("GhostRed", 128);
                PlayerPrefs.SetInt("GhostGreen", 0);
                PlayerPrefs.SetInt("GhostBlue", 128);
                Debug.Log("Pushed 5");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))     //水
            {
                PlayerPrefs.SetInt("GhostRed", 0);
                PlayerPrefs.SetInt("GhostGreen", 255);
                PlayerPrefs.SetInt("GhostBlue", 255);
                Debug.Log("Pushed 6");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))     //灰
            {
                PlayerPrefs.SetInt("GhostRed", 128);
                PlayerPrefs.SetInt("GhostGreen", 128);
                PlayerPrefs.SetInt("GhostBlue", 128);
                Debug.Log("Pushed 7");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))     //黒
            {
                PlayerPrefs.SetInt("GhostRed", 0);
                PlayerPrefs.SetInt("GhostGreen", 0);
                PlayerPrefs.SetInt("GhostBlue", 0);
                Debug.Log("Pushed 8");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))     //白
            {
                PlayerPrefs.SetInt("GhostRed", 255);
                PlayerPrefs.SetInt("GhostGreen", 255);
                PlayerPrefs.SetInt("GhostBlue", 255);
                Debug.Log("Pushed 9");
            }
        } 
        #endregion
    }
}
