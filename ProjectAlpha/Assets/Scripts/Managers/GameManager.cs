using System;
using UnityEngine;

namespace DefaultNamespace.Managers
{
    [RequireComponent(typeof(ObjectPool), typeof(ShopManager), typeof(AudioManager))]
    public class GameManager : MonoBehaviour
    {
        #region Variables
        [Header("Settings")]
        [SerializeField] private bool debug = false;
        public enum GameState { inMenu,inPaused, inGame, Results, Dead };
        [ReadOnlyInspector] public GameState gameState;
    
        [Header("Script References")]
        [HideInInspector] public ShopManager shopManager;    
    
        public static event Action OnGameStateChanged;

        #endregion

        #region Mutators
    
        #endregion

        #region Singleton
        //Singleton Instantiation
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this.gameObject);
            else
                Instance = this;

            DontDestroyOnLoad(this);
        }
        #endregion

        #region Unity Messages
        void Start()
        {
            //Application Settings
            Application.targetFrameRate = -1;
            // When Game starts load the menu:
            ChangeGameState(GameState.inMenu);

            shopManager = GetComponent<ShopManager>();
            ObjectPool.Instance.InitializePool();
        
            LoadSave();
        }

        private void OnEnable()
        {
            OnGameStateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            OnGameStateChanged -= OnStateChanged;
        }
        void Update()
        {
            GameStateManager();
        }
        #endregion

        #region Update
        void GameStateManager() // Use these states to determine the actions of other scripts based on where the player/user is in the game.
        {
            switch (gameState)
            {
                case GameState.inMenu:

                    break;
                case GameState.inPaused:

                    break;
                case GameState.inGame:
                
                    break;
                case GameState.Dead:
                    OnDeath();
                    break;
                case GameState.Results:
                
                    break;
            }
        }
        #endregion

        #region Game States
        public void OnDeath()
        {
            //Save Score
            ChangeGameState(GameState.Results);
        }
        public void OnStateChanged()
        {
            switch (gameState)
            {
                case GameState.inMenu:
                    AudioManager.Instance.PlayAudio(AudioTypes.TRACK_MENU);
                    break;
                case GameState.inPaused:
                    break;
                case GameState.inGame:
                    AudioManager.Instance.PlayAudio(AudioTypes.TRACK_GAME);
                    break;
                case GameState.Results:
                    break;
                case GameState.Dead:
                    AudioManager.Instance.PlayAudio(AudioTypes.SFX_GAMEOVER);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Switching Game States
        private void ChangeGameState(GameState state)
        {
            gameState = state;
            OnGameStateChanged?.Invoke();
        }

        #endregion

        #region Game Events

        #endregion

        #region Saving and Loading
        public void Save()
        {
            DataManager.SaveData();
        }

        private void LoadSave()
        {
            if (!DataManager.LoadData()) { LogError("Load File was unable to be read"); return; }
        
            Log("Save has been loaded");
        }

        /// <summary>
        /// Permanently Deletes Save Files
        /// </summary>
        public void ResetSave()
        {
            DataManager.ResetData();
        }
    
        private void OnApplicationQuit()
        {
            Save();
        }
        #endregion

        #region Logging Functions
        private void Log(string msg)
        {
            if (!debug) return;

            Debug.Log("[GAMEMANAGER]: " + msg);
        }
        private void LogWarning(string msg)
        {
            Debug.LogWarning("[GAMEMANAGER]: " + msg);
        }
        private void LogError(string msg)
        {
            Debug.LogError("[GAMEMANAGER]: " + msg);
        }
        #endregion
    }
}