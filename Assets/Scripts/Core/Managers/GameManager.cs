using UnityEngine;
using DeadZone.Core.Events;
using System.Collections.Generic;

namespace DeadZone.Core.Managers
{
    /// <summary>
    /// Central game manager that handles overall game state and flow.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private bool isPaused = false;
        [SerializeField] private GameState currentGameState = GameState.MainMenu;
        [SerializeField] private float deltaTimeScale = 1f;

        public GameState CurrentGameState => currentGameState;
        public bool IsPaused => isPaused;
        public float DeltaTimeScale => deltaTimeScale;

        // Events
        public GameEvent OnGameStateChanged { get; private set; }
        public GameEvent OnGamePaused { get; private set; }
        public GameEvent OnGameResumed { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            OnGameStateChanged = new GameEvent();
            OnGamePaused = new GameEvent();
            OnGameResumed = new GameEvent();
        }

        public void SetGameState(GameState newState)
        {
            if (currentGameState == newState) return;

            currentGameState = newState;
            OnGameStateChanged.Invoke();
        }

        public void PauseGame()
        {
            if (isPaused) return;

            isPaused = true;
            Time.timeScale = 0f;
            OnGamePaused.Invoke();
        }

        public void ResumeGame()
        {
            if (!isPaused) return;

            isPaused = false;
            Time.timeScale = deltaTimeScale;
            OnGameResumed.Invoke();
        }

        public void SetTimeScale(float scale)
        {
            deltaTimeScale = scale;
            if (!isPaused)
            {
                Time.timeScale = scale;
            }
        }
    }

    public enum GameState
    {
        MainMenu,
        Loading,
        Playing,
        Paused,
        GameOver,
        Victory
    }
}
