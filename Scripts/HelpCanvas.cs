using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpCanvas : MonoBehaviour
{
    public GameObject Background;
    public GameObject[] Pages;
    private int page;

    void Update()
    {
        float h, s, v;
        Color.RGBToHSV(Background.GetComponent<Image>().color, out h, out s, out v);
        h += Time.deltaTime * 0.01f;
        if (h > 1) h -= 1;
        Background.GetComponent<Image>().color = Color.HSVToRGB(h, s, v);
    }

    public void CloseCanvas()
    {
        Pages[0].gameObject.SetActive(true);
        for(int i = 1; i < Pages.Length; i++)
        {
            Pages[i].gameObject.SetActive(false);
        }
        page = 0;
        gameObject.SetActive(false);
    }

    public void OpenCanvas()
    {
        gameObject.SetActive(true);
    }
    public void MovePage(int dir)
    {
        Pages[page].gameObject.SetActive(false);
        page+=dir;
        page %= Pages.Length;
        Pages[page].gameObject.SetActive(true);
    }
}
