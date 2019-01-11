using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class ScrollMove : MonoBehaviour {
    public ScrollRect Scroll;
    public GameObject ListFrame;
    public GameObject EndFrame;
    public int retu = 4;
    GameObject[] NodeObj;
    SceneChange script;
    float time=0;
    int Node;
    int Move_x=1;
    int Move_y=1;
    bool stop=false;
    void Start()
    {
        script = GetComponent<SceneChange>();
        Node = GameObject.FindGameObjectsWithTag("Node").Length;
        //↓くそがっ！
        //NodeObj = GameObject.FindGameObjectsWithTag("Node");
        NodeObj = new GameObject[Node];
        for (int i1 = 0; i1 < Node; i1++)
        {
            NodeObj[i1] = GameObject.Find("Node" + Convert.ToInt32(i1 + 1));
        }
    }
    void Update()
    {
        ToPictureBookScene();
        ToTitleScene();
        time += Time.time-(Time.time-1);
        Debug.Log(time);
        
        if (time > 20) {
            ScrollMovement();
        }
        
    }
    void ToPictureBookScene()
    {
        if (this.transform.position.x == -2020)
        {
            if (stop)
            {
                if (Move_y != Node + 1)
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        PlayerPrefs.SetInt("PictureBookHeadP", ((Move_y - 1) * retu) + Move_x);
                        Debug.Log("PictureBook_HeadP=" + (((Move_y - 1) * retu) + Move_x));
                        script.SceneChanegeFanc("PictureBook");
                    }
                }
                
            }
            stop = true;
        }
    }
    void ToTitleScene()
    {
        if (Move_y == Node + 1)
        {
            script.SceneChanegeFanc("Title");
        }
    }
    void ScrollMovement()
    {
        if (this.transform.position.x == -2020)
        { //リザルト2がカメラ内に移っているとき
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                time = 0;
                if (Move_y == Node)       //Move_yがNode数の時に下入力が行われたとき,EndFrameの無効化とListFrameの有効化
                {
                    ListFrame.SetActive(false);
                    EndFrame.SetActive(true);
                    Move_y++;
                }
                if (Move_y < Node)
                {
                    Move_y++;
                    Debug.Log(Scroll.verticalNormalizedPosition);
                    Scroll.verticalNormalizedPosition = Mathf.Clamp(Scroll.verticalNormalizedPosition - ((1f / (Node - 1))), 0f, 1f);
                    ListFrame.transform.position = new Vector3(ListFrame.transform.position.x, NodeObj[Move_y - 1].transform.position.y, ListFrame.transform.position.z);
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                time = 0;
                if (Move_y == Node + 1)       //Move_yがNode+1数の時に上入力が行われたとき,EndFrameの有効化とListFrameの無効化
                {
                    ListFrame.SetActive(true);
                    EndFrame.SetActive(false);
                    Move_y--;
                    
                }
                else {                        //上記に該当しない場合（リスト内を選択中の時）
                    if (1 < Move_y)
                    {
                        Move_y--;
                        Debug.Log(Scroll.verticalNormalizedPosition);
                        Scroll.verticalNormalizedPosition = Mathf.Clamp(Scroll.verticalNormalizedPosition + (1f / (Node - 1)), 0f, 1f);
                        ListFrame.transform.position = new Vector3(ListFrame.transform.position.x, ListFrame.transform.position.y, ListFrame.transform.position.z);
                        ListFrame.transform.position = new Vector3(ListFrame.transform.position.x, NodeObj[Move_y - 1].transform.position.y, ListFrame.transform.position.z);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                time = 0;
                if (Move_x < retu)
                {
                    Move_x++;
                    ListFrame.transform.position = new Vector3(ListFrame.transform.position.x + 400, ListFrame.transform.position.y, ListFrame.transform.position.z);

                }

            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                time = 0;
                if (1 < Move_x)
                {
                    Move_x--;
                    ListFrame.transform.position = new Vector3(ListFrame.transform.position.x - 400, ListFrame.transform.position.y, ListFrame.transform.position.z);

                }
            }
        }
    }

}
