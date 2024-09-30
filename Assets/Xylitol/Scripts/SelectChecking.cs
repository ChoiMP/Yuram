using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectChecking : MonoBehaviour, IPointerClickHandler
{
    // ui 활성화 시 유물 클릭 체크

    GameObject gm;
    GameObject player;

    public static GameObject clickedObject;
    void Start()
    {
        gm = GameObject.Find("GameManager").gameObject;
        player = GameObject.Find("Player").gameObject;
    }

    void Update()
    {
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
            //player.GetComponent<PlayerStatus >().SpeedUpManager();
            gm.GetComponent<ItemSelectManager>().SelectBoxOff();
        }


    }
}
