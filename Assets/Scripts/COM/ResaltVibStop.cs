using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResaltVibStop : MonoBehaviour {
    Vib_Stop vib_stop;
	// Use this for initialization
	void Start () {
        vib_stop = gameObject.GetComponent<Vib_Stop>();
        vib_stop.VibStop();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
