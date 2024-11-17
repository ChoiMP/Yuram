using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector2 dir=Vector2.zero;
    private void OnEnable()
    {
        Invoke("Enable_Obj", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(dir!=Vector2.zero)
        {
            transform.Translate(dir * speed * Time.deltaTime);

        }
    }

    public void SetDir(Vector2 _dir)
    {
        dir = _dir.normalized;
    }

    void Enable_Obj()
    {
        gameObject.SetActive(false);
    }

}
