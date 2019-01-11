using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Introduce_Scene_Change : MonoBehaviour {

    //シーン切り替えまでの時間：インスペクターで指定
    [SerializeField]
    private List<float> Scene_Change_Time = new List<float>();
    [SerializeField]
    private List<float> Text_Change_Time = new List<float>();
    private int TextNum = 0;

    //インスペクターでアタッチするゲームオブジェクト関連
    public List<TextMeshProUGUI> Child_Text = new List<TextMeshProUGUI>();
    //カメラ
    public GameObject FastCamera;
    public GameObject SecondCamera;
    public GameObject BackGroundCamera;
	public GameObject Finish_Camera;

    public GameObject Ghost_Cleaner_Particl;
    public GameObject SpawanGhost;
    public GameObject Head_Toutch;
	public GameObject Battery_UI;

    public Image Ghost_Cleaner_Pic;
   
    public AudioSource Wind;
    public TextMeshProUGUI End;
    public TextMeshProUGUI demoText;
    public GameObject objDemoBack;
    Demo_Back demo_Back;
    public int demoOrGame;
    float textFlash;
    public float flashSpead = 4f;
    // Use this for initialization
    void Start () {
        Head_Toutch.SetActive(false);
        Ghost_Cleaner_Pic.enabled = false;
        End.enabled = false;
		Battery_UI.SetActive (false);
        for (int i = 0; i < Child_Text.Count; i++) {
            Child_Text[i].enabled = false;
        }
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
        StartCoroutine("SceneChange");
        StartCoroutine("TextChange");
    }
	
	// Update is called once per frame
	void Update () {
        if (demoOrGame == 0)
        {
            textFlash += Time.deltaTime * flashSpead;
            demoText.color = new Color(demoText.color.r, demoText.color.g, demoText.color.b, -Mathf.Cos(textFlash) / 2f + 0.5f);
            //Debug.Log(demoText.color.a);
        }
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
        //Debug.Log("MoveCamera");
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
		yield return new WaitForSeconds(Scene_Change_Time[num]);
		if (demoOrGame == 0)
        {
            demo_Back = objDemoBack.GetComponent<Demo_Back>();
            demo_Back.BackTitle();
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
		Debug.Log (TextNum);
        Child_Text[TextNum].enabled = true;
        if(TextNum == 3 || TextNum == 6 || TextNum == 10 || TextNum == 13)
        {
            Child_Text[TextNum+1].enabled = true;
        }
        yield return new WaitForSeconds(Text_Change_Time[TextNum]);
        Child_Text[TextNum].enabled=false;
        if (TextNum == 3 || TextNum == 6 || TextNum == 10|| TextNum == 13)
        {
            Child_Text[TextNum+1].enabled = false;
            TextNum++;
        }
        TextNum++;
        if (TextNum != 15)
        {
            StartCoroutine("TextChange");
        }
    }
}
