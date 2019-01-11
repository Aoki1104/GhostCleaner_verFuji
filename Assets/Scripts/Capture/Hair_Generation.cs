using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hair_Generation : MonoBehaviour
{

    int ran = 1;
    public List <GameObject> hair = new List<GameObject>();
    private bool start = false;
    private int hitHair;                                    //髪に当たってない：0,  当たっている：1
    GameObject captureManager;
    CaptureManagerScript capScript;
    int finished;

    void Start()
    {
        hitHair = 0;
    }

    // Use this for initialization
    void Update()
    {
        int step = PlayerPrefs.GetInt("Step");
        captureManager = GameObject.Find("CaptureManager");
        capScript = captureManager.GetComponent<CaptureManagerScript>();
        finished = capScript.Finished();
        if (finished != 1)
        {

            if (Input.GetKey(KeyCode.Return))
            {
                if (start == false)
                {
                    if (hitHair == 0)
                    {
                        ran = Random.Range(1, 6);
                        if (ran == 1)
                        {
                            Instantiate(hair[0], new Vector3(-10, 0, 5), Quaternion.identity);
                        }
                        if (ran == 2)
                        {
                            Instantiate(hair[1], new Vector3(-24, -1, 0), Quaternion.identity);
                        }
                        if (ran == 3)
                        {
                            Instantiate(hair[2], new Vector3(12, 1, -4), Quaternion.identity);
                        }
                        if (ran == 4)
                        {
                            Instantiate(hair[3], new Vector3(0, 0, 10), Quaternion.identity);
                        }
                        if (ran == 5)
                        {
                            Instantiate(hair[4], new Vector3(0, 0, 0), Quaternion.identity);
                        }
                    }
                    start = true;
                }
            }
            else
            {
                start = false;
            }
        }
    }

    public void HIT(int hits)
    {
        hitHair = hits % 2;
    }
}
	
	// Update is called once per frame
