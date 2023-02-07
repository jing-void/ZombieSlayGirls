using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class CharaButtonSelect : MonoBehaviour
{
    GameManagerData gameManagerData;
    public Button unityButton, acquireButton;
    ColorBlock unityColor, acquireColor;
    GameObject startButton,cancelButton;

    public GameObject optionPrefab;
    public Transform prevCharacter;
    public Transform selectedCharacter;

    float time,flushSpeed = 4f;
    int selectCharaNum = 0;

    private void Start()
    {
        gameManagerData = FindObjectOfType<GameSystemManager>().GetGameManagerData();
        startButton = GameObject.Find("StartButton");
        cancelButton = GameObject.Find("CancelButton");
        unityButton = GetComponentInChildren<Button>();
        acquireButton = GetComponentInChildren<Button>(); ;

        unityColor = unityButton.colors;
        acquireColor = acquireButton.colors;

        startButton.SetActive(false);
        cancelButton.SetActive(false);

        foreach (GameObject obj in GameSystemManager.instance.selectedPlayer)
        {
            GameObject option = Instantiate(optionPrefab, transform);
            Button button = option.GetComponent<Button>();

            button.onClick.AddListener(() =>
            {
                GameSystemManager.instance.SetCharacter(obj);

                if (selectedCharacter != null)
                {
                    prevCharacter = selectedCharacter;
                }

                selectedCharacter = option.transform;
            });
        }
    }

    private void Update()
    {
        if (startButton.activeInHierarchy || cancelButton.activeInHierarchy)
        {
            return;
        }
    }

    public void OnSelectCharacter(GameObject chara)
    {
        EventSystem.current.SetSelectedGameObject(null);
        gameManagerData.SetUnity(chara);
        gameManagerData.SetAcquire(chara);

        startButton.SetActive(true);
        cancelButton.SetActive(true);
    }
   
  /*
    public void OnSelectUnity()
    {
        // ボタンの選択状態を解除
        EventSystem.current.SetSelectedGameObject(null);
        startButton.SetActive(true);
        cancelButton.SetActive(true);

        flushButton(unityColor);
    }
  */
    public void OnSelectAcquire()
    {
        // ボタンの選択状態を解除
        EventSystem.current.SetSelectedGameObject(null);
        startButton.SetActive(true);
        cancelButton.SetActive(true);

        flushButton(acquireColor);
    }

    public void CancelButton()
    {
        if (startButton.activeInHierarchy || cancelButton.activeInHierarchy)
        {
            startButton.SetActive(false);
            cancelButton.SetActive(false);
        }
    }

    public Color ReturnColor(Color color)
    {
        color.a = Mathf.Sin(time);
        return color;
    }
    public ColorBlock flushButton(ColorBlock color)
    {
        time += flushSpeed * Time.deltaTime;
        color.selectedColor = ReturnColor(color.selectedColor);
        return color;

    }
   




}
