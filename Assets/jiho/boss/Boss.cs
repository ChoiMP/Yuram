using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Status
{
    public Status main_Player;

    [SerializeField] float move_Timer;
    [SerializeField] float Timer;
    [SerializeField] Vector2 dir;

    //패턴 관련 오브젝트는 아래 있음
    [SerializeField] float pattenTimer;
    [SerializeField] float cur_PattenTimer;
    // Start is called before the first frame update




    private void OnDisable()
    {
        SoundManager.Instance.ChangeSound(SoundManager.Instance.audioClips[0]);
    }

    public override void Start()
    {
        SoundManager.Instance.ChangeSound(SoundManager.Instance.audioClips[1]);

        main_Player = Follow_Manager.instance.all_sponed_Unit[0];

        dir = main_Player.transform.position - transform.position;
        dir = dir.normalized;

        base.Start();
    }

    private void Update()
    {
        Boss_Moving();

        useAttack();
    }

    public void Boss_Moving()
    {
        if(main_Player==null)
        {
            main_Player = Follow_Manager.instance.all_sponed_Unit[0];
        }


        transform.Translate(dir * ReturnCurrentSpeed * Time.deltaTime);

        Timer += Time.deltaTime;

        if(Timer>= move_Timer)
        {
            //1~3초 사이에서 도망갈찌 따라갈지 고민을 한다
            move_Timer = Random.Range(1, 3);
            Timer = 0;
            if (Vector2.Distance(main_Player.transform.position,transform.position)>2)
            {
                //다가가야함
                dir = main_Player.transform.position - transform.position;
                dir = new Vector2(dir.x + Random.Range(-0.2f, 0.2f), dir.y + Random.Range(-0.2f, 0.2f));
                dir = dir.normalized;
            }
            else
            {
                //도망가야함
                dir = transform.position - main_Player.transform.position;
                dir = new Vector2(dir.x + Random.Range(-0.2f, 0.2f), dir.y + Random.Range(-0.2f, 0.2f));
                dir = dir.normalized;

            }
        }
    }


    //공격 패턴
    [Header("투사체")]
    public GameObject projectile;
    [Header("투사체패턴설정")]
    public int projectileCount = 8; // 투사체 개수

    public float projectileSpeed = 5f; // 투사체 발사 속도
    public float delayBetweenProjectiles = 0.2f; // 패턴2에서 투사체 간 생성 간격
    [SerializeField] bool isRunCorutine=false;
    void useAttack()
    {
        cur_PattenTimer += Time.deltaTime;

        if (cur_PattenTimer >= pattenTimer && isRunCorutine == false)
        {
            cur_PattenTimer = 0;
            pattenTimer = Random.Range(2, 6);

            int attack_num= Random.Range(0, 4);


            switch (attack_num) 
                {
                case 0:
                    StartCoroutine("ProjectilePattern1"); isRunCorutine = true;
                    break;
                case 1:
                    StartCoroutine("ProjectilePattern2"); isRunCorutine = true;
                    break;
                case 2:
                    spon_wide_area(Vector2.zero);
                    break;
                case 3:
                    StartCoroutine("wide_Area_Patten1"); isRunCorutine = true;
                    break;           
                case 4:
                    StartCoroutine("wide_Area_Patten2"); isRunCorutine = true;
                    break;
            }


          
        }
    }


    IEnumerator ProjectilePattern1()
    {
      
        // 원형으로 투사체 생성
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * (360f / projectileCount);
            Vector3 spawnPosition = transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right;

            GameObject proj = Instantiate(projectile, spawnPosition, Quaternion.identity);
            
            // 투사체의 방향을 보스 중심에서 바깥으로 설정
            proj.GetComponent<projectile_CS>().dir = proj.transform.position - transform.position;
           
        }

        yield return null;

        isRunCorutine = false;
    }

    // 패턴 2: 시계방향 순서대로 생성 후 발사
    IEnumerator ProjectilePattern2()
    {

        // 시계방향으로 순서대로 투사체 생성
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * (360f / projectileCount);
            Vector3 spawnPosition = transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right ;

            GameObject proj = Instantiate(projectile, spawnPosition, Quaternion.identity);

            // 투사체의 방향을 보스 중심에서 바깥으로 설정
            proj.GetComponent<projectile_CS>().dir = proj.transform.position - transform.position;

            yield return new WaitForSeconds(delayBetweenProjectiles); // 각 투사체 생성 간 대기
        }

        

        // 생성된 순서대로 투사체 발사
       
        isRunCorutine = false;
    }


    [Header("장판 오브젝트")]
    public GameObject wide_area_object;
    public List<GameObject> spon_WideArea_objectpooling; 

    public void spon_wide_area(Vector2 pos)
    {
        Vector2 sponPos = Vector2.zero;
        if (pos==Vector2.zero)
        {
            float x = Random.Range(-5, 5);
            float y = Random.Range(-5, 5);

            sponPos= new Vector2(x + transform.position.x, y + transform.position.y); ;
        }
        else
        {
            sponPos = pos;

        }




        if (spon_WideArea_objectpooling.Count==0)
        {
            GameObject g = Instantiate(wide_area_object, sponPos, Quaternion.identity);
            g.transform.GetChild(0).GetComponent<Wide_Area_Attack>().boss = this;
            
        }
        else
        {
            for(int i=0; i< spon_WideArea_objectpooling.Count; i++)
            {
                if(spon_WideArea_objectpooling[i].gameObject.activeSelf==false)
                {
                    spon_WideArea_objectpooling[i].gameObject.SetActive(true);
                    spon_WideArea_objectpooling[i].transform.position = sponPos;
                    spon_WideArea_objectpooling[i].transform.GetChild(0).GetComponent<Wide_Area_Attack>().boss = this;
                    break;

                }
                else if (spon_WideArea_objectpooling.Count == i - 1)
                {
                   GameObject g =  Instantiate(wide_area_object, sponPos, Quaternion.identity);
                    g.transform.GetChild(0).GetComponent<Wide_Area_Attack>().boss = this;
                    break;
                }
            }
        }
    }


    IEnumerator wide_Area_Patten1()
    {

        // 시계방향으로 순서대로 투사체 생성
        for (int i = 0; i < 10; i++)
        {
            Vector3 spawnPosition = main_Player.transform.position;

            spon_wide_area(spawnPosition);

             yield return new WaitForSeconds(delayBetweenProjectiles); // 각 투사체 생성 간 대기
        }


        isRunCorutine = false;
    }

    IEnumerator wide_Area_Patten2()
    {

        // 시계방향으로 순서대로 투사체 생성
        for (int i = 0; i < 10; i++)
        {
            float angle = i * (360f / projectileCount);
            Vector3 spawnPosition = main_Player.transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right;
            spon_wide_area(spawnPosition);
           
        }
        yield return null;

        isRunCorutine = false;
    }
}
