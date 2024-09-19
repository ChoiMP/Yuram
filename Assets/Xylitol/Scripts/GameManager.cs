using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Text xpText;

    void Start()
    {
        
    }

    void Update()
    {
        XpTextManage();
    }

    void XpTextManage()
    {
        xpText.text = "XP: "+ player.GetComponent<Player>().playerXp.ToString();
    }
}
