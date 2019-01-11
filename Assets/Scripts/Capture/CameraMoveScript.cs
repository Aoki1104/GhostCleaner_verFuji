using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMoveScript : MonoBehaviour {
	//CaptureManagerに貼り付ける

	public GameObject cameras;												//カメラの親オブジェクト
	public Camera zoomCamera;												//ズーム用カメラ(カメラ版)
	public GameObject zoomObject;											//ズーム用カメラ(オブジェクト版)
	public GameObject moveCamera;											//ズーム中動かす用カメラ
	public GameObject peopleCount;											//頭民カウント用
	public GameObject cursor;												//カーソル
    public float spead = 1f;                                                //カーソルのスピード調整

	int width;																//スクリーンサイズ(横)
	int height;																//スクリーンサイズ(縦)

    
	// Use this for initialization
	void Start () {
		zoomCamera = zoomObject.GetComponent<Camera> ();
		moveCamera.SetActive (false);										//ズーム中動かす用カメラ非表示
		peopleCount.SetActive(false);

		width = Screen.width;												//スクリーンサイズ代入
		height = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		
		cursor.transform.position = new Vector3 (							//移動
			//移動範囲指定(横)
			Mathf.Clamp (value : cursor.transform.position.x + Input.GetAxis ("Horizontal") * 5f * spead, min : 50f, max : width - 50f), 
			//移動範囲指定(縦)
			Mathf.Clamp (value : cursor.transform.position.y + Input.GetAxis ("Vertical") * 5f * spead, min : 50f, max : height - 50f), 
			0f);

		cameras.transform.position = new Vector3 (						//Cameras位置移動
			(cursor.transform.position.x - width / 2f) / width * 30f, 
			20f,
			(cursor.transform.position.y - height / 2f) / height * 20f - 11f);

		if (Input.GetKeyDown (KeyCode.Return)) {								//Enterキー離したとき
			cursor.transform.position = new Vector3 (width / 2f, height / 2f, 0f);	//カーソルを中央に持ってくる
			cameras.transform.position = new Vector3 (0f, 20f, -11f);		//Cameras位置戻す
		}

		if (Input.GetKey (KeyCode.LeftShift)) {								//spaceボタン押したとき
			cameras.transform.position = new Vector3 (						//Cameras位置移動
				(cursor.transform.position.x - width / 2f) / width * 30f, 
				20f,
				(cursor.transform.position.y - height / 2f) / height * 20f - 11f);
			
			zoomCamera.fieldOfView = Mathf.Clamp (zoomCamera.fieldOfView - 10f, 12f, 45f);
			
			if (zoomCamera.fieldOfView == 12f) {							//ズーム最大時
				cursor.SetActive (false);									//カーソル非表示
				zoomObject.SetActive (false);								//ズーム用カメラ非表示
				moveCamera.SetActive (true);								//ズーム中動かす用カメラ表示
				peopleCount.SetActive(true);

				moveCamera.transform.position = new Vector3 (				//MoveCameraを動かす
					cameras.transform.position.x,
					4f,
					cameras.transform.position.z + 8.8f
				);
			}
		} else {
			cursor.SetActive (true);										//カーソル表示
			moveCamera.SetActive (false);									//ズーム用カメラ非表示
			peopleCount.SetActive(false);
			zoomObject.SetActive (true);									//ズーム中動かす用カメラ表示
			cameras.transform.position = new Vector3 (0f, 20f, -11f);		//Cameras位置戻す
			zoomCamera.fieldOfView = Mathf.Clamp (zoomCamera.fieldOfView + 10f, 12f, 45f);	//元のズームに戻す
			PlayerPrefs.SetInt("Status",0);
		}
        if (Input.GetKeyUp(KeyCode.Return))
        {
          
        }

    }
}
