using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : Status
{
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

        dir = player.transform.position - transform.position;
        dir = dir.normalized;

        transform.Translate(dir * curSpeed * Time.deltaTime);
    }

/*    private void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);

        dir = player.transform.position - transform.position;
        dir=dir.normalized;

        transform.Translate(dir * curSpeed * Time.deltaTime);
    }*/
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Unit>().GetDamege(this); 
        }
    }
}
