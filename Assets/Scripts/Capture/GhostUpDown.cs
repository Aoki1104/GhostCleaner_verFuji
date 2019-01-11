using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostUpDown : MonoBehaviour {
    private float timeY;
	// Use this for initialization
	void Start () {
        timeY = Random.Range(-180f, 180f);
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Sin(timeY) / 2 + 1f +transform.position.y,
            transform.position.z
            );
    }
	
	// Update is called once per frame
	void Update () {
        
        timeY += Time.deltaTime * 3f;
        transform.Translate(0f, Mathf.Cos(timeY) / 16f, 0f);
    }
}
