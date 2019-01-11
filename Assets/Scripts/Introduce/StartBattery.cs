using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattery : MonoBehaviour {
	[SerializeField]
	float MAX = 2.5f;
	float Current;
	[SerializeField]
	GameObject UI;
	UI_Battery denti;
	// Use this for initialization
	void Start () {
		Current = 0;
		denti = UI.GetComponent<UI_Battery>();
	}

	// Update is called once per frame
	void Update () {
		Current = Current + Time.deltaTime;
		denti.HP_Anim (MAX,Current);
	}
}
