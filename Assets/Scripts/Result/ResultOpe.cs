using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResultOpe : MonoBehaviour {
    SceneChange script;
	// Use this for initialization
	void Start () {
        script = GetComponent<SceneChange>();
	}
	
	// Update is called once per frame
	void Update () {
        script.SceneChanegeFanc("Title");
    }
}
