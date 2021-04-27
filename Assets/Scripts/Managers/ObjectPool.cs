using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Managers
{
    public class ObjectPool : MonoBehaviour
    {
        #region Variables
        public bool debug = false;
    
        [Header("Assignables")] 
        public GameObjectToBePooled[] gameObjectsToBePooled;

        private List<GameObject> _pooledGameObjects = new List<GameObject>();

        private float _debugTimer;

        private bool _isInitialized = false;

        #endregion

        #region Singleton
        //Singleton Instantiation
        public static ObjectPool Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this.gameObject);
            else
                Instance = this;

            DontDestroyOnLoad(this);
        }
        #endregion

        #region Initialize Pool
        public void InitializePool()
        {
            Log("Pooling has started"); 
            _debugTimer = Time.realtimeSinceStartup;

            StartCoroutine(LoadGameObjects());

            _isInitialized = true;
        }

        IEnumerator LoadGameObjects()
        {
            for (int i = 0; i < gameObjectsToBePooled.Length; i++)
            {
                for (int j = 0; j < gameObjectsToBePooled[i].amountToBePooled; j += 10)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        GameObject go = Instantiate(gameObjectsToBePooled[i].gameObjectToBePooled);
                        go.SetActive(false);
                        go.transform.SetParent(transform);
                        _pooledGameObjects.Add(go);
                    }
                    yield return 0;
                }
            }
        
            Log("Pooling has ended, " + _pooledGameObjects.Count + " Pooled Objects");
            Log("Generating Pool Took " + (Time.realtimeSinceStartup - _debugTimer));
            yield return null;
        }
    
        #endregion

        #region Set in Pool
        public void SetObjectInPool(GameObject go)
        {
            go.SetActive(false);
            go.transform.position = Vector3.zero;
        }
        #endregion

        #region Get from Pool
        public GameObject GetObjectFromPool(string name)
        {
            if (!_isInitialized) InitializePool();

            int startPosInList = 0;
            int index = 0;
            for (int i = 0; i < gameObjectsToBePooled.Length; i++)
            {
                if (gameObjectsToBePooled[i].name != name)
                {
                    startPosInList += gameObjectsToBePooled[i].amountToBePooled;
                }
                else
                {
                    index = i;
                    break;
                }
            }

            for (int i = startPosInList; i < startPosInList + gameObjectsToBePooled[index].amountToBePooled; i++)
            {
                if (!_pooledGameObjects[i].activeSelf) return _pooledGameObjects[i];
            }

            Log("No Objects Ready in Pool " + gameObjectsToBePooled[index].name);

            if (gameObjectsToBePooled[index].loadMoreIfNoneLeft)
            {
                Log("Added Object in Pool " + gameObjectsToBePooled[index].name);
                _pooledGameObjects.Insert(startPosInList + gameObjectsToBePooled[index].amountToBePooled, gameObjectsToBePooled[index].gameObjectToBePooled);
                gameObjectsToBePooled[index].amountToBePooled++;
                return _pooledGameObjects[startPosInList + gameObjectsToBePooled[index].amountToBePooled - 1];
            }

            LogError("No Objects Left in Pool");
            return null;
        }
    
        #endregion

        #region Logging Functions
        private void Log(string msg)
        {
            if (!debug) return;

            Debug.Log("[OBJECTPOOL]: " + msg);
        }
        private void LogWarning(string msg)
        {
            Debug.LogWarning("[OBJECTPOOL]: " + msg);
        }
        private void LogError(string msg)
        {
            Debug.LogError("[OBJECTPOOL]: " + msg);
        }
        #endregion
    }

    #region Structs 
    [System.Serializable]
    public struct GameObjectToBePooled
    {
        [Header("Object Info")]
        public string name;
        public int amountToBePooled;
        public GameObject gameObjectToBePooled;

        [Header("Settings")]
        public bool loadMoreIfNoneLeft;
    }

    [System.Serializable]
    public struct LevelInformation
    {
        [Header("Level Info")]
        /// <summary>
        /// Folder Name located under the Assets/Resources/folderName
        /// </summary>
        public string folderName;
        [HideInInspector] public int levelIndex;
        [HideInInspector] public int listIndex;
        [HideInInspector] public int amountOfLevels;
        [HideInInspector] public int amountOfLoadedLevels;

        //[Header("Settings")]
        //public bool loadMoreIfNoneLeft;
    }
    #endregion
}