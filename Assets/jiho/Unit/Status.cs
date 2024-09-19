using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [Header("기본스택")]
    [SerializeField] protected float f_Hp;
    [SerializeField] protected float curHp;
    [SerializeField] protected float f_Mp;
    [SerializeField] protected float curMp;

    [Header("마나재생률")]
    [SerializeField] protected float regenerateMp;

    [SerializeField] protected float f_Damege;
    [SerializeField] public float curDamege;

    [SerializeField] protected float attackDelay;
    [SerializeField] protected float curAttackDelay;

    [Header("이동속도 관련")]
    [SerializeField] protected float f_Speed;
    [SerializeField] protected float curSpeed;

    [Header("공격범위")]
    [SerializeField] protected LayerMask attackLayer;
    [SerializeField] protected float attackRange;

    private void Awake()
    {
        init();
    }

    public virtual void Start()
    {
       
    }

    void init()
    {
        curHp = f_Hp;
        curDamege = f_Damege;
        curSpeed = f_Speed;

      
    }

    public float ReturnMaxHp//최대 체력
    {
        get { return f_Hp; }
        set { f_Hp = value; }
    }

    public float RetrunHP//현재 체력
    {
        get
        { return curHp; }

        set
        { curHp = value; }
    }
    public float ReturnMaxMP//최대 마나
    {
        get
        { return f_Mp; }

        set
        { curMp = value; }
    }
    public float ReturnMP//현재마나
    {
        get
        { return curMp; }

        set
        { curMp = value; }
    }


    public float ReturnRegenerateMp//마나회복량
    {
        get { return regenerateMp; }
        set { regenerateMp = value; }
    }


    public float ReturnF_Damage//초기 데미지
    {
        get { return f_Damege; }
        set { f_Damege = value; }
    }    
    
    public float ReturnCurrentDamage//현재 데미지
    {
        get { return curDamege; }
        set { curDamege = value; }
    }

    public float ReturnAttackDelay//공격딜레이
    {
        get { return attackDelay; }
        set { attackDelay = value; }
    }


    public float ReturnCurrentSpeed//현재 이동속도
    {
        get { return curSpeed; }
        set { curSpeed = value; }
    }

    public float ReturnAttackRange//일반공격 사거리
    {
        get { return attackRange; }
        set { attackRange = value; }
    }

    public virtual void GetDamege(Status s, Skill skill = null)
    {


        if (skill == null)
        {
            curHp -= s.curDamege;
        }
        else
        {
            //스킬이 있는 경우
            if (skill != null)
            {
                curHp -= s.curDamege * skill.skillDamegeRatio;

            }
            else
            {
                curHp -= s.curDamege;
            }


        }

    }



    //=====================군 중 제 어 =========================
    public void Apply_Skill_CCC(string cc, Skill s)
    {

        switch (cc)
        {
            case "Sturn":

                StartCoroutine(GetSturnF(s));
                break;

            case "Slow":
                StartCoroutine(GetSlowF(s));
                break;

            case "Position":
                StartCoroutine(GetPositionF(s));
                break;
        }

    }


    public IEnumerator GetSturnF(Skill s)
    {
        float f_speed = curSpeed;
        curSpeed = 0;

        float timer = 0;
        while (timer <= s.cc_duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        curSpeed = f_speed;

    }

    public IEnumerator GetSlowF(Skill s)
    {
        float f_speed = curSpeed; // 원래 속도 저장
        float slowFactor = 1 - (s.cc_Ratio / 100f); // 퍼센트 값을 비율로 변환
        curSpeed *= slowFactor; // 퍼센트 값을 비율로 변환
        float timer = 0;
        while (timer <= s.cc_duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        curSpeed = f_speed;


    }

    public IEnumerator GetPositionF(Skill s)
    {
        yield return s.CC_Timer;
    }

    //=====================군 중 제 어 =========================
}



