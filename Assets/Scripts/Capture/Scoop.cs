using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoop : MonoBehaviour {

    int step = 0;
    public GameObject scoop;
    Text scoopText;
    // Use this for initialization
    void Start () {
        scoopText = scoop.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        step = PlayerPrefs.GetInt("Step");

        if(step == 0)
        {
            scoopText.text = "×1";
        }
        if (step == 1)
        {
            scoopText.text = "×25";
        }
        if (step == 2)
        {
            scoopText.text = "×50";
        }
    }
}
