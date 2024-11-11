using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : InvenItemCount
{
    public GameObject invenObj;

    public GameObject[] invenImg;

    public GameObject addPos;

    public int currPosNum; //현재 추가 위치
    public int maxPosNum; //최대 추가 위치

    public int currImgNum;

    public int[] curr_last_Num = new int[2];
    public int[] getNums = new int[5]; //선택한 번호 저장

    public bool isSame;




    void Start()
    {
        invenObj.transform.localScale = Vector3.zero;
        currPosNum = 0;
        maxPosNum = 8;
        currImgNum = 0;
        curr_last_Num[0] = 100; //현재 번호
        curr_last_Num[1] = 200; //이전 번호_
        NumInit();
        isSame = false;
    }

    void NumInit()
    {
        for (int i = 0; i < getNums.Length; i++)
        {
            getNums[i] = 1000;
        }
    }

    void Update()
    {
        InventoryOpenClose();
    }

    void InventoryOpenClose()
    {
        bool invenDown = Input.GetKey(KeyCode.Tab);

        if (invenDown)
        {
            invenObj.transform.localScale = Vector3.one;
        }
        else
        {
            invenObj.transform.localScale = Vector3.zero;
        }

    }

    public void AddToInventory()
    {
        Transform pos = addPos.transform.GetChild(currPosNum).transform;

        if (invenImg[currImgNum] != null)
            Instantiate(invenImg[currImgNum], pos.transform);

        invenImg[currImgNum] = null;

        SetCountBox(currImgNum);

        //curr_last_Num[0] = currImgNum; //현재 번호 할당

        AlreadyGetCheck();

        if (currPosNum <= maxPosNum)
        {
            if (!isSame)
            {
                ++currPosNum;
            }           
        }

        //curr_last_Num[1] = curr_last_Num[0]; //이전 번호 할당
        //curr_last_Num[0] = 100; //현재 번호 초기화
    }

    void AlreadyGetCheck()
    {
        int findNum = currImgNum;
        int inArr = Array.IndexOf(getNums, findNum);
        Debug.Log(inArr);
        if (inArr == -1)
        {
            getNums[currImgNum] = currImgNum;
            isSame = false;
        }
        else
            isSame = true;
    }

    public void ItemTypeChcek(string type)
    {
        switch (type)
        {
            case "HP":
                currImgNum = 0;
                count_HP++;
                getHP = true;
                break;
            case "MP":
                currImgNum = 1;
                count_MP++;
                getMP = true;
                break;
            case "Power":
                currImgNum = 2;
                count_Power++;
                getPower = true;
                break;
            case "Skill":
                currImgNum = 3;
                count_Skill++;
                getSkill = true;
                break;
            case "Speed":
                currImgNum = 4;
                count_Speed++;
                getSpeed = true;
                break;
        }
    }

    void SetCountBox(int num)
    {
        switch (num)
        {
            case 0:
                if (getHP)
                {
                    if (thisHp == null)
                    {
                        thisHp = addPos.transform.GetChild(currPosNum).GetChild(0).transform.gameObject;
                        getHP = false;
                    }
                }
                break;

            case 1:
                if (getMP)
                {
                    if (thisMp == null)
                    {
                        thisMp = addPos.transform.GetChild(currPosNum).GetChild(0).transform.gameObject;
                        getMP = false;
                    }
                }
                break;
            case 2:
                if (getPower)
                {
                    if (thisPower == null)
                    {
                        thisPower = addPos.transform.GetChild(currPosNum).GetChild(0).transform.gameObject;
                        getPower = false;
                    }
                }
                break;
            case 3:
                if (getSkill)
                {
                    if (thisSkill == null)
                    {
                        thisSkill = addPos.transform.GetChild(currPosNum).GetChild(0).transform.gameObject;
                        getSkill = false;
                    }
                }
                break;
            case 4:
                if (getSpeed)
                {
                    if (thisSpeed == null)
                    {
                        thisSpeed = addPos.transform.GetChild(currPosNum).GetChild(0).transform.gameObject;
                        getSpeed = false;
                    }
                }
                break;
        }            
    }


    public void CountBoxControl(int i)
    {

        switch (i) 
        {
            case 10:
                if (count_HP > 1)
                {
                    thisHp.transform.GetChild(0).GetChild(count_HP-1).GetComponent<Image>().color = Color.green;
                }            
                break;
            case 11:
                if (count_MP > 1)
                {
                    thisMp.transform.GetChild(0).GetChild(count_MP-1).GetComponent<Image>().color = Color.green;
                }
                break;
            case 12:
                if (count_Power > 1)
                {
                    thisPower.transform.GetChild(0).GetChild(count_Power-1).GetComponent<Image>().color = Color.green;
                }
                break;
            case 13:
                if (count_Skill > 1)
                {
                    thisSkill.transform.GetChild(0).GetChild(count_Skill-1).GetComponent<Image>().color = Color.green;
                }
                break;
            case 14:
                if (count_Speed > 1)
                {
                    thisSpeed.transform.GetChild(0).GetChild(count_Speed-1).GetComponent<Image>().color = Color.green;
                }
                break;        
        }
    }
}
