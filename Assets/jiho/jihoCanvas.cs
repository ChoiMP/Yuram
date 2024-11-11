using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jihoCanvas : MonoBehaviour
{
    public GameObject main_Title;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        Time.timeScale = 1;
        main_Title.SetActive(false);
    }
}
