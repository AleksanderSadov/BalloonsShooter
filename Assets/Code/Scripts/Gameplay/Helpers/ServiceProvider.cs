using BalloonsShooter.Gameplay.ScriptableObjects;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Helpers
{
    public class ServiceProvider : MonoBehaviour
    {
        [SerializeField]
        private LeaderboardsServiceSO leaderboardsService;

        private void Awake()
        {
            ServiceLocator<LeaderboardsServiceSO>.ProvideService(leaderboardsService);
        }
    }
}

