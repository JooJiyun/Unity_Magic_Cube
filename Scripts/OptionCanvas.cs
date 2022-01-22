using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCanvas : MonoBehaviour
{
    public GameObject Background;
    public Slider[] sliders;
    public Text[] slider_texts;
    public GameObject BGM;
    public Text BGM_name;

    private int BGM_cnt = 7;
    private string[] slider_names = { "BGM_v","SE_v", "Sensivility" };
    void Start()
    {
        for(int i = 0; i < sliders.Length; i++)
        {
            sliders[i].value = PlayerPrefs.GetFloat(slider_names[i], 80);
        }
        BGM_name.text = "BGM-0"+(PlayerPrefs.GetInt("BGM", 0) + 1).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float h, s, v;
        Color.RGBToHSV(Background.GetComponent<Image>().color, out h, out s, out v);
        h += Time.deltaTime * 0.01f;
        if (h > 1) h -= 1;
        Background.GetComponent<Image>().color = Color.HSVToRGB(h, s, v);

        for(int i = 0; i < sliders.Length; i++)
        {
            slider_texts[i].text = sliders[i].value.ToString() + "%";
            PlayerPrefs.SetFloat(slider_names[i], sliders[i].value);
        }

        BGM.GetComponent<BGM>().ChangeVolume(PlayerPrefs.GetFloat("BGM_v", 80));
    }

    public void CloseCanvas()
    {
        gameObject.SetActive(false);
    }

    public void OpenCanvas()
    {
        gameObject.SetActive(true);
    }

    public void BGM_change(int val)
    {
        int BGM_no = PlayerPrefs.GetInt("BGM", 0);
        BGM_no = (BGM_no + val) % BGM_cnt;
        PlayerPrefs.SetInt("BGM", BGM_no);
        BGM_name.text = "BGM-0" + (BGM_no + 1).ToString();
        BGM.GetComponent<BGM>().ChangeMusic(BGM_no);
    }
}
