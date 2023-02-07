using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemManager : MonoBehaviour
{
    public static GameSystemManager instance;
    [SerializeField]
    private GameManagerData gameManagerData = null;

    public GameObject[] selectedPlayer;
    public GameObject currenetCharacter;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (selectedPlayer.Length > 0 && currenetCharacter == null)
        {
            currenetCharacter = selectedPlayer[0];
        }
    }

    public void SetCharacter(GameObject chooseCharaPref)
    {
        currenetCharacter = chooseCharaPref;
    }

    public GameManagerData GetGameManagerData()
    {
        return gameManagerData;
    }

}
