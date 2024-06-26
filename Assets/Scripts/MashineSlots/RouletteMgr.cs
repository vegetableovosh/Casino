using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteMgr : MonoBehaviour, IntefaceGame
{
    public GameObject[] SlotSkillObject;
    public Button[] Slot;
    public Sprite[] SkillSprite;

    [System.Serializable]
    public class DisplayItemSlot
    {
        public List<Image> SlotSprite = new List<Image>();
    }
    public DisplayItemSlot[] DisplayItemSlots;

    public Image DispayResultImage;

    [SerializeField]
    private AudioClip rouleteAudio;

    List<int> StartList = new List<int>();
    List<int> ResultIndexList = new List<int>();
    int ItemCnt = 3;

    private void init()
    {
        StartList.Clear();
        ResultIndexList.Clear();
        for (int i = 0; i < ItemCnt; i++)
        {
            SlotSkillObject[i].transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void RunMain()
    {

        init();
        // Populate StartList with indices.
        for (int i = 0; i < SkillSprite.Length; i++)
        {
            StartList.Add(i);
        }

        // Disable all slot buttons.
        for (int i = 0; i < Slot.Length; i++)
        {
            Slot[i].interactable = false;
        }



        // Populate DisplayItemSlots and ResultIndexList.
        for (int i = 0; i < Slot.Length; i++)
        {
            for (int j = 0; j < ItemCnt; j++)
            {
                int randomIndex = Random.Range(0, StartList.Count);

                if (i == 0 && j == 1 || i == 1 && j == 0 || i == 2 && j == 2)
                {
                    ResultIndexList.Add(StartList[randomIndex]);
                }

                DisplayItemSlots[i].SlotSprite[j].sprite = SkillSprite[StartList[randomIndex]];

                if (j == 0)
                {
                    DisplayItemSlots[i].SlotSprite[ItemCnt].sprite = SkillSprite[StartList[randomIndex]];
                }
                StartList.RemoveAt(randomIndex);
            }
        }
        this.GetComponent<AudioSource>().PlayOneShot(rouleteAudio);
        // Start slot coroutines.
        for (int i = 0; i < Slot.Length; i++)
        {
            StartCoroutine(StartSlot(i));
        }


    }

    int[] answer = { 2, 3, 1 };

    
    IEnumerator StartSlot(int SlotIndex)
    {
        for (int i = 0; i < (ItemCnt * (6 + SlotIndex * 4) + answer[SlotIndex]) * 2; i++)
        {
            SlotSkillObject[SlotIndex].transform.localPosition -= new Vector3(0, 50f, 0);
            if (SlotSkillObject[SlotIndex].transform.localPosition.y < 50f)
                SlotSkillObject[SlotIndex].transform.localPosition += new Vector3(0, 300f, 0);
            yield return new WaitForSeconds(0.005f);
        }
        // Enable slot buttons after the slot stops spinning.
        for (int i = 0; i < Slot.Length; i++)
        {
            Slot[i].interactable = true;
        }
        if (
            SlotSkillObject[0].transform.localPosition.y == 100
            &&
            SlotSkillObject[1].transform.localPosition.y == 300
            &&
            SlotSkillObject[2].transform.localPosition.y == 200
           )
            CheckCombination();
    }



    public void CheckCombination()
    {
        float x = 0;

        if (SkillSprite[ResultIndexList[0]].name == SkillSprite[ResultIndexList[1]].name &&
            SkillSprite[ResultIndexList[1]].name == SkillSprite[ResultIndexList[2]].name &&
            SkillSprite[ResultIndexList[2]].name == "lemon")
            x = 5;

        if (SkillSprite[ResultIndexList[0]].name == SkillSprite[ResultIndexList[1]].name &&
            SkillSprite[ResultIndexList[1]].name == SkillSprite[ResultIndexList[2]].name &&
            SkillSprite[ResultIndexList[2]].name == "starwbarry")
            x = 10;

        if (SkillSprite[ResultIndexList[0]].name == SkillSprite[ResultIndexList[1]].name &&
            SkillSprite[ResultIndexList[1]].name == SkillSprite[ResultIndexList[2]].name &&
            SkillSprite[ResultIndexList[2]].name == "melon")
            x = 20;

        if (SkillSprite[ResultIndexList[0]].name == SkillSprite[ResultIndexList[1]].name &&
            SkillSprite[ResultIndexList[1]].name == SkillSprite[ResultIndexList[2]].name &&
            SkillSprite[ResultIndexList[2]].name == "cherry")
            x = 30;

        if (SkillSprite[ResultIndexList[0]].name == SkillSprite[ResultIndexList[1]].name &&
            SkillSprite[ResultIndexList[1]].name == SkillSprite[ResultIndexList[2]].name &&
            SkillSprite[ResultIndexList[2]].name == "banan")
            x = 50;

        if (SkillSprite[ResultIndexList[0]].name == SkillSprite[ResultIndexList[1]].name &&
            SkillSprite[ResultIndexList[1]].name == SkillSprite[ResultIndexList[2]].name &&
            SkillSprite[ResultIndexList[2]].name == "7")
            x = 100;

        if (
            (((SkillSprite[ResultIndexList[0]].name == SkillSprite[ResultIndexList[1]].name) && SkillSprite[ResultIndexList[0]].name == "banan")
            &&
            (SkillSprite[ResultIndexList[1]].name != SkillSprite[ResultIndexList[2]].name))
            ||
            (((SkillSprite[ResultIndexList[0]].name == SkillSprite[ResultIndexList[2]].name) && SkillSprite[ResultIndexList[2]].name == "banan")
            &&
            (SkillSprite[ResultIndexList[0]].name != SkillSprite[ResultIndexList[1]].name))
            ||
            (((SkillSprite[ResultIndexList[1]].name == SkillSprite[ResultIndexList[2]].name) && SkillSprite[ResultIndexList[1]].name == "banan")
            &&
            (SkillSprite[ResultIndexList[1]].name != SkillSprite[ResultIndexList[0]].name))
            )
            x = 7;

        if (
            (((SkillSprite[ResultIndexList[0]].name == SkillSprite[ResultIndexList[1]].name) && SkillSprite[ResultIndexList[0]].name == "7")
            &&
            (SkillSprite[ResultIndexList[1]].name != SkillSprite[ResultIndexList[2]].name))
            ||
            (((SkillSprite[ResultIndexList[0]].name == SkillSprite[ResultIndexList[2]].name) && SkillSprite[ResultIndexList[2]].name == "7")
            &&
            (SkillSprite[ResultIndexList[0]].name != SkillSprite[ResultIndexList[1]].name))
            ||
            (((SkillSprite[ResultIndexList[1]].name == SkillSprite[ResultIndexList[2]].name) && SkillSprite[ResultIndexList[1]].name == "7")
            &&
            (SkillSprite[ResultIndexList[1]].name != SkillSprite[ResultIndexList[0]].name))
            )
            x = 5;
                    
        
            this.GetComponent<StopBet>().Stop();

            this.GetComponent<Bet>().Return_rate(x);

    }


}
