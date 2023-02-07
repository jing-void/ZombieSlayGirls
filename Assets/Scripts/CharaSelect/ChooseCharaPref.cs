using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseCharaPref : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPrefab;
    private int selected = 0;

    void Awake()
    {
        selected = PlayerPrefs.GetInt("PLAYER", 0);
       
    }

    public void ChooseUnity()
    {
        playerPrefab[selected].SetActive(false);
        selected++;

        if (selected >= playerPrefab.Length)
        {
            selected = 0;
        }
            playerPrefab[selected].SetActive(true);
        PlayerPrefs.SetInt("PLAYER", selected);
        
    }

    public void ChooseAcquire()
    {
        playerPrefab[selected].SetActive(false);
        selected--;

        if (selected < 0)
        {
            selected = playerPrefab.Length - 1;
        }
        playerPrefab[selected].SetActive(true);
        PlayerPrefs.SetInt("PLAYER", selected);
    }
     
    public void ChoosePlayer()
    {
        PlayerPrefs.SetInt("PLAYER", selected);
        SceneManager.LoadScene("PlayScene");
    }
 
}
