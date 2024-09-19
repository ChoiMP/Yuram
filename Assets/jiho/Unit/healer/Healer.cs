using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Unit
{

    //중갑을 입은 탱커로 방어와 보호 역할 수행

    //스킬
    /*모든 아군을 공격력 * 5 만큼 회복*/
    public override void UseSkill()
    {
        base.UseSkill();

        List<Status> tartgetEnemy = skill_obj.Circle_Skill(skill_obj);
        skill_obj.skill_Tartget = tartgetEnemy;


        if (tartgetEnemy.Count != 0)
        {
            for (int i = 0; i < tartgetEnemy.Count; i++)
            {
                skill_obj.HealUnit(tartgetEnemy[i], skill_obj);
            }
        }
    }
}
