using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleGohastSpawn : MonoBehaviour {

    public GameObject[] Gohast;
    Title_Gohast GohastScriptL,GohastScriptR;
    private int numR,numL;
	// Use this for initialization
	void Start () {
        GohastLSpawn();
        GohastRSpawn();
    }
	
	// Update is called once per frame
	void Update () {
        if (GohastScriptL.cap == true)
        {
            GohastLSpawn();
        }
        if (GohastScriptR.cap == true)
        {
            GohastRSpawn();
        }
    }

    private void GohastLSpawn()
    {
        numL = Random.Range(0, 3);
        Instantiate(Gohast[numL], new Vector3(12.3f, -9.42f, 58.93f), new Quaternion(-20, 180, 0, 1.0f));
        GohastScriptL = Gohast[numL].GetComponent<Title_Gohast>();
       // GohastScriptL.Gohast_Patern = 2;
    }

    private void GohastRSpawn()
    {
        numR = Random.Range(0, 3);
        Instantiate(Gohast[numR], new Vector3(-14.8f, -9.42f, 58.93f), new Quaternion(-20, 180, 0, 1.0f));
        GohastScriptR = Gohast[numR].GetComponent<Title_Gohast>();
      // GohastScriptR.Gohast_Patern = 2;
    }
}
