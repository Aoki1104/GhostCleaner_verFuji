﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCountScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Getcount(){
		Destroy (this);								//オブジェクトからスクリプトを消して、PeopleCountScriptでカウントできなくさせる
	}
}