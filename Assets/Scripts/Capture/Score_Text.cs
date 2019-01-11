using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_Text : MonoBehaviour {

    //public ParticleSystem effect;
    public TextMeshProUGUI scoretext;

    private float score;
    private float beforescore;
	// Use this for initialization
	void Start () {
        score = 0;
        scoretext.text = "×"+score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        score = PlayerPrefs.GetFloat("R") + PlayerPrefs.GetFloat("G") + PlayerPrefs.GetFloat("B") + PlayerPrefs.GetFloat("K");
        if (score != beforescore)
        {
            scoretext.text = "×" + score.ToString();
         //   effect.Play();
            beforescore = score;
        }
    }
}
