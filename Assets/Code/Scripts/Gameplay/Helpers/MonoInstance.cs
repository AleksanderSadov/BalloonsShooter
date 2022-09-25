using UnityEngine;

namespace BalloonsShooter.Gameplay.Helpers
{
    public class MonoInstance : MonoBehaviour
    {
        public static MonoInstance Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
    }
}

