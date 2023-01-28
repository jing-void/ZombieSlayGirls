using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameManagerData", menuName = "GameManagerData")]
public class GameManagerData : ScriptableObject
{
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private GameObject unityPrefab;
    [SerializeField]
    private GameObject acquirePrefab;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            nextSceneName = null;
            unityPrefab = null;
            acquirePrefab = null;
        }
    }

    public void SetNextSceneName(string nextSceneName)
    {
        this.nextSceneName = nextSceneName;
    }

    public string GetNextSceneName()
    {
        return nextSceneName;
    }

    public void SetAcquire(GameObject acquire)
    {
        this.acquirePrefab = acquire;
    }

    public void SetUnity(GameObject unity)
    {
        this.unityPrefab = unity;
    }

    public GameObject GetUnityPrefab()
    {
        return unityPrefab;        
    }

    public GameObject GetAcquirePrefab()
    {
        return acquirePrefab;
    }
}

    
 
