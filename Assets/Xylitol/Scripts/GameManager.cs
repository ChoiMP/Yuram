using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public Text xpText;
    public int xp;
    ItemSelectManager itemSelc;
    public int nextLevel = 10;

    private void Awake()
    {
        instance = this;
        itemSelc = GetComponent<ItemSelectManager>();
    }

    void Update()
    {
        XpTextManage();
        if (xp >= nextLevel)
        {

            itemSelc.SelectBoxOn();
            itemSelc.SetItmes();
            nextLevel += 10;
        }
    }

    void XpTextManage()
    {
        xpText.text = "XP: " + xp.ToString();
    }
}
