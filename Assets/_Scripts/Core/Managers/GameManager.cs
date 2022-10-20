using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private string m_currentLevelName = string.Empty;
    List<AsyncOperation> m_loadOperations = new List<AsyncOperation>();
    private List<GameObject> m_instancedSystemPrefabs;


    public GameObject[] systemPrefabs;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);        
    }
    void Start()
    {
        LoadLevel("GameScene");
        InstantiateSystemPrefabs();
    }
    public void LoadLevel(string levelName )
    {
        m_currentLevelName = levelName;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (asyncOperation == null)
        {
            Debug.LogError("[GameManager] unable to load level");
            return;
        }
        asyncOperation.completed += OnLoadOperationComplete;
        m_loadOperations.Add(asyncOperation);
    }    

    public void UnloadLevel(string levelName)
    {
        m_currentLevelName = levelName;
        AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(levelName);
        if (asyncOperation == null)
        {
            Debug.LogError("[GameManager] unable to unload level");
            return;
        }
        asyncOperation.completed += OnUnLoadOperationComplete;
    }

    void OnLoadOperationComplete(AsyncOperation asyncOperation)
    {
        if (m_loadOperations.Contains(asyncOperation))
        {
            m_loadOperations.Remove(asyncOperation);
            // Dispatch message transition between scenes
        }
    }

    void OnUnLoadOperationComplete(AsyncOperation asyncOperation)
    {
        Debug.Log("Unload completed");
    }

     void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i< systemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(systemPrefabs[i]);
            m_instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        for (int i = 0; i < m_instancedSystemPrefabs.Count; i++)
        {
            Destroy(m_instancedSystemPrefabs[i]);
        }
        m_instancedSystemPrefabs.Clear();
    }
}
