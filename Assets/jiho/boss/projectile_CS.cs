using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_CS : MonoBehaviour
{
    public Vector2 dir;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
