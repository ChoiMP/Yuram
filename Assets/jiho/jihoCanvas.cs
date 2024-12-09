using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jihoCanvas : MonoBehaviour
{
    public static jihoCanvas Instance;

    public GameObject main_Title;
    public AudioSource audioSource;
    public AudioClip[] soundClip; //0 인게임  1 보스
    // Start is called before the first frame update
    private void Awake()
    {
        Instance=this;
    }

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

        ChangeMusic(soundClip[0]);

    }

    
    public void Boss_Spawn()
    {
        ChangeMusic(soundClip[1]);

    }

    public void Boss_Die()
    {
        ChangeMusic(soundClip[0]);

    }

    public void ChangeMusic(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();

    }


}
