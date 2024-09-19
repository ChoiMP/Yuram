using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 dir = Vector3.up;
        transform.Translate(dir* speed * Time.deltaTime);

        Destroy(this.gameObject, 8f);
    }
}
