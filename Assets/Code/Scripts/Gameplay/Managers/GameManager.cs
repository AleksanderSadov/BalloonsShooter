using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BalloonsShooter.Gameplay.Manager
{
    public class GameManager : MonoBehaviour
    {
        public string gameOverSceneName;

        private void Awake()
        {
            EventsManager.Broadcast(new GameStartedEvent());
        }

        private void OnEnable()
        {
            EventsManager.AddListener<PlayerDeathEvent>(OnPlayerDeath);
        }

        private void OnDisable()
        {
            EventsManager.RemoveListener<PlayerDeathEvent>(OnPlayerDeath);
        }

        private void OnDestroy()
        {
            EventsManager.Broadcast(new GameEndedEvent());
        }

        private void OnPlayerDeath(PlayerDeathEvent evt)
        {
            SceneManager.LoadScene(gameOverSceneName, LoadSceneMode.Single);
        }
    }
}

