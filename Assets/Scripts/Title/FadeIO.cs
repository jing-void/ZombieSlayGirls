using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class FadeIO : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public float fadeTime = 1f;
    public static FadeIO instance;

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


    private void Update()
    {
        if (ToCharacterSelectScene())
        {
            SceneManager.LoadScene("PlayScene");

        }
    }


    public void FadeOutCanvas()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 2.5f).OnComplete(() => canvasGroup.blocksRaycasts = false);
    }

    public void FadeInCanvas()
    {
        canvasGroup.blocksRaycasts = true;

        canvasGroup.DOFade(0, 2.5f).OnComplete(() => canvasGroup.blocksRaycasts = false);
    }

    public void FadeOutToIn(TweenCallback action)
    {
        canvasGroup.blocksRaycasts = true;

        canvasGroup.DOFade(1, 2.5f).OnComplete(() =>
        {
            action(); FadeInCanvas();
        });
    }

    public bool ToCharacterSelectScene()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            return true;
        }
        return false;
    }
}
