using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour {
	int width;									//スクリーンサイズ(横)
	int height;									//スクリーンサイズ(縦)
	[SerializeField]private float speed;		//カーソルを動かす基準のスピード
	public GameObject peopleCount;				//ゲームオブジェクト「PeopleCount」
	public GameObject cursor;					//ゲームオブジェクト「cursor」
	public Camera caputureCamera;				//捕獲シーンのカメラ
	private Vector3 peopleVec;					//「PeopleCount」のワールド座標
    private bool SE_Play = false;               //効果音を再生したかの判定
    private float SE_Time = 0.0f;               //効果音の再生時間の記録
	Ray ray;
	RaycastHit hit;
	private float distance;		                //rayの長さ
    public GameObject countMove;
    //public AudioSource Vacuum;                //掃除機の音
    public GameObject controler;                //コントローラー
    [SerializeField]
    private float contTime;                     //カーソルを動かした時間

    CaptureManagerScript capScript;
    int finished;                               //時間が終わったかどうか     1なら終了

    private bool Controller;                    //9/7正しくカーソルを動かしたか
    // Use this for initialization
    void Start () {
		width = Screen.width;					//スクリーンサイズ代入
		height = Screen.height;
		speed = 3.5f;                           //速度
		distance = 100f;
        contTime = 0f;
        Controller = false;
        finished = 0;
	}

    // Update is called once per frame
    void Update()
    {
        capScript = GetComponent<CaptureManagerScript>();
        finished = capScript.Finished();
        if (finished != 1)
        {
            hit = new RaycastHit();
            cursor.transform.position = new Vector3(                            //移動
                                                                                //移動範囲指定(横)
                Mathf.Clamp(value: cursor.transform.position.x + Input.GetAxis("Horizontal") * 5f * speed, min: 50f, max: width - 50f),
                //移動範囲指定(縦)
                Mathf.Clamp(value: cursor.transform.position.y + Input.GetAxis("Vertical") * 5f * speed, min: 50f, max: height - 50f),
                0f);
            if (cursor.transform.position.x < width - 370f)
            {
                controler.transform.position = new Vector3(                            //移動
                                                                                       //移動範囲指定(横)
                    cursor.transform.position.x + 260f,
                    //移動範囲指定(縦)
                    cursor.transform.position.y - 41f,
                    0f);
            }
            else
            {
                controler.transform.position = new Vector3(                            //移動
                                                                                       //移動範囲指定(横)
                    cursor.transform.position.x - 260f,
                    //移動範囲指定(縦)
                    cursor.transform.position.y - 41f,
                    0f);
            }

            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && contTime <= 1f)
            {
                contTime += Time.deltaTime;
                if (contTime > 1f)
                {
                    controler.SetActive(false);
                    Controller = true;
                }
            }


            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (Controller == false)
                {
                    controler.SetActive(true);
                }
            }
            /*
            transform.position = new Vector3(
                Mathf.Clamp(Input.mousePosition.x, 50f, width - 50f),
                Mathf.Clamp(Input.mousePosition.y, 50f, height - 50f),
                0f
            );*/
            //ray = caputureCamera.ScreenPointToRay (transform.position);
            Vector3 vec = cursor.transform.position;
            vec.z = 0f;
            ray = caputureCamera.ScreenPointToRay(vec);
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
            if (Physics.Raycast(ray, out hit, distance))
            {
                //Debug.Log (hit.collider.tag);
                if (hit.collider.tag == "Scalp")
                {
                    //peopleCount.transform.position = hit.point;
                    countMove.transform.LookAt(hit.point);
                }
            }
        }
        else
        {
            controler.SetActive(false);
        }
    }
}