using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlinkoC : MonoBehaviour, IntefaceGame
{
    public GameObject Ball;
    private int numberOfBalls = 1;
    private int numberOfBallsRezerv;
    private int DestroyBalls;
    private int currentBallCount;
    private Money Balance;
    private TMP_InputField input;
    private float timeMoney;
    private int betAmout;


    [SerializeField]
    private TextMeshProUGUI numberOfMinestText;

    private void init()
    {
        numberOfBallsRezerv = numberOfBalls;
        input = GameObject.Find("InputField_Bet").GetComponent<TMP_InputField>();
        Balance = GameObject.Find("ManageBalance").GetComponent<Money>();
        timeMoney = PlayerPrefs.GetFloat("Money");
        int.TryParse(input.text, out betAmout);
        timeMoney -= betAmout * (numberOfBallsRezerv - 1);
    }

    public void RunMain()
    {
        init();
        PlayerPrefs.SetFloat("Money", timeMoney);
        Balance.GetMoney();
        DestroyBalls = 0;
        currentBallCount = 0;
        InvokeRepeating("SpawnObject", 0f, 0.6f);   
        //StartCoroutine(SpawnObjectAfterDelay(0.5f));   
    }

private void SpawnObject()
{
    if (currentBallCount < numberOfBallsRezerv)
    {
        float randomIndex = Random.Range(0.01f, 0.1f);
        Instantiate(Ball, new Vector2(randomIndex, 6.5f), Quaternion.identity);
        currentBallCount++;
    }
    else
    {
        CancelInvoke("SpawnObject"); // Останавливаем вызовы, когда все объекты созданы
    }
}

/*
    private void SpawnObjects()
    {

        for (int i = 0; i < numberOfBallsRezerv; i++)
        {
            float randomIndex = Random.Range(0.01f, 0.1f);
            Instantiate(Ball, new Vector2(randomIndex, 6.5f), Quaternion.identity);

            //yield return new WaitForSeconds(delay);
        }
        
    }
*/

    public void CoutBall()
    {
        DestroyBalls++;
        if (DestroyBalls >= numberOfBallsRezerv)
            this.gameObject.GetComponent<StopBet>().Stop();
    }

    public void ChangeBall(int Right)
    {
        if (0 < numberOfBalls && numberOfBalls < 10)
            numberOfBalls = Right > 0 ?
            (numberOfBalls + 1) > 9 ? 9 : numberOfBalls + 1 :
            (numberOfBalls - 1) < 1 ? 1 : numberOfBalls - 1;
        else
            numberOfBalls = numberOfBalls >= 9 ? 9 : 1;
        numberOfMinestText.text = numberOfBalls.ToString();
    }


    public bool PreparationCheck()
    {
        init();
        if((timeMoney - betAmout) > 0)
            return true;
        else
            return false;
    }
}
