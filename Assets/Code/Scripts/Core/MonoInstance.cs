using UnityEngine;

namespace BalloonsShooter.Core
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

