using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Call_VibStop : MonoBehaviour {
    Vib_Stop vib_stop;

    void Start()
    {
        vib_stop = GetComponent<Vib_Stop>();
        vib_stop.VibStop();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
