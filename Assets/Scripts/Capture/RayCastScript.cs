using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastScript : MonoBehaviour {
	RaycastHit hit;								//raycastにヒットしたもの
	private int syougunCount;					//捕まえた数(将軍)

	// Use this for initialization
	void Start () {
		//syougunCount = PlayerPrefs.GetInt ("Syougun");	//捕まえた数(将軍)読み込み
	}
	
	// Update is called once per frame
	void Update () {
		if (Physics.Raycast (transform.position, transform.forward, out hit)) {			//正面に何かあるとき
			//Debug.DrawRay(transform.position, transform.forward*10f, Color.red, 0f, true);
			//Debug.Log(hit.collider.tag);
			if (hit.collider.tag == "Syougun" && Input.GetKey (KeyCode.Space)) {		//タグがプレイヤー、かつenter押しているとき
				Debug.Log("頭民をつかまえた！");
				Destroy (hit.collider.gameObject);										//将軍を削除
			}
		}
	}
}
