using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    private float currentMoney;
    private TextMeshProUGUI textBalance;

    void Start()
    {
        textBalance = GameObject.Find("Balance").GetComponent<TextMeshProUGUI>();
        GetMoney();
    }

    // Update is called once per frame
    public void SetMoney(int add)
    {
        currentMoney += add;
        PlayerPrefs.SetFloat("Money", currentMoney);

    }

    public void GetMoney()
    {
        currentMoney = PlayerPrefs.GetFloat("Money");
        textBalance.text = currentMoney.ToString("#.00");
    }


}
