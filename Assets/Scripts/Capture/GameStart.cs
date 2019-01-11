using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {

	public float Next_Scene_Time,TextChange1,TextChange2;
	public Text Go, Charge,Compleate;
	public AudioSource Compleate_SE;

	private float Now_Time;
	private bool se_on = false;
	// Use this for initialization
	void Start () {
		Now_Time = 0;
		Go.enabled= false;
		Compleate.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Now_Time += Time.deltaTime;

		if (Now_Time > TextChange1) {
			Charge.enabled = false;
			Compleate.enabled = true;
			if (se_on == false) {
				Compleate_SE.Play ();
				se_on = true;
			}
		}
		if (Now_Time > TextChange2) {
			Compleate.enabled = false;
			Go.enabled = true;

		}
		if (Now_Time > Next_Scene_Time) {
			SceneManager.LoadScene ("Capture");
		}
	}
}
