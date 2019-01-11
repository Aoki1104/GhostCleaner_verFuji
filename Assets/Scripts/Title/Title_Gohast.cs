using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Gohast : MonoBehaviour {

    //ゴーストの行動パターン
    public enum State{
        Standing,           //棒立ち
        Action,             //特殊アクション    
        Walk              //歩く
    }

    public State TitleState;
	AnimatorStateInfo stateInfo;  

    public bool cap; //ゴーストが捕まえられたかの判定

    [SerializeField]
    Transform CenterOfBalance;  // 重心
    [SerializeField]
    private float speed; //ゴーストの移動速度
    [SerializeField]
    private float capspeed; //ゴーストの捕まえられる速度

    private Animator anim; //ゴーストのアニメーション
    private Vector3 initial_position;
    private bool move;
    private bool captime;
    private bool animemove;
    private float speed_tmp; //速度の一時保存
    private float capsecond; //捕獲までの時間
    private float randomcap; //捕獲のランダム設定
    private string beforestate;
    private float Change_Time;
    
    void Start()
    {
        anim = GetComponent<Animator>();
		stateInfo = anim.GetCurrentAnimatorStateInfo(0);    //アニメーター情報(読み取り専用)
        cap = false;
        StartCoroutine(RandomAnim());
        beforestate = "None";
    }

    void Update()
    {
		stateInfo = anim.GetCurrentAnimatorStateInfo(0); 
		if (beforestate == "Action") {
			
			if (stateInfo.normalizedTime > 0.995f) {    //再生時間がアニメーション終了時間の99%を越したら
				Debug.Log("変更");
				SetState("Standing");
			}
		}
    }

    void OnTriggerEnter(Collider wall)
    {
        if(wall.gameObject.tag == "Wall")
        {
            captime = true;
           // anim.Play("devil_stand");
        }

    }

    public void SetState(string state)
    {

        if (beforestate != state)
        {
            if (beforestate != state)
            {
                switch (state)
                {
                    case "Standing":
                        anim.Play("ghost_stop_Ver2");
                        TitleState = State.Standing;
                        beforestate = "Standing";
                        break;
                    case "Action":
                        anim.Play("ghost_action_Ver3");
                        TitleState = State.Action;
                        Change_Time = 0;
                        beforestate = "Action";

                        break;

                }
            }
        }
    }

    #region //未実装の球体の上を歩く処理
    private IEnumerator Walk()
    {

        float x = Random.Range(-10, 10);  //目的の位置のx方向のランダム決定
        float z = Random.Range(-11, 11);  //目的の位置のz方向のランダム決定
        Vector3 Newposi = new Vector3(x, 0, z); //目的地の座標設定
        Debug.Log(Newposi);
        var look = this.gameObject.transform.position - Newposi;
        //transform.LookAt(new Vector3(look.x,this.transform.position.y,look.z));
        Debug.Log(look);
        transform.rotation = Quaternion.LookRotation(new Vector3(look.x,0,look.z));
        move = true;    //動いてる間はtrueにして動か
       // anim.Play("devil_walk");
        speed = speed_tmp;
        yield return new WaitForSeconds(3.0f);
        speed = 0;
        //anim.Play("devil_stand");
        yield return new WaitForSeconds(2.0f);
        move = false; //3秒たったら止まらせて104行目に戻る
    }
    #endregion

    private IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }

    //ランダムでアニメを再生する処理
    private IEnumerator RandomAnim()
    {
        int rananim = Random.Range(1,3);
		float randomtime = Random.Range(5, 15);
        if(rananim == 1)
        {
            SetState("Action");
			randomtime = 1.5f;
        }
        if(rananim == 2)
        {
            SetState("Standing");
        }
        yield return new WaitForSeconds(randomtime);
        StartCoroutine(RandomAnim());
    }

}
