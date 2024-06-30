using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bet : MonoBehaviour
{
    private int betAmout;
    public float timeMoney;
    private Money Balance;
    private TMP_InputField input;
    public GameObject Button_Stop, Button_Bet;
    private IntefaceGame game;
    public InterstitialAd ad;
    private Canvas canvas; // ������ �� Canvas
    public GameObject uiPrefab; // ������ UI �������
    [SerializeField]
    private AudioClip betAudio;
    private void init()
    {

        input = GameObject.Find("InputField_Bet").GetComponent<TMP_InputField>();
        Balance = GameObject.Find("ManageBalance").GetComponent<Money>();
        game = GameObject.Find("GameController").GetComponent<IntefaceGame>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        timeMoney = PlayerPrefs.GetFloat("Money");
    }

    public void Place()
    {
        init();
        int.TryParse(input.text, out betAmout);
        bool isCheck = game.PreparationCheck();

        if (timeMoney > 0 && betAmout > 0 && betAmout <= timeMoney && isCheck)
        {
            this.GetComponent<AudioSource>().PlayOneShot(betAudio);
            input.interactable = false;
            timeMoney -= betAmout;
            PlayerPrefs.SetFloat("Money", timeMoney);
            Balance.GetMoney();
            Button_Stop.SetActive(true);
            Button_Bet.SetActive(false);
            game.RunMain();
        }
    }

    //returns the bet 
    public void Return_rate(float ix)
    {

        GameObject newUIObject = Instantiate(uiPrefab);
        TextMeshProUGUI textObject = newUIObject.GetComponent<TextMeshProUGUI>();
        textObject.text = ix > 0 ? (ix < 1 ? ix.ToString("0.00" + "X") : ix.ToString("#.00" + "X")) : "";
        newUIObject.transform.SetParent(canvas.transform, false);

        // ���������� ������� Canvas
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        RectTransform rectTransform = newUIObject.GetComponent<RectTransform>();
        rectTransform.localScale = new Vector2(ix > 1 ? ((ix /75) + 1.75f) : 1, ix > 1 ? ((ix / 75) + 1.75f) : 1);

        // ������ ��������� ������� ������ ������ Canvas
        float x = Random.Range(canvasRect.rect.xMin + (rectTransform.localScale.x + 220), canvasRect.rect.xMax - (rectTransform.localScale.x + 220));

        // ������ ��������� ������� ������ ��������� ������ �� ������ (-400 �� 400)
        float y = Random.Range(-200, 400);

        // ������������� ������� �������
        rectTransform.anchoredPosition = new Vector2(x, y);

        timeMoney = PlayerPrefs.GetFloat("Money");


        int cur;
        int.TryParse(input.text, out cur);

        float current = (float)timeMoney + ((float)cur * ix);
        PlayerPrefs.SetFloat("Money", current);
        Balance.GetMoney();
        Destroy(newUIObject, 1.0f);
        //ad.ShowAd();
    }

}
