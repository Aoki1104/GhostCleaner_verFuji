using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vib_Stop : MonoBehaviour {
    //モーターの振動をストップさせるためのスクリプトです

    SerialHandler serialHandler;
    GameObject serial;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void VibStop()
    {
        serial = GameObject.Find("Serial");
        serialHandler = serial.GetComponent<SerialHandler>();
        serialHandler.Motor_Stop(0);
        serialHandler.Motor_Stop(1);
    }
}
