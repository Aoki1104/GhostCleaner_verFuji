using UnityEngine;

public class RGBToHSV : MonoBehaviour {
    //*注意*
    //実運用の際：スクリプト「RGBSet」を切っておくこと
    //「RGBSet」はEventSystemにアタッチされている
    float H, S, V;
    float R, G, B;
    void Start()
    {
        R = PlayerPrefs.GetFloat("R") * 1.5f;
        G = PlayerPrefs.GetFloat("G") * 1.5f;
        B = PlayerPrefs.GetFloat("B") * 1.5f;
        if (R >= 255)
        {
            R = 255;
        }
        if (G >= 255)
        {
            G = 255;
        }
        if (B >= 255)
        {
            B = 255;
        }
        //color型変数「RGB」にセットしたR,G,B値を格納（HSV変換の際に使用する）
        Color RGB = new Color(R, G, B);
        
        //RGBをHSV変換（変換後は変数H,S,Vに格納）
        //H = 0～360 
        //S = 0～255 
        //V = 0～255
        Color.RGBToHSV(RGB, out H, out S, out V);
        //数値調整
        H = 360 * H;
        S = 255 * S;
        //HSV変換後値確認ログ
        Debug.Log("R:" + R + " G:" + G + " B:" + B);
        Debug.Log("H:" + H + " S:" + S + " V:" + V);
        //PlayerPrefsに格納
        PlayerPrefs.SetFloat("H", H);
        PlayerPrefs.SetFloat("S", S);
        PlayerPrefs.SetFloat("V", V);
    }
}
