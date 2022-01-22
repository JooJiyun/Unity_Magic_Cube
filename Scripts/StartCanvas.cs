using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartCanvas : MonoBehaviour
{
    public Text[] Levels_text;
    public GameObject Background;
    public GameObject CountingSettingCanvas;
    public Image CountingSettingImage;
    public Sprite[] Level_sprite;
    public Slider CountSlider;
    public Text SliderText;
    public GameObject TitleBackground;

    void Start()
    {

        for (int i = 0; i < Levels_text.Length; i++)
        {
            int level = i + 2;
            if (PlayerPrefs.GetFloat("BestScore" + level.ToString(), -1) == -1)
            {

                Levels_text[i].text = "BestScore : --.--s";
            }
            else
            {
                int score = (int)(PlayerPrefs.GetFloat("BestScore" + level.ToString(), -1) * 100f);
                Levels_text[i].text = "BestScore : "+(score/100).ToString()+"."+(score%100).ToString()+"s";
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        float h, s, v;
        Color.RGBToHSV(Background.GetComponent<Image>().color, out h, out s, out v);
        h += Time.deltaTime * 0.01f;
        if (h > 1) h -= 1;
        Background.GetComponent<Image>().color = Color.HSVToRGB(h, s, v);

        Color.RGBToHSV(TitleBackground.GetComponent<Image>().color, out h, out s, out v);
        h += Time.deltaTime;
        if (h > 1) h -= 1;
        TitleBackground.GetComponent<Image>().color = Color.HSVToRGB(h, s, v);



        SliderText.text = CountSlider.value.ToString();
    }

    public void PressLevelButton(int level)
    {
        PlayerPrefs.SetInt("Level", level+2);
        CountingSettingImage.sprite = Level_sprite[level];
        CountSlider.value = 10;
        CountingSettingCanvas.gameObject.SetActive(true);
    }
    public void CloseCountCanvas()
    {
        CountingSettingCanvas.gameObject.SetActive(false);
    }

    public void LoadMainScene()
    {
        PlayerPrefs.SetInt("Mix_Count", (int)CountSlider.value);        
        SceneManager.LoadScene("MainScene");
    }
}
