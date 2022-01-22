using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private float rotate_speed = 400;
    private float real_rotate;
    private float right_angle = 0;
    private float up_angle = 0;

    //                         top front rignt back left bottom
    private int[] side_color = { 0, 1, 2, 3, 4, 5 };

    void Update()
    {
        real_rotate = rotate_speed * Time.deltaTime;
        if (right_angle > 0)
        {
            real_rotate = real_rotate < right_angle ? real_rotate : right_angle;
            transform.RotateAround(Vector3.zero, Vector3.down, real_rotate);
            right_angle -= real_rotate;
        }
        else if (right_angle < 0)
        {
            real_rotate = real_rotate < -right_angle ? real_rotate : -right_angle;
            transform.RotateAround(Vector3.zero, Vector3.up, real_rotate);
            right_angle += real_rotate;
        }
        else if (up_angle > 0)
        {
            real_rotate = real_rotate < up_angle ? real_rotate : up_angle;
            transform.RotateAround(Vector3.zero, Vector3.right, real_rotate);
            up_angle -= real_rotate;
        }
        else if (up_angle < 0)
        {
            real_rotate = real_rotate < -up_angle ? real_rotate : -up_angle;
            transform.RotateAround(Vector3.zero, Vector3.left, real_rotate);
            up_angle += real_rotate;
        }
    }

    public int get_color_top() { return side_color[0]; }
    public int get_color_front() { return side_color[1]; }
    public int get_color_right() { return side_color[2]; }
    public int get_color_left() { return side_color[3]; }
    public int get_color_back() { return side_color[4]; }
    public int get_color_bottom() { return side_color[5]; }


    public void Rotate_Column(float value)
    {
        up_angle += value;
        if (value > 0)
        {
            //0,1,5,3
            int tmp = side_color[0];
            side_color[0] = side_color[1];
            side_color[1] = side_color[5];
            side_color[5] = side_color[3];
            side_color[3] = tmp;
        }
        else
        {
            //0,3,5,1
            int tmp = side_color[0];
            side_color[0] = side_color[3];
            side_color[3] = side_color[5];
            side_color[5] = side_color[1];
            side_color[1] = tmp;
        }
    }

    public void Rotate_Row(float value)
    {
        right_angle += value;
        if (value > 0)
        {
            //1,4,3,2
            int tmp = side_color[1];
            side_color[1] = side_color[4];
            side_color[4] = side_color[3];
            side_color[3] = side_color[2];
            side_color[2] = tmp;
        }
        else 
        { 
            //1,2,3,4
            int tmp = side_color[1];
            side_color[1] = side_color[2];
            side_color[2] = side_color[3];
            side_color[3] = side_color[4];
            side_color[4] = tmp;
        }
    }

    public void Active(bool flg)
    {
        gameObject.SetActive(flg);
    }
}
