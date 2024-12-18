using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : Status
{
    public bool shild_C;
    [SerializeField] bool isLongAttack;
    public bullet attack_Prefab;
    public bullet attack_obj;

    public  GameObject[]  player_List; 

    public GameObject player;

    public Vector2 dir;

    public bool lone_Attack_C; // 원거리 공격중 확인
    public bool bat_Check;
    private float move_Time = 0.5f;
    private float setOff_Time = 7f;

    private Rigidbody2D rb;

    public override void Start()
    {
        base.Start();
        if (bat_Check == false)
        {
            player = GameObject.Find("Player");

            player_List = GameObject.FindGameObjectsWithTag("Player");
            int num = Random.Range(0, player_List.Length);
            player = player_List[num];
        }
        else
        {
            rb = GetComponent<Rigidbody2D>();
            dir = player.transform.position - transform.position;
        }
    }

    private void OnEnable()
    {
        if(player != null)
        {
            dir = player.transform.position - transform.position;
        }
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (ItemSelectManager.isSelectMode == true)
        {
            return;
        }



        if (lone_Attack_C == false)
        {
            if (isLongAttack) // 원거리 적이면
            {
                if (player != null)
                {
                    dir = player.transform.position - transform.position;

                    if(Vector2.Distance(player.transform.position, transform.position) >= 2f) // 플레이어와 적이 가까우면 정지
                    {
                        dir = dir.normalized;
                        transform.Translate(dir * curSpeed * Time.deltaTime);
                    }
                }
            }
            else if (shild_C) // 실드 적이면
            {
                // 목표 위치 계산
                Vector3 targetPosition = (Vector3)Enemy_Spawner.instance.cameraCenter;
                // 방향 벡터 계산 (정규화)
                Vector3 direction = (targetPosition - transform.position).normalized;
                // 일정한 속도로 이동
                transform.position += direction * curSpeed * Time.deltaTime;
            }
            else if (bat_Check) // 박쥐
            {
                if(move_Time <= 0)
                {
                    int rand = Random.Range(0, 4);

                    if (rand == 0) { dir += new Vector2(-0.15f, 0); }
                    else if (rand == 1) { dir += new Vector2(0.15f, 0); }
                    else if (rand == 2) { dir += new Vector2(0, 0.15f); }
                    else if (rand == 3) { dir += new Vector2(0, -0.15f); }
                    move_Time = 0.5f;
                }
                else { move_Time -= Time.deltaTime; }

                rb.velocity = dir * curSpeed * 7 * Time.deltaTime;

                if(setOff_Time <= 0)
                {
                    setOff_Time = 5f;
                    transform.position = new Vector3(0, 0, 0);
                    player = null;
                    gameObject.SetActive(false);
                }
                else { setOff_Time -= Time.deltaTime; }
            }
            else
            {
                if (player != null)
                {
                    dir = player.transform.position - transform.position;
                    dir = dir.normalized;
                }
                transform.Translate(dir * curSpeed * Time.deltaTime);
            }
        }
    }

    private void Update()
    {
        Find_Unit();
    }
    

    public void Find_Unit()
    {
        if(isLongAttack)
        {
            Collider2D findplayer = Physics2D.OverlapCircle(transform.position, attackRange, attackLayer);

            if (findplayer != null)
            {
                Collider2D player = Physics2D.OverlapCircle(transform.position, attackRange, attackLayer);

                if (attack_obj == null)
                {
                    if(lone_Attack_C == false) { lone_Attack_C = true; StartCoroutine(LongAttack(0)); }
                }
                else if (attack_obj.gameObject.activeSelf==false)
                {
                    if (lone_Attack_C == false) { lone_Attack_C = true; StartCoroutine(LongAttack(1)); }
                }
            }
        }
    }

    IEnumerator LongAttack(int num)
    {
        yield return new WaitForSecondsRealtime(2f);
        if(num == 0)
        {
            attack_obj = Instantiate(attack_Prefab, transform.position, Quaternion.identity);
            attack_obj.SetDir(dir);
        }
        else
        {
            attack_obj.gameObject.SetActive(true);
            attack_obj.transform.position = transform.position; attack_obj.SetDir(dir);
            attack_obj.SetDir(dir);
        }
        lone_Attack_C = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Unit>().GetDamege(this); 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange * 2);

    }
}
