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

    Vector2 dir;

    public override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");

        player_List = GameObject.FindGameObjectsWithTag("Player");
        int num = Random.Range(0, player_List.Length);
        player = player_List[num]; 

       
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (shild_C)
        {
            // 목표 위치 계산
            Vector3 targetPosition = (Vector3)Enemy_Spawner.instance.cameraCenter;
            // 방향 벡터 계산 (정규화)
            Vector3 direction = (targetPosition - transform.position).normalized;
            // 일정한 속도로 이동
            transform.position += direction * curSpeed * Time.deltaTime;
        }
        else
        {
            dir = player.transform.position - transform.position;
            dir = dir.normalized;

            transform.Translate(dir * curSpeed * Time.deltaTime);
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
            Collider2D findplayer = Physics2D.OverlapCircle(transform.position, attackRange * 2, attackLayer);

            if (findplayer != null)
            {
                Collider2D player = Physics2D.OverlapCircle(transform.position, attackRange, attackLayer);

                if (attack_obj == null)
                {
                    attack_obj = Instantiate(attack_Prefab, transform.position, Quaternion.identity); 
                    attack_obj.SetDir(dir); 
                }
            

                if (attack_obj.gameObject.activeSelf==false)
                {
                  
                    attack_obj.gameObject.SetActive(true); 
                    attack_obj.transform.position = transform.position; attack_obj.SetDir(dir);
                    attack_obj.SetDir(dir);
                }
            }
        }
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
