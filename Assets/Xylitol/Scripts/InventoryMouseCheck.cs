using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class InventoryMouseCheck : MonoBehaviour
{
    public GameObject textBox;

    public Text relicName;
    public Text relicEffect;

    public Camera uiCam;

    public GameObject MouseOnEffect;


    void Start()
    {
        textBox.transform.localScale = Vector3.zero;
        textBox.transform.localPosition = Vector3.zero;

        MouseOnEffect.SetActive(false);
        InitText();
    }

    void Update()
    {
        if (IsPointerOverUI())
        {
            textBox.transform.localScale = Vector3.one;

            float x = Input.mousePosition.x + 200f;
            float y = Input.mousePosition.y + 120f;
            float z = 1f;
            textBox.transform.position = uiCam.ScreenToWorldPoint(new Vector3(x, y, z));

            MouseOnEffect.transform.position = uiCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, z));
            MouseOnEffect.SetActive(true);
         
            Debug.Log(Input.mousePosition);
        }
        else
        {
            textBox.transform.localScale = Vector3.zero;
            MouseOnEffect.SetActive(false);

            InitText();
        }
    }  

    private bool IsPointerOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.layer == LayerMask.NameToLayer("UI"))
                if (results[i].gameObject.tag == "Relic")
                {

                    if (results[i].gameObject.name.Contains("hp"))
                        TextInBoxManage("hp");
                    else if (results[i].gameObject.name.Contains("mp"))
                        TextInBoxManage("mp");
                    else if (results[i].gameObject.name.Contains("power"))
                        TextInBoxManage("power");
                    else if (results[i].gameObject.name.Contains("skill"))
                        TextInBoxManage("skill");
                    else if (results[i].gameObject.name.Contains("speed"))
                        TextInBoxManage("speed");

                    return true;             
                }            
        }

        return false;
    }

    public void TextInBoxManage(string relic)
    {  
        switch(relic)
        {
            case "hp":
                relicName.text = "제국의 명령";
                relicEffect.text = "전체 유닛\n" + "체력 " + "<color=FF0000>" + RelicStatus.instance.applied_HP_Value + "</color>" + " 증가";
                break;
            case "mp":
                relicName.text = "영겁의 지팡이";
                relicEffect.text = "전체 유닛\n" + "마나 " + "<color=FF0000>" + RelicStatus.instance.applied_MP_Value + "</color>" + " 증가";
                break;
            case "power":
                relicName.text = "수호 천사";
                relicEffect.text = "전체 유닛\n" + "공격력 " + "<color=FF0000>" + RelicStatus.instance.applied_POWER_Value + "</color>" + "% 증가";
                break;
            case "skill":
                relicName.text = "음전자 망토";
                relicEffect.text = "전체 유닛\n" + "스킬 업그레이드 " + "<color=FF0000>" + RelicStatus.instance.applied_SKILL_Value + "</color>" + " 단계";
                break;
            case "speed":
                relicName.text = "망령의 두건";
                relicEffect.text = "전체 유닛\n" + "속도 " + "<color=FF0000>" + RelicStatus.instance.applied_SPEED_Value + "</color>" + "% 증가";
                break;
        }
    }

    public void InitText()
    {
        relicName.text = "";
        relicEffect.text = "";
    }
  
}
