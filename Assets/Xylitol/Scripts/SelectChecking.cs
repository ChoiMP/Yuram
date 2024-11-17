using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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

            if (clickedObject.name.Contains("HP"))
            {
                inven.ItemTypeChcek("HP");
                inven.AddToInventory();
                inven.CountBoxControl(10);
            }
            else if (clickedObject.name.Contains("MP"))
            {
                inven.ItemTypeChcek("MP");
                inven.AddToInventory();
                inven.CountBoxControl(11);

            }
            else if (clickedObject.name.Contains("Power"))
            {
                inven.ItemTypeChcek("Power");
                inven.AddToInventory();
                inven.CountBoxControl(12);

            }
            else if (clickedObject.name.Contains("Skill"))
            {
                inven.ItemTypeChcek("Skill");
                inven.AddToInventory();
                inven.CountBoxControl(13);

            }
            else if (clickedObject.name.Contains("Speed"))
            {
                inven.ItemTypeChcek("Speed");
                inven.AddToInventory();
                inven.CountBoxControl(14);
            }
        }

        IEnumerator CloseBox()
        {
            yield return new WaitForSeconds(0.3f);
            gm.GetComponent<ItemSelectManager>().SelectBoxOff();

        }


    }
}
