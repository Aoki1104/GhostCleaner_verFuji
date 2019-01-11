using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class renzurotate : MonoBehaviour {
    bool bl;
    float TimeS,TimeG=0;
    public float Speed = 4;
    int step, next_step;
    public GameObject captureManager;
    CaptureManagerScript capScript;
    int finished;
    bool moved;

    private float Debug_Time; 
	void Start () {
        step = 0;
        next_step = 0;
        moved = false;
	}
	
	// Update is called once per frame
	void Update () {
       step = PlayerPrefs.GetInt("Step");

        captureManager = GameObject.Find("CaptureManager");
        capScript = captureManager.GetComponent<CaptureManagerScript>();
        finished = capScript.Finished();
        if (finished == 1)
        {
            Destroy(gameObject);
        }
        else { 
            if (step == 0)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    bl = !bl;
                    TimeG += TimeS;                                    //回転の速さかな
                }
            }
            if (step == 1 || step == 2)
            {
                if (Input.GetKeyUp(KeyCode.Return))
                {
                    bl = !bl;
                    TimeG += TimeS;
                }

                /*
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    bl = !bl;
                    TimeG += TimeS;
                }
            }  
            if (step == 2)
            {
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    bl = !bl;
                    TimeG += TimeS;
                }
        */
            }


            if (bl)
            {
                LensRotate_plus();
            }
            else
            {
                LensRotate_minus();
            }
        }

    }
    void LensRotate_minus()
    {
        TimeS = Time.time - TimeG;
        transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(0, 180, TimeS * Speed));
        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            moved = true;
        }
        else
        {
            moved = false;
        }
    }
    void LensRotate_plus()
    {
        TimeS = Time.time - TimeG;
        transform.eulerAngles = new Vector3(0, 0, -Mathf.LerpAngle(0, 180, TimeS*Speed));
        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            moved = true;
        }
        else
        {
            moved = false;
        }
    }
    
}
