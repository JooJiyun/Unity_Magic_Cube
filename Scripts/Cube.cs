using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    public int Cube_Length = 140;

    public GameObject Cell_prefab;
    public GameObject[] Cells;
    public GameObject Canvas_Touch_Block;
    public Text Message;
    public GameObject Timer;
    public GameObject SE_audio;

    private int Level;
    private int Mode = 0;
    private int Count_Inital_Mix;
    private float delta_time;

    private int[,,] Cells_Idx;
    
    void Start()
    {
        Mode = -1;
        Level = PlayerPrefs.GetInt("Level", 3);
        transform.GetComponent<CubeRotater>().rotate_speed = PlayerPrefs.GetFloat("Speed_Watch_Mode", 10);
        Count_Inital_Mix = PlayerPrefs.GetInt("Mix_Count", 10);

        float sensivility = PlayerPrefs.GetFloat("Sensivility", 80);
        transform.GetComponent<CubeRotater>().rotate_speed = sensivility / 10;

        RespawnCells();
        transform.GetComponent<BoxCollider>().enabled = false;
    }

    void Update()
    {
        // Mode== -1 : Initial Mix Time
        // Mode==0 : Watch mode
        // Mode==1 : Match mode
        // Mode==2 : Ready to Match
        if (Mode == -1)
        {
            delta_time += Time.deltaTime;
            if (delta_time < 0.4) return;
            delta_time = 0;

            int rand_dir = Random.Range(0, 4);
            int rand_line_idx = Random.Range(0, Level);
            switch (rand_dir)
            {
                case 0:
                    Match_Cells_Down(rand_line_idx);
                    break;
                case 1:
                    Match_Cells_Up(rand_line_idx);
                    break;
                case 2:
                    Match_Cells_Left(rand_line_idx);
                    break;
                case 3:
                    Match_Cells_Right(rand_line_idx);
                    break;
            }

            Count_Inital_Mix--;
            if (Count_Inital_Mix <= 0)
            {
                Mode = 0;
                transform.GetComponent<BoxCollider>().enabled = true;
                Message.text = "Mixing...";
                Canvas_Touch_Block.gameObject.SetActive(false);
                Timer.GetComponent<Timer>().Start_flg = true;
                Timer.GetComponent<Timer>().On_flg = true;
            }
        }
        else if (Mode == 2)
        {
            transform.rotation = Quaternion.Euler(0,0,0);

            if ((transform.rotation.x == 0) && (transform.rotation.y == 0) && (transform.rotation.z == 0))
            {
                Toggle_Mode();
            }
        }

    }

    public void CubeOn(bool flg)
    {
        gameObject.SetActive(flg);
    }

    public void RespawnCells()
    {
        float cell_length = (float)Cube_Length / (float)Level; 
        float pos_d = 0.02f * cell_length;
        float pos_m = (float)(Level - 1) / 2.0f;

        Cells_Idx = new int[Level, Level, Level];
        Cells = new GameObject[Level*Level*Level];
        int idx = 0;
        for(int i = 0; i < Level; i++)
        {
            for(int j = 0; j < Level; j++)
            {
                for(int k = 0; k < Level; k++)
                {
                    Cells[idx] = Instantiate(Cell_prefab) as GameObject;
                    Cells[idx].transform.SetParent(transform, false);
                    Cells[idx].transform.localScale = new Vector3(cell_length, cell_length, cell_length);
                    Cells[idx].transform.localPosition = new Vector3(pos_d*((float)i -pos_m), pos_d * ((float)j - pos_m), pos_d * ((float)k - pos_m));
                    Cells_Idx[i, j, k] = idx;
                    idx++;
                }
            }
        }
    }
    public void Toggle_Mode()
    {
        if (Mode == 0)
        {
            // Mode : Watch -> Ready to Match
            Mode = 2;
            transform.GetComponent<BoxCollider>().enabled = false;
        }
        else if(Mode == 1)
        {
            // Mode : Match -> Watch
            Mode = 0;
            transform.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            // Mode : Ready to Match -> Match
            Mode = 1;
        }
    }

    #region Rotate Cube
    public void Rotate_Cube_Right()
    {
        if (Mode != 1) return;
        for(int i = 0; i < Level; i++)
        {
            Rotate_Cell_Idx_Right(i);
            Rotate_Cells_Row(i, 90);
        }
    }
    public void Rotate_Cube_Left()
    {
        if (Mode != 1) return;
        for (int i = 0; i < Level; i++)
        {
            Rotate_Cell_Idx_Right(i);
            Rotate_Cell_Idx_Right(i);
            Rotate_Cell_Idx_Right(i);
            Rotate_Cells_Row(i, -90);
        }
    }
    public void Rotate_Cube_Up()
    {
        if (Mode != 1) return;
        for (int i = 0; i < Level; i++)
        {
            Rotate_Cell_Idx_Up(i);
            Rotate_Cells_Column(i, 90);
        }
    }
    public void Rotate_Cube_Down()
    {
        if (Mode != 1) return;
        for (int i = 0; i < Level; i++)
        {
            Rotate_Cell_Idx_Up(i);
            Rotate_Cell_Idx_Up(i);
            Rotate_Cell_Idx_Up(i);
            Rotate_Cells_Column(i,-90);
        }
    }
    #endregion

    #region Match Cells
    public void Match_Cells_Right(int line_idx)
    {
        SE_audio.GetComponent<SE>().CubeSound();
        line_idx = Level - line_idx - 1;
        Rotate_Cells_Row(line_idx, 90);
        Rotate_Cell_Idx_Right(line_idx);
        if (Mode == 1)
        {
            Match_Test();
        }
    }
    public void Match_Cells_Left(int line_idx)
    {
        SE_audio.GetComponent<SE>().CubeSound();
        line_idx = Level - line_idx - 1;
        Rotate_Cells_Row(line_idx, -90);
        Rotate_Cell_Idx_Right(line_idx);
        Rotate_Cell_Idx_Right(line_idx);
        Rotate_Cell_Idx_Right(line_idx);
        if (Mode == 1)
        {
            Match_Test();
        }
    }
    public void Match_Cells_Up(int line_idx)
    {
        SE_audio.GetComponent<SE>().CubeSound();
        Rotate_Cells_Column(line_idx, 90);
        Rotate_Cell_Idx_Up(line_idx);
        if (Mode == 1)
        {
            Match_Test();
        }
    }
    public void Match_Cells_Down(int line_idx)
    {
        SE_audio.GetComponent<SE>().CubeSound();
        Rotate_Cells_Column(line_idx, -90);
        Rotate_Cell_Idx_Up(line_idx);
        Rotate_Cell_Idx_Up(line_idx);
        Rotate_Cell_Idx_Up(line_idx);
        if (Mode == 1)
        {
            Match_Test();
        }
    }
    #endregion

    #region Rotate Cells
    private void Rotate_Cell_Idx_Right(int line_idx)
    {
        int[,] new_idx = new int[Level, Level];
        int j = line_idx;
        for (int i = 0; i < Level; i++)
        {
            for (int k = 0; k < Level; k++)
            {
                new_idx[Level-k-1, i] = Cells_Idx[i, j, k];
            }
        }

        for (int i = 0; i < Level; i++)
        {
            for (int k = 0; k < Level; k++)
            {
                Cells_Idx[i, j, k] = new_idx[i, k];
            }
        }
    }
    private void Rotate_Cell_Idx_Up(int line_idx)
    {
        int[,] new_idx = new int[Level, Level];
        int i = line_idx;
        for (int j = 0; j < Level; j++)
        {
            for (int k = 0; k < Level; k++)
            {
                new_idx[Level-k-1, j] = Cells_Idx[i, j, k];
            }
        }

        for (int j = 0; j < Level; j++)
        {
            for (int k = 0; k < Level; k++)
            {
                Cells_Idx[i, j, k] = new_idx[j, k];
            }
        }
    }
    private void Rotate_Cells_Row(int line_idx, float angle)
    {
        int j = line_idx;
        for(int i = 0; i < Level; i++)
        {
            for(int k = 0; k < Level; k++)
            {
                Cells[Cells_Idx[i,j,k]].transform.GetComponent<Cell>().Rotate_Row(angle);
            }
        }
    }
    private void Rotate_Cells_Column(int line_idx, float angle)
    {
        int i = line_idx;
        for (int j = 0; j < Level; j++)
        {
            for (int k = 0; k < Level; k++)
            {
                Cells[Cells_Idx[i,j,k]].transform.GetComponent<Cell>().Rotate_Column(angle);
            }
        }
    }
    #endregion


    private void Match_Test()
    {
        int i, j, k, idx;
        int pivot_color;

        pivot_color = Cells[0].GetComponent<Cell>().get_color_front();
        k = 0;
        for(i = 0; i < Level; i++)
        {
            for(j = 0; j < Level; j++)
            {
                idx = Cells_Idx[i, j, k];
                if (pivot_color != Cells[idx].GetComponent<Cell>().get_color_front()) return;
            }
        }

        pivot_color = Cells[0].GetComponent<Cell>().get_color_left();
        i = 0;
        for(j = 0; j < Level; j++)
        {
            for(k = 0; k < Level; k++)
            {
                idx = Cells_Idx[i, j, k];
                if (pivot_color != Cells[idx].GetComponent<Cell>().get_color_left()) return;
            }
        }

        pivot_color = Cells[0].GetComponent<Cell>().get_color_bottom();
        j = 0;
        for (i = 0; i < Level; i++)
        {
            for (k = 0; k < Level; k++)
            {
                idx = Cells_Idx[i, j, k];
                if (pivot_color != Cells[idx].GetComponent<Cell>().get_color_bottom()) return;
            }
        }

        idx = Cells_Idx[Level-1, Level-1, Level-1];
        pivot_color = Cells[idx].GetComponent<Cell>().get_color_top();
        j = Level - 1;
        for(i = 0; i < Level; i++)
        {
            for(k = 0; k < Level; k++)
            {
                idx = Cells_Idx[i, j, k];
                if (pivot_color != Cells[idx].GetComponent<Cell>().get_color_top()) return;
            }
        }

        idx = Cells_Idx[Level - 1, Level - 1, Level - 1];
        pivot_color = Cells[idx].GetComponent<Cell>().get_color_right();
        i = Level - 1;
        for (j = 0; j < Level; j++)
        {
            for (k = 0; k < Level; k++)
            {
                idx = Cells_Idx[i, j, k];
                if (pivot_color != Cells[idx].GetComponent<Cell>().get_color_right()) return;
            }
        }

        idx = Cells_Idx[Level - 1, Level - 1, Level - 1];
        pivot_color = Cells[idx].GetComponent<Cell>().get_color_back();
        k = Level - 1;
        for (i = 0; i < Level; i++)
        {
            for (j = 0; j < Level; j++)
            {
                idx = Cells_Idx[i, j, k];
                if (pivot_color != Cells[idx].GetComponent<Cell>().get_color_back()) return;
            }
        }
        Game_Complete();
    }
    private void Game_Complete()
    {
        SE_audio.GetComponent<SE>().WinSound();
        Timer.GetComponent<Timer>().Start_flg = false;
        Timer.GetComponent<Timer>().On_flg = false;
        PlayerPrefs.SetFloat("BestScore" + Level.ToString(),Timer.GetComponent<Timer>().time);
        Toggle_Mode();
        /*
        int idx = 0;
        for(int i = 0; i < Level; i++)
        {
            for(int j = 0; j < Level; j++)
            {
                for(int k = 0; k < Level; k++)
                {
                    Cells[idx].GetComponent<Cell>().Active(false);
                    idx++;
                }
            }
        }
        */
        Message.text = "Complete";
        Canvas_Touch_Block.gameObject.SetActive(true);
    }

}
