using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lens_SE : MonoBehaviour {
    int step;
    GameObject captureManager;
    CaptureManagerScript capScript;
    int finished;

    public AudioSource Lens_Effect;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        step = PlayerPrefs.GetInt("Step");
        captureManager = GameObject.Find("CaptureManager");
        capScript = captureManager.GetComponent<CaptureManagerScript>();
        finished = capScript.Finished();
        if (finished != 1)
        {
            if (step == 1)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Lens_Effect.PlayOneShot(Lens_Effect.clip);
                }
            }

            if (step == 1 || step == 2)
            {
                /*
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Lens_Effect.PlayOneShot(Lens_Effect.clip);
                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    Lens_Effect.PlayOneShot(Lens_Effect.clip);
                }
                */
                if (Input.GetKeyUp(KeyCode.Return))
                {
                    Lens_Effect.PlayOneShot(Lens_Effect.clip);
                }
            }
        }
	}
}
