using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectChecking : MonoBehaviour, IPointerClickHandler
{
    // ui 활성화 시 유물 클릭 체크

    GameObject gm;
    GameObject player;

    InventoryManager inven;

    public GameObject selectEffect;
    public Camera uiCam;
    private Vector3 target;

    public static GameObject clickedObject;

    void Start()
    {
        gm = GameObject.Find("GameManager").gameObject;
        player = GameObject.Find("Player").gameObject;
        inven = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

    }

    void Update()
    {
        CreateEffect();
    }

    void CreateEffect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Vector2 mousePos = Input.mousePosition;
                target = uiCam.ScreenToWorldPoint(mousePos);
            }        
        }
    } 

    public void OnPointerClick(PointerEventData eventData)
    {
        clickedObject = eventData.pointerCurrentRaycast.gameObject;
        Debug.Log(clickedObject.name);

        /*if (clickedObject.name.Contains("Speed"))
        {
            player.GetComponent<PlayerStatus >().SpeedUpManager();
        }*/

        if (clickedObject.name.Contains("_Item"))
        {
            Instantiate(selectEffect, target, Quaternion.identity);
            StartCoroutine(CloseBox());

            Follow_Manager fm = Follow_Manager.instance;
            int unitCount = fm.all_sponed_Unit.Count; //유닛 수

            if (clickedObject.name.Contains("HP")) //체력 증가 유물
            {
                inven.ItemTypeChcek("HP");
                inven.AddToInventory();
                inven.CountBoxControl(10);
                for (int i=0; i < unitCount; i++)
                {
                    int hpValue = RelicStatus.instance.increase_HP_Value;
                    fm.all_sponed_Unit[i].RetrunHP += hpValue;
                    if (i == 0)
                        RelicStatus.instance.applied_HP_Value += hpValue;
                }                          
            }
            else if (clickedObject.name.Contains("MP")) //마나 증가 유물
            {
                inven.ItemTypeChcek("MP");
                inven.AddToInventory();
                inven.CountBoxControl(11);
                for (int i = 0; i < unitCount; i++)
                {
                    int mpValue= RelicStatus.instance.increase_MP_Value;
                    fm.all_sponed_Unit[i].ReturnMP += mpValue;
                    if (i == 0)
                        RelicStatus.instance.applied_MP_Value += mpValue;
                }             
            }
            else if (clickedObject.name.Contains("Power")) //공격력 증가 유물
            {
                inven.ItemTypeChcek("Power");
                inven.AddToInventory();
                inven.CountBoxControl(12);
                for (int i = 0; i < unitCount; i++)
                {
                    float powerValue = fm.all_sponed_Unit[i].ReturnF_Damage * RelicStatus.instance.increase_POWER_Value;
                    fm.all_sponed_Unit[i].ReturnCurrentDamage += powerValue;
                    if (i == 0)
                        RelicStatus.instance.applied_POWER_Value += RelicStatus.instance.increase_POWER_Value * 100;
                }
            }
            else if (clickedObject.name.Contains("Skill")) //스킬 업그레이드 유물
            {
                inven.ItemTypeChcek("Skill");
                inven.AddToInventory();
                inven.CountBoxControl(13);
                for (int i = 0; i < unitCount; i++)
                {
                    int skillValue = RelicStatus.instance.increase_SKILL_Value;
                    fm.all_sponed_Unit[i].lv += skillValue;
                    if (i == 0)
                        RelicStatus.instance.applied_SKILL_Value += skillValue;
                }
            }
            else if (clickedObject.name.Contains("Speed")) //이동속도 증가 유물
            {
                inven.ItemTypeChcek("Speed");
                inven.AddToInventory();
                inven.CountBoxControl(14);
                for (int i = 0; i < unitCount; i++)
                {
                    float speedValue = fm.all_sponed_Unit[i].ReturnCurrentSpeed * RelicStatus.instance.increase_SPEED_Value;
                    fm.all_sponed_Unit[i].ReturnCurrentSpeed += speedValue;
                    if (i == 0)
                        RelicStatus.instance.applied_SPEED_Value += RelicStatus.instance.increase_SPEED_Value * 100;
                }
            }
        }

        IEnumerator CloseBox()
        {
            yield return new WaitForSeconds(0.3f);
            gm.GetComponent<ItemSelectManager>().SelectBoxOff();

        }


    }
}
