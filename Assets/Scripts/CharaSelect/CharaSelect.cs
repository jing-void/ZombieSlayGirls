using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class CharaSelect : MonoBehaviour
{
    GameManagerData gameManagerData;
    GameObject startButton,cancelButton;
    

    int selectCharaNum = 0;

    private void Start()
    {
        gameManagerData = FindObjectOfType<GameSystemManager>().GetGameManagerData();
        startButton = GameObject.Find("StartButton");
        cancelButton = GameObject.Find("CancelButton");
        startButton.SetActive(false);
        cancelButton.SetActive(false);

    }

    public void OnSelectUnity(GameObject unityChan)
    {
        // ボタンの選択状態を解除
        EventSystem.current.SetSelectedGameObject(null);
        gameManagerData.SetUnity(unityChan);
        startButton.SetActive(true);
    }

    public void OnSelectAcquire(GameObject acquire)
    {
        // ボタンの選択状態を解除
        EventSystem.current.SetSelectedGameObject(null);
        gameManagerData.SetAcquire(acquire);
        startButton.SetActive(true);
    }




    public void SelectUnity()
    {
        selectCharaNum = 0;
    }

    public void SelectAcquire()
    {
        selectCharaNum = 1;
    }

    public void OnClickToStart()
    {
        PlayerPrefs.SetInt("CHARACTER_NUM",selectCharaNum);
        SceneManager.LoadScene("PlayScene");
    }

}
