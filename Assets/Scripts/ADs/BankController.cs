using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BankController : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;
    public void ShowCanvas()
    {
        canvas.SetActive(true);
        //rAD.LoadAd();
    }

    public void Close()
    {
        canvas.SetActive(false);
    }
}
