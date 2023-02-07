using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
       

    GameManagerData gameManagerData;

    private void Awake()
    {
        if (instance == this)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        gameManagerData = FindObjectOfType<GameSystemManager>().GetGameManagerData();
    }


    public void LoadTo(string sceneName)
    {
        FadeIO.instance.FadeOutToIn(() => Load(sceneName));
    }

    void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void GameStart()
    {
        SceneManager.LoadScene(gameManagerData.GetNextSceneName());
    }

}
