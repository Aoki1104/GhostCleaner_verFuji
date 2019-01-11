using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleCountScript : MonoBehaviour {
	//MoveCameraの子オブジェクトに付けること

	[SerializeField]private int syouDis;					//発見数(将軍)
	[SerializeField]private int syouGet;					//捕獲数(将軍)
	[SerializeField]private int devilDis;					//発見数(悪魔)
	[SerializeField]private int devilGet;					//捕獲数(悪魔)
	[SerializeField]private int angelDis;					//発見数(天使)
	[SerializeField]private int angelGet;					//捕獲数(天使)
	DiscountScript discountScript;							//発見数を制御するためのスクリプト
	GetCountScript getCountScript;							//捕獲数を制御するためのスクリプト
	ShougunMoveScript shougunMoveScript;					//頭民を動かすスクリプト(将軍)
	DevilMoveScript devilMoveScript;						//頭民を動かすスクリプト(悪魔)
	AngelMoveScript angelMoveScript;						//頭民を動かすスクリプト(天使)
	GameObject random_Minzoku;
	ToResultData toResultData;
	Animator anim;
    public AudioSource Neeto;                               //頭民を捕まえる時になるネットの音

    public float damage;                                    //ダメージ
    private int stop;                                       //終了したかどうか

	// Use this for initialization
	void Start () {
		random_Minzoku = GameObject.Find ("Random_Minzoku");
		//PlayerPrefs.DeleteAll ();
		toResultData = random_Minzoku.GetComponent<ToResultData> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
        syouDis = PlayerPrefs.GetInt("HeadPDisNum1");       //発見数の読み込み
        syouGet = PlayerPrefs.GetInt("HeadPGetNum1");
        devilDis = PlayerPrefs.GetInt("HeadPDisNum1");      //発見数の読み込み
        devilGet = PlayerPrefs.GetInt("HeadPGetNum1");
    }

	void OnTriggerStay (Collider hit) {
        //Debug.Log ("Ontrriger");
        stop = PlayerPrefs.GetInt("Stop");
        if (stop == 1)
        {

        }
        else {
            if (hit.tag == "Syougun")
            {                           //範囲内に"Syougun"タグを持ったオブジェクトが入っている場合
                if ((discountScript = hit.GetComponent<DiscountScript>()) == null)
                {       //DiscountScriptがついていない場合

                }
                else {                                                                    //ついている場合
                    discountScript.Discount();                                              //Discountスクリプトを消す
                    toResultData.HeadPDis(1);                                               //セーブ
                }
                if (Input.GetKey(KeyCode.Space))
                {                                       //範囲内にあるときにEnterキーを押した場合
                    shougunMoveScript = hit.GetComponent<ShougunMoveScript>();
                    shougunMoveScript.ShougunCaptured(Time.deltaTime * damage);
                }
            }

            if (hit.tag == "Devil")
            {                           //範囲内に"Devil"タグを持ったオブジェクトが入っている場合
                /*
                if ((discountScript = hit.GetComponent<DiscountScript>()) == null)
                {       //DiscountScriptがついていない場合

                }
                else {                                                                  //ついている場合
                    discountScript.Discount();                                              //Discountスクリプトを消す
                    toResultData.HeadPDis(1);                                               //セーブ
                }
                */
                //if (Input.GetKey (KeyCode.Space)) {									//範囲内にあるときにEnterキーを押した場合
                devilMoveScript = hit.GetComponent<DevilMoveScript>();
                devilMoveScript.DevilCaptured(Time.deltaTime * damage);
                //}
            }

            if (hit.tag == "Angel")
            {                           //範囲内に"Angel"タグを持ったオブジェクトが入っている場合
                if ((discountScript = hit.GetComponent<DiscountScript>()) == null)
                {       //DiscountScriptがついていない場合

                }
                else {                                                                  //ついている場合
                    discountScript.Discount();                                              //Discountスクリプトを消す
                    angelDis++;                                                             //発見数増やす
                    toResultData.HeadPDis(0);                                               //セーブ
                    PlayerPrefs.Save();
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {                                   //範囲内にあるときにEnterキーを押した場合
                    if ((getCountScript = hit.GetComponent<GetCountScript>()) == null)
                    {       //DiscountScriptがついていない場合

                    }
                    else {
                        angelMoveScript = hit.GetComponent<AngelMoveScript>();
                        angelMoveScript.AngelCaptured(0.23f);
                        getCountScript.Getcount();
                        angelGet++;                                                             //捕獲数増やす
                        toResultData.HeadPGet(0);                                               //捕獲数セーブ
                        Neeto.PlayOneShot(Neeto.clip);
                        PlayerPrefs.Save();
                    }
                }
            }
        }
	}
    public void peopleGet(int i)
    {
        toResultData.HeadPGet(i);                                               //捕獲数セーブ
    }
}
