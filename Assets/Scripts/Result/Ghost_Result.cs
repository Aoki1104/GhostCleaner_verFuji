using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Result : MonoBehaviour {

	private float speed;
	private Vector3 Initial_Position;
	// Use this for initialization
	void Start () {
		speed = Random.Range(0.8f, 1.3f);
		Initial_Position = this.gameObject.transform.position;
		this.transform.Rotate (new Vector3 (0, 180, 0));
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (0,speed,0);
		if (this.transform.position.y > 975) {
			this.transform.position = Initial_Position;
		}
	}


}
