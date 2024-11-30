using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill : Skill_Algorithm
{
    public ParticleSystem particleSystem;
    public AudioSource audioSource;
    public AudioClip attack_Sound;

    [Header("스킬 이름")]
    public string skillName;
    [Header("스킬 범위")]
    [SerializeField] public Vector2 skillRange;
    [Header("스킬계수")]
    [SerializeField] public float skillDamegeRatio;
    [Header("투차체 일 경우")]
    public float speed;

    //lv에 따라 여러번 쓰는 스킬들
    float nextSkill_Waittime;//다음 스킬까지의 시간
    float curtimer;

    [Header("==스킬 군중제어 효과 관련==")]
    public float cc_duration;//cc지속시간
    public float cc_Ratio;//cc계수
    public WaitForSeconds CC_Timer;
    [SerializeField] bool Sturn, Slow, Position;

    private void OnEnable()
    {
        if (GetComponent<ParticleSystem>())
        {
            particleSystem = GetComponent<ParticleSystem>();
            particleSystem.Play();
            Invoke("enable_Particle", 1);
        }


    }

    public void enable_Particle()
    {
        particleSystem.gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (GetComponent<ParticleSystem>())
        {
            particleSystem = GetComponent<ParticleSystem>();
        }
   
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = attack_Sound;
        CC_Timer = new WaitForSeconds(cc_duration);
    }

    private void Update()
    {
        if(particleSystem!=null)
        {
            if (particleSystem.isStopped)
            {
                gameObject.SetActive(false);

            }
        }
        

    }

    public void SkillEffect_Generation(Unit u, Status target)//스킬 생성
    {


        if (apply_Unit == null)
        {
            apply_Unit = u;

        }

        if (apply_Unit.all_skill_obj.Count == 0)
        {
            apply_Unit.all_skill_obj.Add(Instantiate(apply_Unit.skill_Perfab, target.transform.position, Quaternion.identity).GetComponent<Skill>());
            apply_Unit.skill_obj = apply_Unit.all_skill_obj[0];
        }
        else
        {

            for (int j = 0; j < apply_Unit.all_skill_obj.Count; j++)
            {
                if (apply_Unit.all_skill_obj[j].gameObject.activeSelf == false)
                {
                    apply_Unit.all_skill_obj[j].transform.position = target.transform.position;

                    apply_Unit.skill_obj = apply_Unit.all_skill_obj[j];
                    apply_Unit.skill_obj.gameObject.SetActive(true);
                    break;
                }
                else if (apply_Unit.all_skill_obj.Count - 1 == j)
                {
                    apply_Unit.all_skill_obj.Add(Instantiate(apply_Unit.skill_Perfab, target.transform.position, Quaternion.identity).GetComponent<Skill>());

                    apply_Unit.skill_obj = apply_Unit.all_skill_obj[apply_Unit.all_skill_obj.Count - 1];
                    break;
                }
            }


            apply_Unit.UseSkill();
            //공격 생성시에 공격 사운드 실행
            audioSource.Play();


        }

    }


    public void Apply_Skill_CC(List<Status> apply_Status = null)
    {
        for (int i = 0; i < apply_Status.Count; i++)
        {
            if (Sturn)
            {
                apply_Status[i].Apply_Skill_CCC("Sturn", this);
            }

            if (Slow)
            {
                apply_Status[i].Apply_Skill_CCC("Slow", this);
            }

            if (Position)
            {
                apply_Status[i].Apply_Skill_CCC("Position", this);
            }
        }



    }
    //애니메이션끝날때 스킬 제거
    public void ANI_Destroy_Skill()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(speed!=0 && collision.GetComponent<Status>() != null)
        {
            collision.GetComponent<Status>().GetDamege(apply_Unit);
        }
    }
}
