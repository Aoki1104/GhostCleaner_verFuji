using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour {

	int num;
	// Use this for initialization
	void Start () {
		num = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.H))
			{
			ScreenCapture.CaptureScreenshot(Application.dataPath + "/savedata"+num+".PNG");
			num++;
		}
	}
}
