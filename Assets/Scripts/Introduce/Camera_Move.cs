using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カメラを動かすためのスクリプト
//最初の頭の上のゴーストにズームしていくシーンで使われる
public class Camera_Move : MonoBehaviour {

    public float goal_y, goal_z;    //止まる位置
    public float Move_Until_Time;   //動くまでの時間
    public float Move_Time;
    

	void Start () {
        StartCoroutine("Move"); 
    }
	
	private IEnumerator Move()
    {
        yield return new WaitForSeconds(Move_Until_Time);
        //オブジェクトの移動関数 (対象オブジェクト,(目的地の座標))
        iTween.MoveTo(this.gameObject, iTween.Hash("y", goal_y, "z", goal_z,"Time",Move_Time));
    }
}
