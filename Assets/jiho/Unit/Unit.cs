using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Unit : Status
{
    public SpriteRenderer unitImage;
    public int lv=1;
    [Header("이동 관련")]
    float x;
    float y;
    public bool isMain = false;
    public enum CurStatus {move,attack,find,just_Home }
    public CurStatus curstatus=CurStatus.move;

    [Header("메인 유닛이 아닌 경우 따라가야하는 오브젝트 유닛")]
    public Transform follow_Unit;//따라가야하는 유닛

    [Header("일반 공격 이펙트 && 생성된 일반공격 오브젝트")]
    [SerializeField] protected Skill attack_Perfab;
    public Skill attack_obj;//생성된 스킬 오브젝트


    [Header("스킬 오브젝트 프리펩 && 생성된 스킬 오브젝트")]
    [SerializeField] public Skill skill_Perfab;
    public List<Skill> all_skill_obj;//생성된 스킬 오브젝트
    public Skill skill_obj;//생성된 스킬 오브젝트

    [Header("따라갈 적")]
    [SerializeField] Status findTarget;

    [SerializeField] Status curTarget;

    [SerializeField] Animator animator;
    [Header("따라갈 유닛의 포지션 offset")]
    [SerializeField] Vector2 offset_pos;
    public override void Start()
    {
        base.Start();
        init();


        Follow_Manager.instance.add_list_Unit(this);
        unitImage = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

    }

    public void init()
    {
        
        curHp = f_Hp;
        GetComponent<Unit>().enabled = true;

        //랜덤 위치 초기화
        int x = Random.Range(-3, 3);
        int y = Random.Range(-3, 3);
        offset_pos = new Vector2(x, y);
    }

    void Update()
    {
        if(curstatus == CurStatus.attack)
        {
            Attack();
        }

        if (curstatus != CurStatus.just_Home)
        {
            FindEnemy();
        }
      
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
            if (curTarget != null)
                if (curTarget.gameObject.activeSelf == false)
                    curTarget = null;


            if (findTarget != null)
                if (findTarget.gameObject.activeSelf == false)
                    findTarget = null;


            if (curstatus==CurStatus.move || curstatus == CurStatus.just_Home)
            {
                // 오브젝트가 목표 위치로 이동
                transform.position = Vector3.MoveTowards(transform.position, follow_Unit.transform.position + (Vector3)offset_pos, curSpeed);
                if(Vector2.Distance(transform.position,follow_Unit.transform.position)<5)
                {
                    curstatus = CurStatus.attack;
                }
            }
         


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

        if(!isMain)
        {
            if (Vector2.Distance(transform.position, follow_Unit.transform.position) > 5)//거리가 5보다 멀어지면 메인 플레이어로 이동
            {
                curstatus = CurStatus.just_Home;
            }
        }



        // 지정된 위치와 반경 내에 있는 attackLayer의 첫 번째 Collider2D 찾기
        Collider2D[] find_collider = Physics2D.OverlapCircleAll(transform.position, attackRange * 2, attackLayer);
        Collider2D collider = Physics2D.OverlapCircle(transform.position, attackRange, attackLayer);

        // 감지된 콜라이더가 있는지 확인
        if (find_collider.Length != 0 && curTarget == null)
        {
            findTarget = find_collider[0].GetComponent<Status>();
           
            if(!isMain)
            {
                //발견한 적으로 이동
                transform.position = Vector2.MoveTowards(transform.position, findTarget.transform.position, 0.05f);
            }
            



            curstatus = CurStatus.attack;
        }
        else if (find_collider.Length == 0)
        {
            findTarget = null;
            curstatus = CurStatus.move;
        }

        if (collider != null)
        {
            curTarget = collider.GetComponent<Status>(); ;
        }
        else
        {
            curTarget = null;
        }

        return find_collider;
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
                        StartCoroutine(nextSkill_Corutine());
                       
                        animator.SetTrigger("Skill");

                    }
                   

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


    IEnumerator nextSkill_Corutine()
    {
        for (int i = 0; i < lv; i++)
        {
            skill_Perfab.SkillEffect_Generation(this, curTarget);
            yield return new WaitForSeconds(0.2f);
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
