using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Unit : Status
{
    public string unitName;
    public int Lv = 1;
    public SpriteRenderer unitImage;


    [Header("일반 공격 이펙트 && 생성된 일반공격 오브젝트")]
    [SerializeField] protected Skill attack_Perfab;
    public Skill attack_obj;//생성된 스킬 오브젝트


    [Header("스킬 오브젝트 프리펩 && 생성된 스킬 오브젝트")]
    [SerializeField] protected Skill skill_Perfab;
    public Skill skill_obj;//생성된 스킬 오브젝트
    [SerializeField] bool is_skill_Available = false;//스킬 사용가능 여부

    Vector2 arrivePos = Vector2.zero;//도착할 위치
    [SerializeField] Status curTarget;
    public override void Start()
    {
        base.Start();
        init();


        unitImage = GetComponent<SpriteRenderer>();

    }

    public void init()
    {
        curHp = f_Hp;
        GetComponent<Unit>().enabled = true;
    }

    void Update()
    {

        FindEnemy();
        Attack();
        UnitMove();

    }

    //========이동 관련======//
    public void setMovePos(Vector2 newPos)//GameManager에서 이동할 위치 정해줌
    {
        arrivePos = newPos;

    }

    void UnitMove()//공격을 하지않았을때
    {
        if (arrivePos != Vector2.zero)
        {
            if (arrivePos != (Vector2)transform.position)//움직이는 중
            {
                transform.position = Vector2.MoveTowards(transform.position, arrivePos, curSpeed);
            }

        }



    }




    public void Synthesis()//합성
    {
        Lv++;

        if (Lv == 1)
        {
            is_skill_Available = true;
        }

    }



    //======== 전투 관련 ======//

    public Collider2D[] FindEnemy()
    {
        // 지정된 위치와 반경 내에 있는 attackLayer의 첫 번째 Collider2D 찾기
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, attackRange, attackLayer);

        // 감지된 콜라이더가 있는지 확인
        if (collider.Length != 0 && curTarget == null)
        {
            curTarget = collider[0].GetComponent<Status>();
        }
        else if (collider.Length == 0)
        {
            curTarget = null;
        }

        return collider;
    }

    public void Attack()
    {

        if (curHp >= 0 && curAttackDelay >= attackDelay)
        {

            if (curTarget != null)
            {
                curAttackDelay -= attackDelay;

                //적 공격하기
                if (curMp >= f_Mp)//스킬 공격
                {
                    if (skill_Perfab != null)
                    {
                        skill_Perfab.SkillEffect_Generation(this, curTarget);

                    }

                }
                else//일반 공격
                {
                    AttackEffect_Generation(curTarget);
                    curTarget.GetDamege(this);
                    if (is_skill_Available)
                    {
                        curMp += regenerateMp;
                    }

                }
            }
        }
        else
        {
            curAttackDelay += Time.deltaTime;
        }
    }





    public override void GetDamege(Status s, Skill skill = null)
    {

        base.GetDamege(s);
        Collider2D[] c = FindEnemy();
        //날 때린놈을 먼저때리기
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i].gameObject == s.gameObject)
            {
                curTarget = c[i].GetComponent<Status>();

                return;
            }

        }

        if (curHp <= 0)
        {
            //쓰러짐
        }
    }

    void AttackEffect_Generation(Status s)//일반 공격 이펙트 소환
    {
        if (attack_obj == null)
        {
            attack_obj = Instantiate(attack_Perfab, s.transform.position, Quaternion.identity).GetComponent<Skill>();

        }
        else
        {
            attack_obj.transform.position = s.transform.position;
            attack_obj.gameObject.SetActive(true);

        }
    }

    public virtual void UseSkill()
    {
        if (curMp >= f_Mp)//마나가 최대 마나보다 많으면
        {
            curMp -= f_Mp;
        }

    }


    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
