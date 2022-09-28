using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BalloonsShooter.Gameplay.Manager
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
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
            EventsManager.Broadcast(new GameEndedEvent());
            SceneManager.LoadSceneAsync(GameConstants.GAME_OVER_ADDITIVE_SCENE_NAME, LoadSceneMode.Additive);
        }
    }
}

