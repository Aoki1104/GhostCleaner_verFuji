using UnityEngine;
using UnityEngine.UI;
public class UI_HP: MonoBehaviour
{
    //Image image;
	public Slider hpbar;						//HPバー

    void Awake()
    {
        //image = GetComponent<Image>();
    }

    public float parsent(float MAXHP,float HP)
    {
        return HP / MAXHP;
    }

    public void HP_Anim(float MAXHP,float HP)
    {
        //image.fillAmount = parsent(MAXHP, HP);  //スクリプトは指定のImageの直下に置く必要がある
		hpbar.value = parsent(MAXHP, HP);		//HPバーを動かす
    }
}