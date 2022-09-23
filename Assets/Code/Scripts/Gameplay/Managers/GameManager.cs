using BalloonsShooter.Gameplay.Events;
using BalloonsShooter.Gameplay.Managers;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Manager
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            EventsManager.Broadcast(new GameStartedEvent());
        }

        private void OnDestroy()
        {
            EventsManager.Broadcast(new GameEndedEvent());
        }
    }
}

