using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDelete : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.DeleteKey("R");
        PlayerPrefs.DeleteKey("G");
        PlayerPrefs.DeleteKey("B");
        PlayerPrefs.DeleteKey("K");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
