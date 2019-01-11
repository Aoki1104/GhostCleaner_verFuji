using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0)){//マウスを押したら反転(ここの条件をやりたいようにいじればかーんせい)
            anim.speed = 2f;//アニメーション再生速度
            if (anim.GetBool("Switch"))
            {
                anim.SetBool("Switch", false);
            }
            else
            {
                anim.SetBool("Switch", true);
            }
            
        }
        
	}
}
