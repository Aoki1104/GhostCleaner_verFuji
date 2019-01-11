using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTransparent : MonoBehaviour {
    public float colorA = 0.6f;
	// Use this for initialization
	void Start () {
        Renderer rend = GetComponent<Renderer>();
        rend.sharedMaterial.color = new Color(
            rend.sharedMaterial.color.r, 
            rend.sharedMaterial.color.g, 
            rend.sharedMaterial.color.b, 
            colorA);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
