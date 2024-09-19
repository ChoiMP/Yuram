using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Unit
{
    public override void UseSkill()
    {
        base.UseSkill();
        StartCoroutine(repit_Skill());
        //skill_Perfab.Skill_Generation(this,transform.position);


    }

    IEnumerator repit_Skill()
    {
        for (int j = 0; j < 3; j++)
        {
            List<Status> tartgetEnemy = skill_obj.Circle_Skill(skill_obj);
            skill_obj.skill_Tartget = tartgetEnemy;


            if (tartgetEnemy.Count != 0)
            {
                for (int i = 0; i < tartgetEnemy.Count; i++)
                {
                    tartgetEnemy[i].GetComponent<Status>().GetDamege(this, skill_obj);
                    print(skill_obj.skillName + "스킬을 사용해서" + tartgetEnemy[i].name + "에게 데미지를 입혔습니다");

                }
            }

            yield return new WaitForSeconds(0.1f);
        }


    }
}
