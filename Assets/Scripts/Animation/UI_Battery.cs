using UnityEngine;
using UnityEngine.UI;
public class UI_Battery: MonoBehaviour
{
    Image image;
 
    void Awake()
    {
        image = GetComponent<Image>();
    }
    private void Start()
    {
    }
    void Update()
    {
        
    }
    float parsent(float MAXElectric,float CurrentElectric)
    {
        return CurrentElectric / MAXElectric;
    }

    public void HP_Anim(float MAXElectric, float CurrentElectric)
    {
        image.fillAmount = parsent(MAXElectric, CurrentElectric);  //スクリプトは指定のImageの直下に置く必要がある
        if (parsent(MAXElectric, CurrentElectric) > 0.8)
        {
            image.color = new Color(0, 255, 0);
        }
        else if (parsent(MAXElectric, CurrentElectric) < 0.4 && parsent(MAXElectric, CurrentElectric) > 0.2)
        {
            image.color = new Color(255, 255, 0);
        }
        else if(parsent(MAXElectric, CurrentElectric) < 0.2)
        {
            image.color = new Color(255,0,0);
        }
 
    }
}