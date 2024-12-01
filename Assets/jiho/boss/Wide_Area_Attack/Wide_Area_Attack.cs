using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wide_Area_Attack : MonoBehaviour
{
    public GameObject Boss_Explosion;
    public Status boss;
    public List<Status> unit;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }


    public void Attack()
    {
        Boss_Explosion.SetActive(true);
        for (int i=0; i<unit.Count; i++)
        {
            unit[i].GetDamege(boss);
        }
        unit.Clear();
        
    }

    public void enable_Obj()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            unit.Add(collision.GetComponent<Status>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            unit.Remove(collision.GetComponent<Status>());
        }
    }
}
