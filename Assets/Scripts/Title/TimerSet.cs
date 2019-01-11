using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSet : MonoBehaviour {
	//タイトルにテキトーなオブジェクトに貼り付け
	[SerializeField]public int minute;									//残り時間：分
	[SerializeField]public float second;								//残り時間：秒

	// Use this for initialization
	void Start () {
	
		PlayerPrefs.SetInt ("TimerMinute", minute);		//残り時間をセーブ
		PlayerPrefs.SetFloat ("TimerSecond", second);
        PlayerPrefs.SetInt("Stop", 0);
		PlayerPrefs.Save ();							//セーブ
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
