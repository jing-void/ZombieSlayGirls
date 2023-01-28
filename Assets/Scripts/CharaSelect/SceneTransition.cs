using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    GameManagerData gameManagerData;
    void Start()
    {
        gameManagerData = FindObjectOfType<GameSystemManager>().GetGameManagerData();
    }

    public void GoToOherScene(string stage)
    {
        gameManagerData.SetNextSceneName(stage);
        SceneManager.LoadScene("CharaSelectScene");
    }
}
