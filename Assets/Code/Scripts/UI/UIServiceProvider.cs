using BalloonsShooter.Core;
using UnityEngine;

namespace BalloonsShooter.UI
{
    public class UIServiceProvider : ServiceProvider
    {
        [SerializeField]
        private PlayerNicknameSO playerNickname;

        private void Awake()
        {
            ServiceLocator<PlayerNicknameSO>.ProvideService(playerNickname);
        }
    }
}

