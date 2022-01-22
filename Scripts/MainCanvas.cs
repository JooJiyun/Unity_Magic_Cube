using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    public GameObject Background;
    public GameObject[] MoveButtons;
    public GameObject[] MatchButtons;
    public GameObject ExitCanvas;

    public Text Watch_Mode_Text, Match_Mode_Text;
    public Image Watch_Mode_Image, Match_Mode_Image;
    public Sprite Watch_Mode_Sprite, Match_Mode_Sprite, Not_Working_Sprite;

    private int Mode = 0;

    void Start()
    {
        int Level = PlayerPrefs.GetInt("Level", 3);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < MatchButtons[i].transform.childCount; j++)
            {
                if (j < Level)
                {
                    MatchButtons[i].transform.GetChild(j).gameObject.SetActive(true);
                }
                else
                {
                    MatchButtons[i].transform.GetChild(j).gameObject.SetActive(false);
                }
            }
        }
        Mode = 1;
        Toggle_Mode();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitCanvas.GetComponent<ExitCanvas>().OpenCanvas();
        }

        float h, s, v;
        Color.RGBToHSV(Background.GetComponent<Image>().color, out h, out s, out v);
        h += Time.deltaTime*0.01f;
        if (h > 1) h -= 1;
        Background.GetComponent<Image>().color = Color.HSVToRGB(h, s, v);

    }

    public void LoadStatScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Toggle_Mode()
    {
        // Mode==0 : Watch mode
        // Mode==1 : Match mode
        // Mode==2 : Ready to Match - - not use this script

        if (Mode == 0)
        {
            Mode = 1;

            Match_Mode_Image.sprite = Match_Mode_Sprite;
            Watch_Mode_Image.sprite = Not_Working_Sprite;

            Color color = Match_Mode_Image.color;
            color.a = 1f;
            Match_Mode_Image.color = color;

            color = Watch_Mode_Image.color;
            color.a = 0.5f;
            Watch_Mode_Image.color = color;

            color = Match_Mode_Text.color;
            color.a = 1f;
            Match_Mode_Text.color = color;

            color = Watch_Mode_Text.color;
            color.a = 0.5f;
            Watch_Mode_Text.color = color;

            for(int i = 0; i < 4; i++)
            {
                color = MoveButtons[i].GetComponent<Image>().color;
                color.a = 1f;
                MoveButtons[i].GetComponent<Image>().color = color;
                MoveButtons[i].GetComponent<Button>().enabled = true;
            }
            for(int i = 0; i < 4; i++)
            {
                MatchButtons[i].gameObject.SetActive(true);
            }
        }
        else
        {
            Mode = 0;

            Watch_Mode_Image.sprite = Watch_Mode_Sprite;
            Match_Mode_Image.sprite = Not_Working_Sprite;

            Color color = Match_Mode_Image.color;
            color.a = 0.5f;
            Match_Mode_Image.color = color;

            color = Watch_Mode_Image.color;
            color.a = 1f;
            Watch_Mode_Image.color = color;

            color = Match_Mode_Text.color;
            color.a = 0.5f;
            Match_Mode_Text.color = color;

            color = Watch_Mode_Text.color;
            color.a = 1f;
            Watch_Mode_Text.color = color;

            for (int i = 0; i < 4; i++)
            {
                color = MoveButtons[i].GetComponent<Image>().color;
                color.a = 0.5f;
                MoveButtons[i].GetComponent<Image>().color = color;
                MoveButtons[i].GetComponent<Button>().enabled = false;
            }
            for (int i = 0; i < 4; i++)
            {
                MatchButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
