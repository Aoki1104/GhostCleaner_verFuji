using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BIG_WHITE_GHOST_DELETE : MonoBehaviour {
    Rigidbody2D rig;
    Image img;
    int stop = 0;
    private float nowtime = 0;
    float timeOut = 5, A = 1;
    void Start()
    {
        //ゲット
        rig = this.GetComponent<Rigidbody2D>();
        img = this.GetComponent<Image>();
    }
    void Update()
    {
        //時間加算
        nowtime += Time.deltaTime;
        //timeOut秒たったら実行
        if (nowtime >= timeOut)
        {
            //rigidbodyのいろんな機能を切る（コライダーも
            rig.simulated = false;
            //徐々に透明にする
            A -= 0.1f;
            
            img.color = new Color(img.color.r, img.color.g, img.color.b, A);
            //完全に透明になったらデストロイ！
            if (img.color.a <= 0)
            {
                PlayerPrefs.SetFloat("IW", PlayerPrefs.GetFloat("IW") + 25f);
                Destroy(gameObject);
                
            }
        }
    }
}

