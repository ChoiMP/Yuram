using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenItemCount : MonoBehaviour
{
    public int count_HP = 0;
    public int count_MP = 0;
    public int count_Power = 0;
    public int count_Skill = 0;
    public int count_Speed = 0;

    public int maxCount = 5;

    public GameObject thisHp = null;
    public GameObject thisMp = null;
    public GameObject thisPower = null;
    public GameObject thisSkill = null;
    public GameObject thisSpeed = null;

    public bool getHP = false;
    public bool getMP = false;
    public bool getPower = false;
    public bool getSkill = false;
    public bool getSpeed = false;


    void Start()
    {
    }

    void Update()
    {
    
    }

}
