using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCanvas : MonoBehaviour
{
    public GameObject Timer;
    public GameObject cube;

    public void CloseCanvas()
    {
        gameObject.SetActive(false);
        if (Timer.GetComponent<Timer>().Start_flg)
        {
            Timer.GetComponent<Timer>().On_flg = true;
        }
        cube.GetComponent<Cube>().CubeOn(true);
    }

    public void OpenCanvas()
    {
        gameObject.SetActive(true);
        if (Timer.GetComponent<Timer>().Start_flg)
        {
            Timer.GetComponent<Timer>().On_flg = false;
        }
        cube.GetComponent<Cube>().CubeOn(false);
    }
}
