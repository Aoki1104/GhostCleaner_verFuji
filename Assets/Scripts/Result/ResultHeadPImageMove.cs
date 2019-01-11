using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultHeadPImageMove : MonoBehaviour
{
    public int Move_X;      //ｘ軸の動く大きさ
    public int Move_Y;      //y軸の動く大きさ
    public int MoveItem_X;  //xに動くための項目数はいくつか
    public int MoveItem_Y;  //yに動くための項目数はいくつか
    int Movement_x = 1;     //x現在地
    int Movement_y = 1;     //y現在地
    bool stop=true;
    void Update()
    {
        Move2();
    }
    void Move1()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (1 < Movement_x)
            {
                transform.position = new Vector3(transform.position.x + Move_X, transform.position.y, 0);
                Movement_x--;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Movement_x < MoveItem_X)
            {
                transform.position = new Vector3(transform.position.x + -Move_X, transform.position.y, 0);
                Movement_x++;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (1 < Movement_y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + -Move_Y, 0);
                Movement_y--;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (Movement_y < MoveItem_Y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + Move_Y, 0);
                    Movement_y++;
                }
            }
        }
    }
    void Move2()
    {
        if(stop){
            if (PlayerPrefs.GetInt("ToResultList") == 1)
            {
                transform.position = new Vector3(transform.position.x - Move_X, transform.position.y, 0);
                Movement_x++;
                stop = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Movement_x < MoveItem_X)
            {
                transform.position = new Vector3(transform.position.x - Move_X, transform.position.y, 0);
                Movement_x++;
            }
        }
    }
}
