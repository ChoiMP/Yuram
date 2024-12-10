using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    public GameObject player;

    private void Start()
    {
        player = Follow_Manager.instance.all_sponed_Unit[0].gameObject;
    }

    private void LateUpdate()
    {
        Vector3 dir = player.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.deltaTime, dir.y * cameraSpeed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);
    }
    private void Update()
    {
        
    }
}
