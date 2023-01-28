using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemManager : MonoBehaviour
{
    public static GameSystemManager instance;
    [SerializeField]
    private GameManagerData gameManagerData = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameManagerData GetGameManagerData()
    {
        return gameManagerData;
    }

}
