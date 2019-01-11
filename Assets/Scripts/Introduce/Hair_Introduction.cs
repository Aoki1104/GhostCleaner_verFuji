using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hair_Introduction : MonoBehaviour {

    [SerializeField]
    private float Escape_Hair;
    [SerializeField]
    private float Escape_Speed;

    private float Time_Measurement;
    private bool buti_on;

    public AudioSource buti;
	// Use this for initialization
	void Start () {
        buti_on = false;	
	}
	
	// Update is called once per frame
	void Update () {
        Time_Measurement += Time.deltaTime;
        if(Time_Measurement > Escape_Hair)
        {
            this.gameObject.transform.Translate(0, Escape_Speed, 0);
            this.gameObject.transform.Rotate(0, 1+Time.deltaTime, 0);

        }
        if(Time_Measurement > Escape_Hair+1)
        {
            if (buti_on != true)
            {
                buti.PlayOneShot(buti.clip);
                buti_on = true;
            }
        }
	}
}
