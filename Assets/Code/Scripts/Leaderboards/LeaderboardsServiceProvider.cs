using BalloonsShooter.Core;
using UnityEngine;

namespace BalloonsShooter.Leaderboards
{
    public class LeaderboardsServiceProvider : ServiceProvider
    {
        [SerializeField]
        private LeaderboardsServiceSO leaderboardsService;

        private void Awake()
        {
            ServiceLocator<LeaderboardsServiceSO>.ProvideService(leaderboardsService);
        }
    }
}

