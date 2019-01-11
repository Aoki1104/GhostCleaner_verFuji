using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitToHead : MonoBehaviour {
    [SerializeField]
    private int pushHitHair;                        //インチキスイッチ(Tabキー)を押したかどうかの判定
    Hair_Generation hair_Generation;

	// Use this for initialization
	void Start () {
        pushHitHair = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))          //インチキスイッチ(Tabキー)
        {
            pushHitHair++;
            hair_Generation = GetComponent<Hair_Generation>();
            hair_Generation.HIT(pushHitHair);
        }
	}
}
