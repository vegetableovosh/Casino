using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BucketX : MonoBehaviour
{
    public float X;
    private float shakeDuration = 0.5f; // Продолжительность покачивания
    private float shakeMagnitude = 0.2f; // Амплитуда покачивания
    private Bet bet;
    private PlinkoC plinko;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position; // Запоминаем исходную позицию
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        plinko = GameObject.Find("GameController").GetComponent<PlinkoC>();
        bet = GameObject.Find("GameController").GetComponent<Bet>();
        bet.Return_rate(X);
        plinko.CoutBall();
        Destroy(collision.gameObject);

        StartCoroutine(Shake()); // Запускаем корутину для покачивания
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float yOffset = Mathf.Sin(Time.time * Mathf.PI * 2 / shakeDuration) * shakeMagnitude;
            transform.position = new Vector3(originalPosition.x, originalPosition.y + yOffset, originalPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Возвращаем объект на исходную позицию
        transform.position = originalPosition;
    }
}
