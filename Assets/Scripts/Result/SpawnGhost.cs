using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGhost : MonoBehaviour {
	public List<GameObject> Gohast;
	private List<GameObject> Gohast_Spawn = new List<GameObject>();

	private float x = 1005;
	private float z = -668;
	private float y = 394;
	public float MAX;
	// Use this for initialization
	void Start () {
		for (int num = 0; num < MAX; num++)
		{
			int GohastNum = Random.Range(0, 3);
			Instantiate(Gohast[GohastNum], new Vector3(Random.Range(x - 65, x + 167), Random.Range(y -500, y -10), Random.Range(z-10,z-40)), Quaternion.identity);
		}
	}

}
