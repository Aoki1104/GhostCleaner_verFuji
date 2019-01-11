using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class On_Canvas : MonoBehaviour {
    public GameObject canvas;
    public Image img;
    private bool start = false;
	// Use this for initialization
	void Start () {
        img.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        int step = PlayerPrefs.GetInt("Step");
        if (start == false)
        {
            if (step == 1)
            {
                img.enabled = true;
                start = true;
            }
        }
        if (step == 0)
        {
            if (start == true)
            {
                img.enabled = false;
                start = false;
            }
        }
	}
}
