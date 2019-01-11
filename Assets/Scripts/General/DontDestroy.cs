using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {
    public AudioSource audiosource;

    // Use this for initialization
    void Start () {
        audiosource = gameObject.GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("Stop", 1);
        }
        if (PlayerPrefs.GetInt("Stop") == 1)
        {
            audiosource.Stop();
        }

    }
}
