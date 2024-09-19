using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Unit
{
    //중갑을 입은 탱커로 방어와 보호 역할 수행

    //스킬
    /*해당 캐릭터에서 가장 가까운 적을 향하여 관통하는 화염구를 날림 피해량은 공격력 *2.*/
    public override void UseSkill()
    {
        base.UseSkill();

        skill_obj.transform.position = transform.position;

        List<Status> tartgetEnemy = skill_obj.Circle_Skill(skill_obj);
        skill_obj.skill_Tartget = tartgetEnemy;

        skill_obj.Straight_Projectile_Skill(skill_obj);
    }

     

}
