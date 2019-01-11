using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountTest : MonoBehaviour {
    ToResultData script;
    public int GoodPop;
    public int BadPop;
    public int GoodGet;
    public int GoodDis;
    public int BadGet;
    public int BadDis;

	// Use this for initialization
	void Start () {
        script = GetComponent<ToResultData>();
        //Good:0//
        for (int i = 0; i < GoodPop; i++)
        {
            script.HeadPTotalPop(0);

        }
        
        for (int i=0;i<GoodGet;i++)
        {
            script.HeadPGet(0);
        }
        for (int i = 0; i < GoodDis; i++)
        {
            script.HeadPDis(0);
        }
        //Bad:1//
        for (int i = 0; i < BadPop; i++)
        {
            script.HeadPTotalPop(1);
        }
        for (int i = 0; i < BadGet; i++)
        {
            script.HeadPGet(1);
        }
        for (int i = 0; i < BadDis; i++)
        {
            script.HeadPDis(1);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
