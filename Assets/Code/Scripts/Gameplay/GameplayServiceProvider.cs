using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.Models;
using BalloonsShooter.Gameplay.ScriptableObjects;
using UnityEngine;

namespace BalloonsShooter.Gameplay
{
    public class GameplayServiceProvider : ServiceProvider
    {
        [SerializeField]
        private BalloonsCountSO balloonsCount;
        [SerializeField]
        private BalloonsSpawnChancesSO balloonsSpawnChances;
        [SerializeField]
        private GameScoreSO gameScore;
        [SerializeField]
        private PlayerHealthSO playerHealth;

        private readonly BalloonsModel balloonsModel = new();

        private void Awake()
        {
            ServiceLocator<BalloonsCountSO>.ProvideService(balloonsCount);
            ServiceLocator<BalloonsSpawnChancesSO>.ProvideService(balloonsSpawnChances);
            ServiceLocator<GameScoreSO>.ProvideService(gameScore);
            ServiceLocator<PlayerHealthSO>.ProvideService(playerHealth);
            ServiceLocator<BalloonsModel>.ProvideService(balloonsModel);
        }
    }
}

