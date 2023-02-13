using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static int gameData1 = 22;


    public static SceneTransition instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
 

    public void LoadTo(string sceneName)
    {
        FadeIO.instance.FadeOutToIn(() => Load(sceneName));
    }

    void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Title()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void CharaSelect()
    {
        SceneManager.LoadScene("CharaSelectScene");
    }

    public void PlayScene()
    {
        SceneManager.LoadScene("PlayScene");    }
}
