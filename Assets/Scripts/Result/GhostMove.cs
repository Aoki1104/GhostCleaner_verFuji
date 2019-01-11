using UnityEngine;
using System.Collections;

public class GhostMove : MonoBehaviour
{

    public float frame;

    float Sx, Sy;
    int stop = 0;
    public GameObject[] Point;
    int NowPoint=0; //現在向かっているポイント
    private int PointNum;
    void Start()
    {
        foreach (GameObject point in Point)
        {
            if (point == null)
            {
                Debug.LogError("エラー:問題のスクリプト:GhostMove.cs \n用意された配列の要素にgameobjectがアタッチされていません");
                return;
            }
        }
        PointNum = Point.Length;
        SpSet(Point[NowPoint].transform.position.x, Point[NowPoint].transform.position.y, frame);
    }
    void Update()
    {
        
        transform.Rotate(0, 0, 1);

        if (Point.Length <= NowPoint)
        {
            if (stop == 0)
            {
                stop = 1;
                Debug.Log("移動は終了しました。");
            }
        }
        else
        {
            //Debug.Log("point:" + NowPoint);
            Frame_Translate(Point[NowPoint].transform.position.x, Point[NowPoint].transform.position.y);
        }
    }
    void SpSet(float x, float y, float frame)
    {
        //どこの座標まで
        
        Sx = (x - transform.position.x) / frame;
        Sy = (y - transform.position.y) / frame;

    }
    void Frame_Translate(float x,float y)
    {
        if (!((transform.position.x < x + 0.1) && (transform.position.x > x - 0.1))) {
            transform.Translate(Sx, Sy, 0, Space.World);
        }
        else
        {
            NowPoint++;
            //終了時一回だけエラー出しちゃうので無視させる
            try {
                transform.position = new Vector3(x, y, transform.position.z);
                SpSet(Point[NowPoint].transform.position.x, Point[NowPoint].transform.position.y, frame);
            }catch{}
        }
    }
}