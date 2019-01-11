using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairScript : MonoBehaviour {

    [SerializeField]private float hp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Hair_DeduceHp()
    {
        hp = hp - (1 * Time.deltaTime);
        if (hp < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
