using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public TextMeshProUGUI xpText;
    public int xp;
    ItemSelectManager itemSelc;
    public int nextLevel = 20;

    public AudioSource openAudio;
    public AudioSource selectAudio;

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
            nextLevel += 20;
        }
    }

    void XpTextManage()
    {
        xpText.text = "XP: " + xp.ToString();
    }



    public void PauseGame()
    {
        int units = Follow_Manager.instance.all_sponed_Unit.Count;
        for(int i =0; i<units; i++)
        {
            Follow_Manager.instance.all_sponed_Unit[i].gameObject.tag = "Untagged";
            Follow_Manager.instance.all_sponed_Unit[i].gameObject.SetActive(false);
        }
        Enemy_Spawner.instance.GetComponent<Enemy_Spawner>().enabled = false;
    }

    public void continueGame()
    {
        int units = Follow_Manager.instance.all_sponed_Unit.Count;
        for (int i = 0; i < units; i++)
        {
            Follow_Manager.instance.all_sponed_Unit[i].gameObject.tag = "Player";
            Follow_Manager.instance.all_sponed_Unit[i].gameObject.SetActive(true);

        }
        Enemy_Spawner.instance.GetComponent<Enemy_Spawner>().enabled = true;
    }


    public void AudioPlay(string a)
    {
        switch (a) 
        {
            case "open":
                openAudio.Play();
                break;

            case "select":
                selectAudio.Play();
                break;
        }
    }
}
