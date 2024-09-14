using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill : Skill_Algorithm
{


    [Header("스킬 이름")]
    public string skillName;
    [Header("스킬 범위")]
    [SerializeField] public Vector2 skillRange;
    [Header("스킬계수")]
    [SerializeField] public float skillDamegeRatio;

    [Header("====스킬 군중제어 효과 관련====")]
    public WaitForSeconds CC_Timer;
    public float cc_duration;//cc지속시간
    public float cc_Ratio;//cc계수
    [SerializeField] bool Sturn, Slow, Position;



    private void Awake()
    {
        CC_Timer = new WaitForSeconds(cc_duration);
    }

    private void Update()
    {


    }

    public void SkillEffect_Generation(Unit u, Status target)//스킬 생성
    {

        if (apply_Unit == null)
        {
            apply_Unit = u;
        }


        if (apply_Unit.skill_obj == null)
        {
            apply_Unit.skill_obj = Instantiate(gameObject, target.transform.position, Quaternion.identity).GetComponent<Skill>();

        }
        else
        {
            apply_Unit.skill_obj.transform.position = target.transform.position;
            apply_Unit.skill_obj.gameObject.SetActive(true);

        }

        apply_Unit.UseSkill();
    }




    public void Apply_Skill_CC(List<Status> apply_Status = null)
    {
        for (int i = 0; i < apply_Status.Count; i++)
        {
            if (Sturn)
            {
                apply_Status[i].Apply_Skill_CCC("Sturn", this);
            }

            if (Slow)
            {
                apply_Status[i].Apply_Skill_CCC("Slow", this);
            }

            if (Position)
            {
                apply_Status[i].Apply_Skill_CCC("Position", this);
            }
        }



    }
    //애니메이션끝날때 스킬 제거
    public void ANI_Destroy_Skill()
    {
        gameObject.SetActive(false);
    }


}