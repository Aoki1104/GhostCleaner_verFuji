using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum_Gohast : MonoBehaviour {

    public List<GameObject> Gohast;

    private List<GameObject> Gohast_Spawn = new List<GameObject>();
    private Introduce_Gohast InGohast;
    private float x, y, z;
    private float RandomNum;
    private int GohastNum;

	// Use this for initialization
	void Start () {
        RandomNum = 30;
        x = this.gameObject.transform.position.x;
        y = this.gameObject.transform.position.y;
        z = this.gameObject.transform.position.z;
        for (int num = 0; num < RandomNum; num++)
        {
            GohastNum = Random.Range(0, 3);
            Gohast_Spawn.Add(Gohast[GohastNum]);
            Instantiate(Gohast_Spawn[num], new Vector3(Random.Range(x - 100, x + 100), Random.Range(y - 500, y -10), Random.Range(z - 100, z + 100)), Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {

       
    }
}
