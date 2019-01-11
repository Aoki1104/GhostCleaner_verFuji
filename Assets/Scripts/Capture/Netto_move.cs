using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Netto_move : MonoBehaviour {

    public GameObject Netto;

    private float Delay_Time;
    private bool cap = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y >= 0)
        {
            Netto.transform.Translate(0, -Time.deltaTime*10,0);

        }

        if(this.transform.position.y < 0)
        {
            Delay_Time += Time.deltaTime;
            if (Delay_Time > 1)
            {
                Destroy(this.Netto);
            }
        }
	}
}
