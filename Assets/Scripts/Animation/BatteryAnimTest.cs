
using UnityEngine;
public class BatteryAnimTest : MonoBehaviour
{

    float MAXElectric=100, Electric;
    GameObject UI1,UI2; //対象のUI
    UI_Battery script1,script2; //HPのアニメーション処理を行うscript
    void Awake()
    {
        Electric = MAXElectric;
        UI1 = GameObject.Find("CurrentElectoric");
        script1 = UI1.GetComponent<UI_Battery>(); //unitychanの中にあるUnityChanScriptを取得して変数に格納する
        Debug.Log(script1);
    }

    // Update is called once per frame
    void Update()
    {
        Electric--;
        if (Electric == 0)
        {
            Electric = MAXElectric;
        }
        script1.HP_Anim(MAXElectric, Electric);



    }
}
