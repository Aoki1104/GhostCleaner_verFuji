using UnityEngine;

public class RGBSet : MonoBehaviour {
    [Range(0f, 255f)]
    public float R=100,G=200,B=100,K=100;
	void Awake()
    {
        Debug.LogWarning("テスト用のRGB値を設定します。\nテスト運用でない場合は,本スクリプト「RGBSet」を切ってください。↓スクリプト配置↓\n場所：Scene:Result EventSystem内");

        PlayerPrefs.SetFloat("R", R);
        PlayerPrefs.SetFloat("G", G);
        PlayerPrefs.SetFloat("B", B);
        PlayerPrefs.SetFloat("K", K);
    }

    // Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
