using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource AudioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();//0일반 1 보스

    // Start is called before the first frame update
    void Awake()
    {
        Instance= this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeSound(AudioClip newSound)
    {
        AudioSource.clip = newSound;
        AudioSource.Play();
    }
}
