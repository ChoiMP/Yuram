using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    Player player;
    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
    }

    void StatusUpManage()
    {
         

    }

    public void SpeedUpManager()
    {
        player.moveSpeed += 0.2f;
    }
}
