using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Unity.VisualScripting;
using TMPro;

public class ItemSelectManager : MonoBehaviour
{
    public Image selectBox;
    Player player;

    public static bool isSelectMode;

    private float time = 10f;
    public Image timerBar;
    public TextMeshProUGUI timerText;

    private int randTrigger;
    List<int> randNumList = new List<int>();
    private int randNum;

    private int itemTrigger;
    private int maxItemCount = 3; //선택 유물 개수 
    List<int> itemNumList = new List<int>();
    private int itemNum;

    public Transform[] selectPos;
    public GameObject[] items;

    public GameObject boxOpenEffect;
    public GameObject uiCam;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        // selectBox.SetActive(false);
        selectBox.transform.localScale = Vector3.zero;
        randTrigger = 1;
        itemTrigger = 1;

        boxOpenEffect.SetActive(false);

    }

    void Update()
    {
        SelectBoxTimer();
        SelectBoxTimerBar();
        SetNum();
        SetItemNum();
    }

    public void SelectBoxOn()
    {
        if ((player.playerXp % 5) == 0)
        {
            selectBox.transform.localScale = Vector3.one;
            isSelectMode = true;
            boxOpenEffect.SetActive(true);
        }
    }

    public void SelectBoxOff() 
    {
        SetItemsInit();
        selectBox.transform.localScale = Vector3.zero;
        randTrigger = 1;
        itemTrigger = 1;
        isSelectMode = false;
        boxOpenEffect.SetActive(false);

    }

    public void SelectBoxTimer()
    {
        if (isSelectMode)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                //selectBox.SetActive(false);
                selectBox.transform.localScale = Vector3.zero;
                isSelectMode = false;
                SetItemsInit();
                randTrigger = 1;
                itemTrigger = 1;
            }
        }

    }
    void SelectBoxTimerBar()
    {
        timerBar.fillAmount = time / 10f;
        timerText.text = "남은 시간: " + time.ToString("N1") + "초";

    }

    
    void SetNum() // 유물 배치할 위치 설정
    {
        if (randTrigger == 1)
        {          
            for (int i=0; i<selectPos.Length; i++)
            {
                do
                {
                    randNum = Random.Range(0, selectPos.Length);
                }
                while (randNumList.Contains(randNum));

                randNumList.Add(randNum);        
                //Debug.Log(randNumList[i]);
            }

            randTrigger = 0;
        }
    }

    void SetItemNum() //선택 유물 번호 설정
    {
        if (itemTrigger == 1)
        {
            for (int i = 0; i < maxItemCount; i++)
            {
                do
                {
                    itemNum = Random.Range(0, items.Length);
                }
                while (itemNumList.Contains(itemNum));

                itemNumList.Add(itemNum);
               // Debug.Log(itemNumList[i]);
            }
            itemTrigger = 0;
        }
    }


    public void SetItmes() //유물 배치
    {
        if (isSelectMode)
        {

            GameObject setItem1 = Instantiate(items[itemNumList[0]]) as GameObject;
            GameObject setItem2 = Instantiate(items[itemNumList[1]]) as GameObject;
            GameObject setItem3 = Instantiate(items[itemNumList[2]]) as GameObject;

            setItem1.transform.SetParent(selectPos[randNumList[0]].transform,false);
            setItem2.transform.SetParent(selectPos[randNumList[1]].transform, false);
            setItem3.transform.SetParent(selectPos[randNumList[2]].transform, false);

        }
    }

    public void SetItemsInit() //배치 초기화
    {
        for(int i=0; i < selectPos.Length; i++)
        {
            foreach (Transform child in selectPos[i])
            {
                Destroy(child.gameObject);
            }
        }
      
        randNumList.Clear();
        itemNumList.Clear();
        


        time = 10f;


    }

   
}
