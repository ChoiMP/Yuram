using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicStatus : MonoBehaviour
{
    public static RelicStatus instance;

    public int increase_HP_Value = 10;
    public int increase_MP_Value = 10;
    public int increase_SKILL_Value = 1;
    public float increase_SPEED_Value = 0.05f;
    public float increase_POWER_Value = 0.1f;


    public int applied_HP_Value;
    public int applied_MP_Value;
    public int applied_SKILL_Value;
    public float applied_SPEED_Value;
    public float applied_POWER_Value;


    void Start()
    {
        instance = this;
        ValueInit();
    }

    void ValueInit()
    {
        applied_HP_Value = 0;
        applied_MP_Value = 0;
        applied_SKILL_Value = 0;
        applied_POWER_Value = 0f;
        applied_SPEED_Value = 0f;
    }
}
