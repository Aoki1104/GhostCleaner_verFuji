using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Introduce_Scene_Change : MonoBehaviour {

    //シーン切り替えまでの時間
                          //左からシーン  1  ,2  3 ,4 ,5 ,6 ,7
    private float[] Scene_Change_Time = { 15, 3, 2, 7, 3, 3, 2 };

    private float[] Text_Change_Time = { 5,2,3,3,0,2,   //シーン1
                                        3,0,            //シーン2
                                        2,              //シーン3
                                        2,5,0,          //シーン4
                                        3,3,            //シーン5
                                        3 };            //シーン6

    private int TextNum = 0;

    private float flashSpead = 4f;
    float textFlash;


    //インスペクターでアタッチするゲームオブジェクト関連
    public List<TextMeshProUGUI> Child_Text = new List<TextMeshProUGUI>();
    

    //カメラ関連
    public GameObject FastCamera;
    public GameObject SecondCamera;
    public GameObject BackGroundCamera;
	public GameObject Finish_Camera;

    //オブジェクト関連
    public GameObject Ghost_Cleaner_Particl;    //吸い込みのエフェクト
    public GameObject SpawanGhost;              //生成させるゴースト
    public GameObject Head_Toutch;              //ゴーストクリーナーの操作説明UI
	public GameObject Battery_UI;               //バッテリーのUI

    //テキスト、絵、音
    public Image Ghost_Cleaner_Pic;             //ゴーストクリーナーのイメージ図
    public AudioSource Wind;                    //吸い込み音
    public TextMeshProUGUI demoText;            //「あらすじ」のテキスト
    public GameObject objDemoBack;
    public int demoOrGame;

    void Start () {
       // Head_Toutch.SetActive(false);
       // Ghost_Cleaner_Pic.enabled = false;
		//Battery_UI.SetActive (false);
        for (int i = 0; i < Child_Text.Count; i++) {
            Child_Text[i].enabled = false;
        }

        //タイトルから数秒待って流れてる場合は右上にあらすじを表示
        demoOrGame = PlayerPrefs.GetInt("DemoOrGame");
        if (demoOrGame == 0)
        {
            objDemoBack.SetActive(true);
            demoText.enabled = true;
            textFlash = 0f;
        }
        else
        {
            objDemoBack.SetActive(false);
            demoText.enabled = false;
        }

        //ムービーとテキスト切り替えの処理を開始する
        StartCoroutine("SceneChange");
        StartCoroutine("TextChange");
    }
	
	// Update is called once per frame
	void Update () {

        //表示したあらすじを点滅させる
        if (demoOrGame == 0)
        {
            textFlash += Time.deltaTime * flashSpead;
            demoText.color = new Color(demoText.color.r, demoText.color.g, demoText.color.b, -Mathf.Cos(textFlash) / 2f + 0.5f);
        }
        //ゲームをスタートしてあらすじを流した場合は「LeftShift」を押すことでゲーム本編へ以降する
        else
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                SceneManager.LoadScene("Cleaning_Start");
            }
        }
    }

	//カメラの切り替え、画像の切り替えなどの処理
    private IEnumerator SceneChange()
    {
        int num = 0;
        SpawanGhost.SetActive(true);

        //最初のカメラズームのシーン

        yield return new WaitForSeconds(Scene_Change_Time[num]);
        //ゴーストクリーナーについて説明するシーン
        Ghost_Cleaner_Pic.enabled = true;
        FastCamera.SetActive(false);
        BackGroundCamera.SetActive(true);
        num++;

        yield return new WaitForSeconds(Scene_Change_Time[num]);
		//ゴーストクリーナーを頭に押し当てる様子を説明してるシーン
        Ghost_Cleaner_Particl.SetActive(false);
        Ghost_Cleaner_Pic.enabled = false;
        Head_Toutch.SetActive(true);
        num++;
		yield return new WaitForSeconds(Scene_Change_Time[num]);

		//ゴーストが吸い込まれているシーン~レッツクリーニング！
		Head_Toutch.SetActive(false);
		BackGroundCamera.SetActive(false);
		SecondCamera.SetActive(true);
		Ghost_Cleaner_Pic.enabled = false;
		Wind.PlayOneShot(Wind.clip);
		num++;

        yield return new WaitForSeconds(Scene_Change_Time[num]);

		//バッテリーの説明をしているシーン
		BackGroundCamera.SetActive(true);
		Battery_UI.SetActive (true);
		Wind.Stop ();
		num++;
		yield return new WaitForSeconds(Scene_Change_Time[num]);
		Wind.PlayOneShot(Wind.clip);
		BackGroundCamera.SetActive(false);
		FastCamera.SetActive(true);
		Battery_UI.SetActive (false);
		num++;

        //あらすじの終了時ゲームがスタートでのムービーなのか、デモムービーなのかで判定
		yield return new WaitForSeconds(Scene_Change_Time[num]);
		if (demoOrGame == 0)
        {
            SceneManager.LoadScene("Title");
        }
        else {
			Wind.Stop ();
			SceneManager.LoadScene("Cleaning_Start");
        }

        yield return new WaitForSeconds(Scene_Change_Time[num]);
        
    }

	//テキストの進行処理
    private IEnumerator TextChange()
    {

        //テキストの表示処理
        Child_Text[TextNum].enabled = true; 
        //2行に分けて表示しているところは分けて表示
        if(TextNum == 3 || TextNum == 6 || TextNum == 10 || TextNum == 13)
        {
            Child_Text[TextNum+1].enabled = true;
        }

        //非表示の処理
        yield return new WaitForSeconds(Text_Change_Time[TextNum]);
        Child_Text[TextNum].enabled=false;
        //2行に分けて表示したところは２行目も非表示にして２つ番号を進める
        if (TextNum == 3 || TextNum == 6 || TextNum == 10|| TextNum == 13)
        {
            Child_Text[TextNum+1].enabled = false;
            TextNum++;
        }
        TextNum++; //次の番号へ

        //最後の行でなければ再び処理を開始する
        if (TextNum != 15)
        {
            StartCoroutine("TextChange");
        }
    }
}
