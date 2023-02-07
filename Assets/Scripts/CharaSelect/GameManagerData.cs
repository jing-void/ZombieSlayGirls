using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameManagerData", menuName = "GameManagerData")]
public class GameManagerData : ScriptableObject
{
    [SerializeField]
    private string nextSceneName;
    [SerializeField,HideInInspector]
    private GameObject unityPrefab;
    [SerializeField,HideInInspector]
    private GameObject acquirePrefab;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            nextSceneName = "";
            unityPrefab = null;
            acquirePrefab = null;
        }
    }
      

    public string GetNextSceneName()
    {
        return nextSceneName;
    }

    public void SetAcquire(GameObject acquirePref)
    {
        this.acquirePrefab = acquirePref;
    }

    public void SetUnity(GameObject unityPref)
    {
        this.unityPrefab = unityPref;
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

    
 
