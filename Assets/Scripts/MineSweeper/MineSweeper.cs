using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MineSweeper : MonoBehaviour, IntefaceGame
{
    public TextMeshProUGUI TextX;
    [SerializeField]
    private Sprite[] sprites = new Sprite[3];


    public TMP_InputField sum;
    [SerializeField]
    private TextMeshProUGUI numberOfMinestText;
    public Image[] cells;
    public int numberOfMines = 1; // Количество мин
    private bool[] mines;
    public GameObject[] rows;
    private int cols = 6; // Количество столбцов
    private int count;
    private bool isMine; //220 220 200 255

    public void RunMain()
    {

        count = 0;
        isMine = false;
        mines = new bool[rows.Length * cols]; // Обновляем массив мин

        for (int j = 0; j < rows.Length; j++)
        {
            cells = rows[j].GetComponentsInChildren<Image>();
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].sprite = sprites[0];
            }
        }

        ActivateNextRow();

    }

    void PlaceMines()
    {
        int placedMines = 0;
        while (placedMines < numberOfMines)
        {
            int index = Random.Range(0, cells.Length);
            if (!mines[index])
            {
                mines[index] = true;
                placedMines++;
            }
        }
    }

    public void OnCellClick(int index)
    {
       
        // Отображение всех клеток
        for (int i = 0; i < cells.Length; i++)
        {
            if (mines[i])
            {
                cells[i].sprite = sprites[2]; // Меняем цвет на красный, если это мина
            }
            else
            {
                cells[i].sprite = sprites[1]; // Или на зелёный, если это не мина
                TextMeshProUGUI go = Instantiate(TextX, new Vector2(0,0), Quaternion.identity);
                go.transform.SetParent(cells[i].transform, false);
                float x = SumX();
                go.name = "CellX" + i + (count-1);
                go.text = x.ToString("#.00" + "X");
            }

            // Деактивируем все кнопки после выбора
            cells[i].color = new Color32(150, 150, 150, 255);
            cells[i].GetComponent<Cell>().enabled = false;
        }

        if (mines[index])
        {
            cells[index].color = Color.white; // Меняем цвет на красный, если это мина
            DeactivateAllCells();
            isMine = true;
            BetSum();
        }
        else
        {
            cells[index].color = Color.white; // Меняем цвет на зелёный, если это не мина
            // Если не мина, активируем следующую строку
            if (count < 6)
                ActivateNextRow();
            else {
                DeactivateAllCells();
                BetSum();
            }
        }
    }

    public void DeactivateAllCells()
    {

        for (int j = 0; j < rows.Length; j++)
        {
            cells = rows[j].GetComponentsInChildren<Image>();
            for (int i = 0; i < cells.Length; i++)
            {
                Destroy(cells[i].GetComponent<Cell>());
            }
        }

    }

    public void StopGame()
    {
        DeactivateAllCells();
        for (int j = 0; j < rows.Length; j++)
        {
            cells = rows[j].GetComponentsInChildren<Image>();
            for (int i = 0; i < cells.Length; i++)
            {
                GameObject go = GameObject.Find("CellX" + i + j);
                cells[i].color = new Color32(220, 220, 220, 255);
                cells[i].sprite = sprites[0];
                Destroy(go);
            }
        }
    }

    public void ChangeMines(int Right)
    {
        if(0 < numberOfMines && numberOfMines < 6)
            numberOfMines = Right > 0 ? 
            (numberOfMines+1) > 5 ? 5: numberOfMines+1: 
            (numberOfMines-1) < 1 ? 1: numberOfMines-1;
        else
            numberOfMines = numberOfMines >= 5 ? 5 : 1;
        numberOfMinestText.text = numberOfMines.ToString();
    }
    //добавить декоратор
    public void ActivateNextRow()
    {
        cells = rows[count].GetComponentsInChildren<Image>();
        count++;
        // Инициализация мин
        mines = new bool[cells.Length];
        PlaceMines();

        // Привязка событий к кнопкам
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].gameObject.AddComponent<Cell>().Setup(this, i);
            cells[i].color = Color.white;
            if (i >= cols) // Деактивируем клетки, которые не в первой строке
            {
                cells[i].GetComponent<Cell>().enabled = false;
            }      
        }
    }

    private float SumX(){
        float cf = count - 0.5f;
        float nf = (float)numberOfMines;
        float x = 1 + ( (nf / 6) * cf);
        return x;
    }

    public void BetSum(){

        if (!isMine)
        {
            float x = SumX();
            Debug.Log(x);
            this.GetComponent<Bet>().Return_rate(x);
        }
        else
        {
            this.GetComponent<Bet>().Return_rate(0);
        }
    }

}
