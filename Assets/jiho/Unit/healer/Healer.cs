using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Unit
{

    //중갑을 입은 탱커로 방어와 보호 역할 수행
    List<Skill> healSkill_ObjPool=new List<Skill>();
    //스킬
    /*모든 아군을 공격력 * 5 만큼 회복*/
    public override void UseSkill()
    {
        base.UseSkill();
        skill_obj.gameObject.SetActive(false);


        if(healSkill_ObjPool.Count==0)
        {
            for(int i=0; i<4; i++)
            {
                healSkill_ObjPool.Add(Instantiate(skill_Perfab, transform.position, Quaternion.identity));
                healSkill_ObjPool[i].transform.parent = Follow_Manager.instance.all_sponed_Unit[i].transform;
            }
        }
        else
        {
            foreach(Skill s in healSkill_ObjPool)
            {
                s.gameObject.SetActive(true);
            }
        }





        for(int i=0; i< Follow_Manager.instance.all_sponed_Unit.Count; i++)
        {
            Follow_Manager.instance.all_sponed_Unit[i].RetrunHP += ReturnCurrentDamage * 5;
            healSkill_ObjPool[i].transform.position = Follow_Manager.instance.all_sponed_Unit[i].transform.position;
            //최대 체력보다 체력이 많으면 최대 체력으로 바꿔준다
            if (Follow_Manager.instance.all_sponed_Unit[i].RetrunHP > Follow_Manager.instance.all_sponed_Unit[i].ReturnMaxHp)
            {
                Follow_Manager.instance.all_sponed_Unit[i].RetrunHP = Follow_Manager.instance.all_sponed_Unit[i].ReturnMaxHp;
            }
        }
     
      
    }
}
