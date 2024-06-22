using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BucketX : MonoBehaviour
{
    public float X;
    private Bet bet;
    private PlinkoC plinko;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        plinko = GameObject.Find("GameController").GetComponent<PlinkoC>();
        bet = GameObject.Find("GameController").GetComponent<Bet>();
        bet.Return_rate(X);
        plinko.CoutBall();
        Destroy(collision.gameObject);
    }
}
