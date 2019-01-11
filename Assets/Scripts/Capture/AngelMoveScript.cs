using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelMoveScript : MonoBehaviour {
	//参考URL　→　http://beatdjam.hatenablog.com/entry/2014/10/22/032751

	private GameObject nearObject;				//最も近いオブジェクト
	private float standTime = 0f;				//経過時間
	private float actionTime = 1.5f;			//経過時間
	private bool near;							//髪に近いかどうか
	private bool stand = false;					//立ちモーション
	private bool action = false;				//固有モーション
	public bool spacekey = false;				//スペースキーを押したか
	private Animator anim;						//ついているアニメーター

    public GameObject Netto;                    //ネット
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		nearObject = searchTag (gameObject, "Hair");		//近い"Hair"タグ付いてるオブジェクト
		near = false;										//近くない
		anim.Play("angel_walk");						//歩きモーション切り替え
	}

	// Update is called once per frame
	void Update () {
		nearObject = searchTag (gameObject, "Hair");		//近くの"Hair"タグを探す
		if (near == false && spacekey == false) {			//近くない場合
			transform.LookAt (nearObject.transform);		//髪の方を向く
			transform.Translate (transform.forward * 0.05f, nearObject.transform);		//髪の方に歩く
			action = false;
		} else if(spacekey == false) {
			transform.LookAt (nearObject.transform);		//髪の方を向く
			transform.Translate (Vector3.zero, nearObject.transform);					//止まる

			if(action == false){
				anim.Play ("angel_action");				//固有モーション切り替え
				action = true;
			}
		}
	}

	GameObject searchTag(GameObject nowObj,string tagName){
		float tmpDis = 0f;
		float nearDis = 0f;
		GameObject targetObj = null;

		foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName)) {
			tmpDis = Vector3.Distance (obs.transform.position, nowObj.transform.position);
			if (nearDis == 0f || nearDis > tmpDis) {
				nearDis = tmpDis;
				targetObj = obs;
				if (nearDis <= 2f) {
					targetObj = gameObject;
					near = true;
				}
			}
		}
		return targetObj;
	}

	public void AngelCaptured(float sec){
        spacekey = true;
        Instantiate(Netto, new Vector3(this.transform.position.x, Netto.transform.position.y, this.transform.position.z), Quaternion.identity);
        StartCoroutine(CaptureAction(sec));
    }

    private IEnumerator CaptureAction(float sec)
    {
        yield return new WaitForSeconds(sec);
        anim.Play("angel_capture");
        StartCoroutine(Captured(1f));
    }

    private IEnumerator Captured(float sec){
		yield return new WaitForSeconds (sec);
		Destroy (this.gameObject);												//将軍消す
	}
}
