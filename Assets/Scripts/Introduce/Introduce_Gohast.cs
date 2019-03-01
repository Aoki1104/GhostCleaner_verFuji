using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//最後のゴーストが吸い込まれて上昇しているシーンに使われているスクリプト
public class Introduce_Gohast : MonoBehaviour {
    
    [SerializeField]
    private float speed;    //上がっていくスピード

    private GameObject Gohast;  
    private Rigidbody rigid;
    private Animator anim; //ゴーストのアニメーション
    private Vector3 Initial_Position;   //初期ポジションを格納する変数

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.Play("ghost_capture");
        Initial_Position = this.gameObject.transform.position;
    }
	

    private void FixedUpdate()
    {
        rigid.AddForce(0,speed*Time.deltaTime,0);
    }


    private IEnumerator DestroyGohast()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this);
    }

    //見えない壁にぶつかったら初期化
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            this.gameObject.transform.position = Initial_Position;
            rigid.velocity = Vector3.zero;
        }
    }
}
