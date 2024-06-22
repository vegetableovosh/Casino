using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bet : MonoBehaviour
{
    private int betAmout;
    private float timeMoney;
    private Money Balance;
    private TMP_InputField input;
    public GameObject Button_Stop, Button_Bet;
    private IntefaceGame game;


    private void init()
    {
        input = GameObject.Find("InputField_Bet").GetComponent<TMP_InputField>();
        Balance = GameObject.Find("ManageBalance").GetComponent<Money>();
        game = GameObject.Find("GameController").GetComponent<IntefaceGame>();
        timeMoney = PlayerPrefs.GetFloat("Money");
    }

    public void Place()
    {
        init();
        int.TryParse(input.text, out betAmout);
        if (timeMoney > 0 && betAmout > 0){
            timeMoney -= betAmout;
            PlayerPrefs.SetFloat("Money", timeMoney);
            Balance.GetMoney();
            Button_Stop.SetActive(true);
            Button_Bet.SetActive(false);
            game.RunMain();
        }
    }

    //returns the bet 
    public void Return_rate(float x)
    {
        int cur;
        int.TryParse(input.text, out cur);

        float current = (float)timeMoney + ((float)cur * x);
        PlayerPrefs.SetFloat("Money", current);
        Balance.GetMoney();

    }

}
