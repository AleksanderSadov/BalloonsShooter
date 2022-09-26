using BalloonsShooter.Core.ScriptableObjects;
using UnityEngine;

namespace BalloonsShooter.Core
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

