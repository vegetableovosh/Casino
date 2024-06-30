using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstManager : MonoBehaviour
{
    void Start()
    {
        // Проверка, установлен ли флаг первого запуска
        if (!PlayerPrefs.HasKey("FirstLaunch"))
        {
            // Если нет, это первый запуск
            SetInitialValues();
            PlayerPrefs.SetInt("FirstLaunch", 1); // Установите флаг первого запуска
            PlayerPrefs.Save(); // Сохраните изменения
        }
    }

    void SetInitialValues()
    {
        PlayerPrefs.SetFloat("Money", 50);
    }
}