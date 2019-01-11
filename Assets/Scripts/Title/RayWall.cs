using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayWall : MonoBehaviour {

    public int x, y, z;
    public int direc;
    Vector3 RayPosition;
	// Use this for initialization
	void Start () {
        RayPosition = new Vector3(this.transform.position.x+50,transform.position.y, 0);
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = new Ray(this.transform.position,RayPosition );

        Debug.DrawLine(ray.origin, ray.direction*direc, Color.red);


    }
}
