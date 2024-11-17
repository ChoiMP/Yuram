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
                relicEffect.text = "체력 10 증가";
                break;
            case "mp":
                relicName.text = "영겁의 지팡이";
                relicEffect.text = "마나 20 증가";
                break;
            case "power":
                relicName.text = "수호 천사";
                relicEffect.text = "공격력 5% 증가";
                break;
            case "skill":
                relicName.text = "음전자 망토";
                relicEffect.text = "스킬 업그레이드";
                break;
            case "speed":
                relicName.text = "망령의 두건";
                relicEffect.text = "속도 2% 증가";
                break;
        }
    }

    public void InitText()
    {
        relicName.text = "";
        relicEffect.text = "";
    }
  
}
