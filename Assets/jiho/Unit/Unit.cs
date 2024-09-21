using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Unit : Status
{
    public SpriteRenderer unitImage;

    [Header("이동 관련")]
    float x;
    float y;
    public bool isMain = false;
    [Header("메인 유닛이 아닌 경우 따라가야하는 오브젝트 유닛")]
    public Transform follow_Unit;//따라가야하는 유닛

    [Header("일반 공격 이펙트 && 생성된 일반공격 오브젝트")]
    [SerializeField] protected Skill attack_Perfab;
    public Skill attack_obj;//생성된 스킬 오브젝트


    [Header("스킬 오브젝트 프리펩 && 생성된 스킬 오브젝트")]
    [SerializeField] protected Skill skill_Perfab;
    public Skill skill_obj;//생성된 스킬 오브젝트

    [SerializeField] Status curTarget;

    [SerializeField] Animator animator;

    public override void Start()
    {
        base.Start();
        init();


        unitImage = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

    }

    public void init()
    {
        Follow_Manager.instance.add_list_Unit(this);
        curHp = f_Hp;
        GetComponent<Unit>().enabled = true;
    }

    void Update()
    {

        FindEnemy();
        Attack();
        UnitMove();
        RegenerateMp_F();


        print("attack_Obj==" + attack_obj + "@@@   skill_Obj==" + skill_obj);

    }

    void UnitMove()//공격을 하지않았을때
    {
        if (isMain)
        {
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");

            transform.Translate(new Vector2(x, y) * curSpeed * Time.deltaTime);

            foreach (Unit unit in Follow_Manager.instance.all_sponed_Unit)
            {
                unit.animator.SetBool("IsWalking", !(x == 0 && y == 0));
            }
        }
        else
        {
            Vector3 direction = (transform.position - follow_Unit.position).normalized;
            Vector2 targetPosition = follow_Unit.position + direction;

            // 오브젝트가 목표 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, curSpeed);


        }
        if (curTarget)
        {
            transform.localScale = new Vector3(curTarget.transform.position.x >= transform.position.x ? 1 : -1, 1, 1);
        }
    }

    void RegenerateMp_F()
    {
        if (curMp < f_Mp)
        {
            curMp += regenerateMp * Time.deltaTime;
        }


    }

    //======== 전투 관련 ======//

    public Collider2D[] FindEnemy()
    {
        if (curTarget)
        {
            if (curTarget.gameObject.activeSelf)
            {
                curTarget = null;
            }
        }
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
                    animator.SetTrigger("Skill");

                }
                else//일반 공격
                {
                    AttackEffect_Generation(curTarget);
                    curTarget.GetDamege(this);
                    animator.SetTrigger("Attack");
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
