using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public float camSpeed;
    private void LateUpdate()
    {
        //transform.position = Vector2.MoveTowards(transform.position, Follow_Manager.instance.all_sponed_Unit[0].transform.position, camSpeed*Time.deltaTime);
      //  transform.position += new Vector3(0, 0, -10);
    }
}
