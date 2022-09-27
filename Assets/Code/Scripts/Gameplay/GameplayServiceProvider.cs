using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.ScriptableObjects;
using UnityEngine;

namespace BalloonsShooter.Gameplay
{
    public class GameplayServiceProvider : ServiceProvider
    {
        [SerializeField]
        private BalloonsCountSO balloonsCount;
        [SerializeField]
        private GameScoreSO gameScore;
        [SerializeField]
        private PlayerHealthSO playerHealth;

        private void Awake()
        {
            ServiceLocator<BalloonsCountSO>.ProvideService(balloonsCount);
            ServiceLocator<GameScoreSO>.ProvideService(gameScore);
            ServiceLocator<PlayerHealthSO>.ProvideService(playerHealth);
        }
    }
}

