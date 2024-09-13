using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron_Guardian : Unit
{

    //중갑을 입은 탱커로 방어와 보호 역할 수행

    //스킬
    /*5초간 자신과 주변 아군의 피해를 50% 감소시키고, 종료 시 방패로 적을 밀쳐내어 짧게 스턴시킴.*/
    public override void UseSkill()
    {
        base.UseSkill();

        //skill_Perfab.Skill_Generation(this,transform.position);

        List<Status> tartgetEnemy = skill_obj.Circle_Skill(skill_obj);
        skill_obj.skill_Tartget = tartgetEnemy;

        skill_obj.Apply_Skill_CC(tartgetEnemy);


        if (tartgetEnemy.Count != 0)
        {
            for (int i = 0; i < tartgetEnemy.Count; i++)
            {
                tartgetEnemy[i].GetComponent<Status>().GetDamege(this, skill_obj);
                print(skill_obj.skillName + "스킬을 사용해서" + tartgetEnemy[i].name + "에게 데미지를 입혔습니다");

            }
        }
    }

}
