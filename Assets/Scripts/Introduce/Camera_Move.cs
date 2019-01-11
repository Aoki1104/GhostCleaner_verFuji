using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour {

    public float goal_y, goal_z;    //止まる位置
    public float Move_Until_Time;   //動くまでの時間
    public float Move_Time;
    
	// Use this for initialization
	void Start () {
        StartCoroutine("Move");
        
    }
	
	private IEnumerator Move()
    {
        yield return new WaitForSeconds(Move_Until_Time);
        iTween.MoveTo(this.gameObject, iTween.Hash("y", goal_y, "z", goal_z,"Time",Move_Time));
    }
}
