
using UnityEngine;
public class HPAnimTest : MonoBehaviour
{

    float MAXHP1 = 100, HP1;
    float MAXHP2 = 200, HP2;
    GameObject UI1,UI2; //対象のUI
    UI_HP script1,script2; //HPのアニメーション処理を行うscript
    void Awake()
    {
        HP1 = MAXHP1;
        HP2 = MAXHP2;
        UI1 = GameObject.Find("HP1");
        UI2 = GameObject.Find("HP2");
        script1 = UI1.GetComponent<UI_HP>(); //unitychanの中にあるUnityChanScriptを取得して変数に格納する
        script2 = UI2.GetComponent<UI_HP>(); 
        Debug.Log(script1);
    }

    // Update is called once per frame
    void Update()
    {
        HP1--;
        HP2--;
        if (HP1 == 0)
        {
            HP1 = MAXHP1;
        }
        if (HP2 == 0)
        {
            HP2 = MAXHP2;
        }
        script1.HP_Anim(MAXHP1, HP1);
        script2.HP_Anim(MAXHP2, HP2);


    }
}
