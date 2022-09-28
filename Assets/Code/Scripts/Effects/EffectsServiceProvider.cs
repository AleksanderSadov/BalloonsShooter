using BalloonsShooter.Core;
using BalloonsShooter.Effects.ScriptableObjects;
using UnityEngine;

namespace BalloonsShooter.Gameplay
{
    public class EffectsServiceProvider : ServiceProvider
    {
        [SerializeField]
        private SkyboxSO skybox;

        private void Awake()
        {
            ServiceLocator<SkyboxSO>.ProvideService(skybox);
        }
    }
}

