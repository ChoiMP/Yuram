using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;

    float h;
    float v;

    public float moveSpeed = 5f;
    private float rotSpeed = 30f;

    public int playerXp;

    private Vector3 dirVec;
    public static Vector3 lookDir;

    public GameObject bullet;
    public Transform gunPos;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerXp = 0;
    }

    void Update()
    {
        Move();
        Rotate();
        //GunFire();
    }

    void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 moveVelocity = new Vector3(h, v, 0).normalized * moveSpeed * Time.deltaTime;
        this.transform.position += moveVelocity;

        /* Vector2 pos = rigid.position;
         pos.x = pos.x + 30f * x * Time.deltaTime;
         pos.y = pos.y + 30f * y * Time.deltaTime;

         rigid.MovePosition(pos);*/

    }

    void Rotate()
    {
        if (v == 1)
        {
            dirVec = Vector3.up;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (v == -1)
        {
            dirVec = Vector3.down;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        else if (h == -1)
        {
            dirVec = Vector3.left;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        else if (h == 1)
        {
            dirVec = Vector3.right;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }
        lookDir = dirVec;
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));

    }

    /*void GunFire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, gunPos.position, gunPos.rotation);
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Item")
        {
            // Debug.Log("������");
            collision.transform.localScale = Vector3.zero;
            collision.transform.tag = "Untagged";
            collision.transform.GetComponent<CircleCollider2D>().enabled = false;
            playerXp++;

            ItemSelectManager itemSelc = GameObject.Find("GameManager").GetComponent<ItemSelectManager>();
            itemSelc.SelectBoxOn();
            itemSelc.SetItmes();
           
        }
    }
}
