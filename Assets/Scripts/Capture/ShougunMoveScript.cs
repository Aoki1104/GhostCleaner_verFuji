using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShougunMoveScript : MonoBehaviour {
	//参考URL　→　http://beatdjam.hatenablog.com/entry/2014/10/22/032751

	private GameObject nearObject;				//最も近いオブジェクト
	private float standTime = 0f;				//経過時間
	private float actionTime = 1.5f;			//経過時間
    private bool SE_Play = false;               //シュポ音（捕獲音）を再生したかどうか
    private bool near;							//髪に近いかどうか
	private bool stand = false;					//立ちモーション
	private bool action = false;				//固有モーション
	public bool spacekey = false;				//スペースキーを押したか
	private Animator anim;                      //ついているアニメーター

    public GameObject HPBar;                    //HPバー
    public float MAX_HP = 30f;                  //最大HP
    public float speed = 20f;                    //吸い込むスピード
    [SerializeField]
    private float HP;                           //HP
    public int point;                           //頭民のポイント
    UI_HP ui_HP;
    PeopleCountScript peopleCountScript;
    GetCountScript getCountScript;
    public GameObject canvasHP;					//Canavas(HP用)
    
    private bool inhale = false;                //HPが0以下か

    private int capPpl;                         //チュートリアルで捕まえた頭民の数
    private bool blPpl = false;                 //ポイントの多重加算や効果音の多重再生防止

    public AudioSource hokaku;              //捕獲されたときのシュポの効果音
    public AudioSource Capture_UP;          //捕獲されたとき上に上がるときの効果音
    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
		nearObject = searchTag (gameObject, "Hair");		//近い"Hair"タグ付いてるオブジェクト
		near = false;										//近くない
		anim.Play("shogun_stand");						    //立ちモーション切り替え
        HP = MAX_HP;                                        //HPの初期値設定
        if (SceneManager.GetActiveScene().name == "Capture")
        {
            peopleCountScript = GameObject.Find("CountMove/PeopleCount").GetComponent<PeopleCountScript>();
        }
        float dly = Random.Range(0f, 100f) / 50f;           //モージョンを遅らせる時間
        StartCoroutine(ActionDelay(dly));
        HPBar.SetActive(false);
        Quaternion qua = Quaternion.Euler(0f, Random.Range(-180f, 180f), 0f);      //頭民を回転角度の設定
        gameObject.transform.rotation = qua;                //頭民を回転させる
    }
	
	// Update is called once per frame
	void Update () {
        if (inhale == false)
        {
            canvasHP.transform.rotation = Camera.main.transform.rotation;  //HPがカメラを向くようにする
            #region nearHair
            /*
            nearObject = searchTag(gameObject, "Hair");     //近くの"Hair"タグを探す
            if (near == false && spacekey == false)
            {           //近くない場合
                transform.LookAt(nearObject.transform);     //髪の方を向く
                transform.Translate(transform.forward * 0.05f, nearObject.transform);       //髪の方に歩く
            }
            else if (spacekey == false)
            {
                transform.LookAt(nearObject.transform);     //髪の方を向く
                transform.Translate(Vector3.zero, nearObject.transform);                    //止まる
                if (actionTime >= 0.58f)
                {
                    action = false;
                    if (stand == false)
                    {
                        anim.Play("shogun_stand");              //立ちモーション切り替え
                        stand = true;
                    }
                    standTime += Time.deltaTime;
                    if (standTime >= 0.5f)
                    {
                        actionTime = 0f;
                    }
                }
                else if (standTime >= 0.5f)
                {
                    stand = false;
                    if (action == false)
                    {
                        anim.Play("shogun_action");             //立ちモーション切り替え
                        action = true;
                    }
                    actionTime += Time.deltaTime;
                    if (actionTime >= 0.58f)
                    {
                        standTime = 0f;
                    }
                }
            }
            */
            #endregion
        }
        else
        {
            #region inhalePeople

            gameObject.transform.position =
                    new Vector3(
                        gameObject.transform.position.x,
                        gameObject.transform.position.y + Time.deltaTime * speed,
                        gameObject.transform.position.z
                        );
            /*
            this.gameObject.AddComponent<Rigidbody>();
            Rigidbody rigid = this.gameObject.GetComponent<Rigidbody>();
            rigid.AddForce(new Vector3(0f, 9.81f * speed, 0f), ForceMode.Acceleration);
            this.transform.Rotate(5f, 0f, 0f); 
            */
            #endregion
        }
    }

	GameObject searchTag(GameObject nowObj,string tagName){
		float tmpDis = 0f;
		float nearDis = 0f;
		GameObject targetObj = null;

		foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName)) {
			tmpDis = Vector3.Distance (obs.transform.position, nowObj.transform.position);
			if (nearDis == 0f || nearDis > tmpDis) {
				nearDis = tmpDis;
				targetObj = obs;
				if (nearDis <= 2f) {
					targetObj = gameObject;
					near = true;
				}
			}
		}
		return targetObj;
	}

	public void ShougunCaptured(float damage){
        HPBar.SetActive(true);
        ui_HP = GetComponent<UI_HP>();
        spacekey = true;
        HP = HP - damage;
        ui_HP.HP_Anim(MAX_HP, HP);

        //HPが0のとき、
        if (HP <= 0f)
        {
            
            //現在がキャプチャシーンかチュートリアルシーンか見分ける
            if (SceneManager.GetActiveScene().name == "Capture")
            {
                //キャプチャシーンでの処理
                //ポイントの数分繰り返す
                for (int i = 0; i < point; i++)
                {
                    if (blPpl == false)
                    {
                            blPpl = true;
                            Capture_UP.PlayOneShot(Capture_UP.clip);        //捕獲効果音再生
                            peopleCountScript.peopleGet(1);
                    }

                    
                }

            }
            //チュートリアルシーンでの処理
            else
            {
                //ループ防止
                if (blPpl == false)
                {
                    capPpl = PlayerPrefs.GetInt("Tutorial_Count");  //チュートリアル用の点数付け
                    capPpl++;   
                    PlayerPrefs.SetInt("Tutorial_Count", capPpl);
                    blPpl = true;
                    hokaku.PlayOneShot(hokaku.clip);        //捕獲効果音再生
                }
            }

            //キャプチャ、チュートリアル関係なく捕まえたときの処理
            inhale = true;
            CaptureAction();
        }
    }

    private void CaptureAction()
    {
        anim.Play("shogun_capture");
        StartCoroutine(Captured(1.5f));

    }

    private IEnumerator Captured(float sec){
        yield return new WaitForSeconds(0.45f);
        if (SE_Play == false)
        {
            hokaku.PlayOneShot(hokaku.clip);        //捕獲効果音再生
            SE_Play = true;
        }
        yield return new WaitForSeconds (sec);
        Destroy (this.gameObject);												//将軍消す
	}

    private IEnumerator ActionDelay(float dly)                                  //個体ごとにアクションずらす
    {
        yield return new WaitForSeconds(dly);
        anim.Play("shogun_action");
    }
}
