using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevilMoveScript : MonoBehaviour {
	//参考URL　→　http://beatdjam.hatenablog.com/entry/2014/10/22/032751

	private GameObject nearObject;				//最も近いオブジェクト
	private float standTime = 0f;				//経過時間
	private float actionTime = 1.5f;			//経過時間
    private bool SE_Play = false;               //シュポ音（捕獲時）の効果音を再生したか
	private bool near;							//髪に近いかどうか
	private bool stand = false;					//立ちモーション
	private bool action = false;				//固有モーション
	public bool spacekey = false;				//スペースキーを押したか
	private Animator anim;						//付いているアニメーター
    [SerializeField]
    private float animTime;                     //アニメーション再生時間
    AnimatorStateInfo stateInfo;                //再生時間等を持ってくるためのもの
    float avtionTime;                           //アクションの再生時間
    bool actionMove;                            //アクション中か
    int rndAnim;
    //public GameObject Netto;
    //public GameObject HPBar;                    //HPバー
    public float MAX_HP = 0.001f;               //最大HP
    public float speed = 20f;                   //吸い込むスピード
    public string whatColor;                    //何色か
    private float captureCount;                 //捕まえた数
    [SerializeField]
    private float HP;                           //HP
    //public int point;                           //ゴーストのポイント
    //UI_HP ui_HP;
    PeopleCountScript peopleCountScript;
    GetCountScript getCountScript;
    //public GameObject canvasHP;					//Canavas(HP用)

    private float Net_y=10;
    private bool inhale = false;                //HPが0以下か

    private int capPeople;
    private bool boolPeople;

    private bool moving, walking;               //moving：初動以降かどうか　walking：歩きモーションかどうか
    private float motionTime;                   //モーションを動かす時間
    private float moveTime;                     //モーションの経過時間
    private float posX, posZ;                   //現在のポジション


    public AudioSource hokaku;              //捕獲されたときの効果音
    public AudioSource Capture_UP;          //捕獲されたとき上に上がるときの効果音
    string sceneName;
    private int capNum;
    GameObject captureManagaer;
    CaptureManagerScript capMana;

    // Use this for initialization
    void Start () {
		nearObject = searchTag (gameObject, "Hair");		//近い"Hair"タグ付いてるオブジェクト
		near = false;										//近くない
        HP = MAX_HP;                                        //HPの初期値設定
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Capture")
        {
            peopleCountScript = GameObject.Find("CountMove/PeopleCount").GetComponent<PeopleCountScript>();
        }
        Quaternion qua = Quaternion.Euler(0f, Random.Range(-180f, 180f), 0f);      //ゴーストを回転角度の設定
        gameObject.transform.rotation = qua;            //ゴーストを回転させる
        //HPBar.SetActive(false);

        //ゴーストを動かすための準備
        #region MoveSetup
        moving = false;                                 //初動に入っていない
        walking = false;                                //歩いていない
        moveTime = 0f;                                  //モーション経過時間の初期設定

        #endregion
		anim = GetComponent<Animator> ();               //付いているアニメーター
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);    //アニメーター情報(読み取り専用)
		anim.Play("ghost_stop_Ver2");						    //立ちモーション切り替え
        float dly = Random.Range(0.5f, 5f);               //モージョンを遅らせる時間
        StartCoroutine(ActionDelay(dly));
    }

    // Update is called once per frame
    void Update ()
    {
        //canvasHP.transform.rotation = Camera.main.transform.rotation;  //HPがカメラを向くようにする
        if (inhale == false)
        {
            #region nearHair
            /*
                nearObject = searchTag(gameObject, "Hair");     //近くの"Hair"タグを探す
                if (near == false && spacekey == false)
                {           //近くない場合
                    transform.LookAt(nearObject.transform);     //髪の方を向く
                    transform.Translate(transform.forward * 0.05f, nearObject.transform);       //髪の方に歩く
                    action = false;
                }
                else if (spacekey == false)
                {
                    transform.LookAt(nearObject.transform);     //髪の方を向く
                    transform.Translate(Vector3.zero, nearObject.transform);                    //止まる

                    if (action == false)
                    {
                        anim.Play("devil_action");              //固有モーション切り替え
                        action = true;
                    }
                }*/ 
            #endregion
            Netto_Transform();
            animTime += Time.deltaTime;

            stateInfo = anim.GetCurrentAnimatorStateInfo(0);    //アニメーター情報(読み取り専用)
            if (animTime > stateInfo.length)
            {
                while(animTime <= stateInfo.length)
                {
                    animTime -= stateInfo.length;
                }
            }
            //ゴーストを動かす
            #region PeopleMove
            if (moving == true || spacekey != true)
            {
                if (walking == true)
                {
                    moveTime += Time.deltaTime;         //モーション経過時間を更新
                    if (moveTime < motionTime)          //モーション経過時間がモーション時間を超えていなかったら
                    {
                        transform.Translate(0f, 0f, Random.Range(0.02f, 0.05f));            //正面に動かす
                        posX = transform.position.x;                        //現在のx座標を代入
                        posZ = transform.position.z;                        //現在のz座標を代入
                        if (posX >= -20f && posX <= 20f && posZ >= -7f && posZ <= 12f)    //描写範囲に入っている場合
                        {

                        }
                        else                                                            //描写範囲から出ている場合
                        {
                            transform.LookAt(new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-7f, 12f)));
                            Quaternion thisQua = transform.rotation;
                            thisQua.x = 0f;
                            thisQua.z = 0f;
                            transform.rotation = thisQua;
                        }
                    }
                    else
                    {
                        motionTime = Random.Range(3f, 8f);
                        moveTime = 0f;
                        anim.Play("ghost_stop_Ver2", 0, animTime);
                        walking = false;
                    }
                }
                else
                {
                    moveTime += Time.deltaTime;
                    if (stateInfo.normalizedTime > 0.995f)    //再生時間がアニメーション終了時間の99%を越したら
                    {
                        if (moveTime >= motionTime)                 //モーション経過時間がモーション時間を超えたら
                        {
                            if (!actionMove)                        //アニメーション(アクション)再生していないとき
                            {
                                Quaternion qua = Quaternion.Euler(0f, Random.Range(-180f, 180f), 0f);
                                transform.rotation = qua;
                                motionTime = Random.Range(3f, 8f);
                                moveTime = 0f;
                                anim.Play("ghost_move");
                                walking = true;
                            }
                        }
                        if (!actionMove)                        //アニメーション(アクション)再生していないとき
                        {
                            if ((rndAnim = Random.Range(0, 200)) < 199)
                            {
                                anim.Play("ghost_stop_Ver2");
                                actionMove = false;
                            }
                            else
                            {
                                anim.Play("ghost_action_Ver3");
                                actionMove = true;
                            }
                            //Debug.Log("rndAnim : " + rndAnim);
                        }
                        else                                    //アニメーション(アクション)再生しているとき
                        {
                            actionMove = false;
                        }
                    }
                    
                }
            }
            #endregion
        }
        else
        {

            #region inhalePeople
            /*
            gameObject.transform.position =
                    new Vector3(
                        gameObject.transform.position.x,
                        gameObject.transform.position.y + Time.deltaTime * speed,
                        gameObject.transform.position.z
                        );
                        */
            /*
            this.gameObject.AddComponent<Rigidbody>();
            Rigidbody rigid = this.gameObject.GetComponent<Rigidbody>();
            rigid.AddForce(new Vector3(0f, 9.81f * speed, 0f), ForceMode.Acceleration);
            this.transform.Rotate(5f, 0f, 0f); 
            */
            #endregion
        }

		if (spacekey == true) {
			this.transform.Translate (0, 0.4f, 0);
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

	public void DevilCaptured(float damage)
    {
        //HPBar.SetActive(true);
        ////Debug.Log(HP);
        //ui_HP = GetComponent<UI_HP>();
        spacekey = true;
        HP = HP - damage;
        //ui_HP.HP_Anim(MAX_HP, HP);
        if (HP <= 0f)
        {
            if (sceneName == "Capture")
            {
                if (boolPeople == false)
                {
                    boolPeople = true;
                    Capture_UP.PlayOneShot(Capture_UP.clip);        //捕獲効果音再生
                    /*
                    peopleCountScript.peopleGet(1);
                    */
                    //捕獲数カウント用
                    #region CaptureCount
                    captureCount = PlayerPrefs.GetFloat(whatColor) + 1f;              //捕獲数にカウント
                    PlayerPrefs.SetFloat(whatColor, captureCount);                 //捕獲数セーブ
                    PlayerPrefs.Save();
                    captureManagaer = GameObject.Find("CaptureManager");
                    capMana = captureManagaer.GetComponent<CaptureManagerScript>();
                    capNum = capMana.CAP_RETURN() + 1;                              //そのシーンで捕まえた数読み込み
                    capMana.CAP_NUM(capNum);                                        //そのシーンで捕まえた数カウント
                    #endregion
					//Debug.Log ("Score:" + (PlayerPrefs.GetFloat ("R") + PlayerPrefs.GetFloat ("G") + PlayerPrefs.GetFloat ("B") + PlayerPrefs.GetFloat ("K")));
                }
            }
            else
            {
                if (boolPeople == false)
                {
                    
                    capPeople = PlayerPrefs.GetInt("Tutorial_Count");
                    capPeople++;
                    PlayerPrefs.SetInt("Tutorial_Count", capPeople);
                    boolPeople = true;
                    hokaku.PlayOneShot(hokaku.clip);        //捕獲効果音再生
                    //Debug.Log("cap");
                }
            }
            inhale = true;
            CaptureAction();
        }
    }

    private void CaptureAction()
    {
        anim.Play("ghost_capture");
        StartCoroutine(Captured(0.5f));
    }

    private IEnumerator Captured(float sec){
        yield return new WaitForSeconds(0.45f);
        if (SE_Play == false)
        {
            hokaku.PlayOneShot(hokaku.clip);        //捕獲効果音再生
            SE_Play = true;
        }
        yield return new WaitForSeconds (sec);
        capMana.Vib0_Start();
		yield return new WaitForSeconds (0.3f);
        capMana.Vib0_Stop();
        Destroy (gameObject);												    //悪魔消す
	}

    private IEnumerator ActionDelay(float dly)                                  //個体ごとにアクションずらす
    {
        yield return new WaitForSeconds(dly);
        animTime = Random.Range(0f, 5f);
        anim.Play("ghost_move", 0, animTime);
        motionTime = Random.Range(3f, 5f);                                      //モーション時間の設定
        moving = true;                                                          //初動を始めた
        walking = true;                                                         //歩き始めた
    }

    void Netto_Transform()
    {
        //new Vector3(0,Netto.transform.position.y - 1,0) ;
    }
}
