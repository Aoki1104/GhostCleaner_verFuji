using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairColor : MonoBehaviour {
    public GameObject hair;
    [SerializeField]private float hairR, hairG, hairB;

	// Use this for initialization
	void Start () {
        hairR = PlayerPrefs.GetFloat("HairRed");
        hairG = PlayerPrefs.GetFloat("HairGreen");
        hairB = PlayerPrefs.GetFloat("HairBlue");
        Renderer hairMaterial = hair.GetComponent<Renderer>();
        hairMaterial.sharedMaterial.color = new Color(hairR / 255f, hairG / 255f, hairB / 255f, 0.8f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
