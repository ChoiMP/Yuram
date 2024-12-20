using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fighter : Unit
{
    //중갑을 입은 탱커로 방어와 보호 역할 수행

    //스킬
    /*해당 캐릭터 3m내의 모든적에게 공격력만큼 3번 피해를 줌 (0.1초마다).*/
    public override void UseSkill()
    {
        base.UseSkill();

        List<Status> tartgetEnemy = skill_obj.Circle_Skill(skill_obj);
        skill_obj.skill_Tartget = tartgetEnemy;

        if (tartgetEnemy.Count != 0)
        {
            Status closestEnemy = null;
            float closestDistance = float.MaxValue;
            Vector3 currentPosition = this.transform.position;

            // 가장 가까운 적 찾기
            for (int i = 0; i < tartgetEnemy.Count; i++)
            {
               
                float distance = Vector3.Distance(currentPosition, tartgetEnemy[i].transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = tartgetEnemy[i];
                }
            }

            // 가장 가까운 적에게만 데미지 적용
            if (closestEnemy != null)
            {
                skill_obj.transform.position = closestEnemy.transform.position;
                  closestEnemy.GetComponent<Status>().GetDamege(this, skill_obj);
                print(skill_obj.skillName + "스킬을 사용해서" + closestEnemy.name + "에게 데미지를 입혔습니다");
            }
        }


    }


}
