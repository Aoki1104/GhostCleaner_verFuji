using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class COMNUM : MonoBehaviour {

    private int num;    //ポート番号
    private int num_tmp; //ポート番号の一時保存
    public TextMeshProUGUI Textnum; //テキスト変更
    public SerialHandler serihan; //SerialHandlerのポートネームに書き込むための宣言
    private string Pnum;
    // Use this for initialization
    void Start () {
        num = 3;    
        num_tmp = num; 
        Textnum.text = num.ToString();
        PlayerPrefs.SetString("PortNum", serihan.portName);
        Debug.Log(serihan.portName);
    }
	
	// Update is called once per frame
	void Update () {
        Textnum.text = num.ToString();

        //右キーで"+1" 左キーで"-1" 0より下にはならず 9より上にはならない
		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.KeypadMultiply) )
        {
            num++;
        }
		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.KeypadDivide))
        {
            num--;
            
        }
        if (num > 9)
        {
            num = 0;
        }
        if (num < 0)
        {
            num = 9;
        }
        if (num_tmp != num)
        {
            serihan.portName = "COM" + num.ToString();
			Debug.Log(serihan.portName);
            num_tmp = num;
            //Debug.Log(serihan.portName);
        }
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.KeypadEnter) )
        {
            PlayerPrefs.SetString("PortNum", serihan.portName);
            PlayerPrefs.Save();

            SceneManager.LoadScene("Title");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(PlayerPrefs.GetString("PortNum"));
        }
    }
}
