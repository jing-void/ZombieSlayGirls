using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public enum SelectBGM
    {
        TITLE,
        CHARASELECT,

    };

    public enum SelectSE
    {
        DASH,

    };

    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioClip[] audioClips;

    [SerializeField] AudioSource seSource;
    [SerializeField] AudioClip[] seClips;


    private void Start()
    {
    }

    public static SoundController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(SelectBGM bgm)
    {
        seSource.PlayOneShot(seClips[(int)bgm]);
    }
   
    
    public void PlaySE(SelectSE se)
    {
        seSource.PlayOneShot(seClips[(int)se]);
    }
}
