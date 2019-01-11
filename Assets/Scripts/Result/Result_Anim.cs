using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result_Anim : MonoBehaviour {
	Image AURA_BODY_BACK;
	//透明度下限（どこまで下がるか）
	[SerializeField][Range(0, 255)] 
	private float Color_LowerLimit = 200;
	float Color_Next;
	[SerializeField]
	private float Change_Value=5;
	//trueならColor_Kを上げる falseならCOlor_Kを下げる
	bool UP_Switch=false;
	float Color_K=255;

	public Image Back_Aura;
	// Use this for initialization
	void Awake(){
		Back_Aura = GetComponent<Image> ();
		if (Back_Aura.color.r == 255 && Back_Aura.color.b == 255 && Back_Aura.color.g == 255) {
			Back_Aura.color = new Color (0/ 255f,0/ 255f,0/ 255f,255/ 255f);
		}
	}
	void Start () {
		AURA_BODY_BACK = GetComponent<Image> ();
	
	}
	
	// Update is called once per frame
	void Update () {


		Debug.Log (Color_K);
		AURA_BODY_BACK.color = new Color(AURA_BODY_BACK.color.r,AURA_BODY_BACK.color.g,AURA_BODY_BACK.color.b,Color_K/255f);

		if(UP_Switch == false){
			if (Color_K >= Color_LowerLimit) {
				Color_K -= Change_Value;
			} else{
				UP_Switch = true;
			}
		}

		if(UP_Switch == true){
			if (Color_K <= 255) {
				Color_K += Change_Value;
			} else{
				UP_Switch = false;
			}
		}
	}
}
