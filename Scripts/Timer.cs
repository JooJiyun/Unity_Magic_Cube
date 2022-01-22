using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float time = 0;
    public bool On_flg = false;
    public bool Start_flg = false;

    // Update is called once per frame
    void Update()
    {
        if (On_flg)
        {
            time += Time.deltaTime;
            int score = (int)(time* 100f);
            transform.GetComponent<Text>().text = (score / 100).ToString() + "." + (score % 100).ToString() + "s";
        }
    }
}
