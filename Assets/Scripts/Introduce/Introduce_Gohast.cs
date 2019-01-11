using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduce_Gohast : MonoBehaviour {
    
    //ゴーストの行動パターン
    public enum State
    {
        Standing,          //棒立ち
        Action,            //特殊アクション    
        Walk,              //歩く
        Capture            //捕獲
    }

    public State IntroduceState;

    [SerializeField]
    private float speed;

    private GameObject Gohast;
    private Rigidbody rigid;
    private Animator anim; //ゴーストのアニメーション
    private Vector3 Initial_Position;
    private string beforestate;

    // Use this for initialization
    void Start () {
        beforestate = "None";
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        IntroduceState = State.Standing;
   anim.Play("ghost_capture");
        Initial_Position = this.gameObject.transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void FixedUpdate()
    {
        rigid.AddForce(0,speed*Time.deltaTime,0);
    }


    private IEnumerator DestroyGohast()
    {
       // iTween.MoveTo(this.gameObject, iTween.Hash("y", 248.0f, "time", time-0.5));
        yield return new WaitForSeconds(5.0f);
        Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            this.gameObject.transform.position = Initial_Position;
            rigid.velocity = Vector3.zero;
        }
    }
}
