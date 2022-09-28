using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.ScriptableObjects;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace BalloonsShooter.UI
{
    public class HealthHUD : MonoBehaviour
    {
        [SerializeField]
        private UIDocument document;
        [SerializeField]
        private VisualTreeAsset healthItemTemplate;

        private VisualElement healthContainer;
        private PlayerHealthSO playerHealthSO;

        private int lastDisplayedHealth = int.MinValue;

        private void Awake()
        {
            healthContainer = document.rootVisualElement.Q<VisualElement>(UIConstants.GAME_HUD_HEALTH_CONTAINER_NAME);
        }

        private void Start()
        {
            playerHealthSO = ServiceLocator<PlayerHealthSO>.GetService();
        }

        private void Update()
        {
            CheckMaxHealth(playerHealthSO.GetMaxHealth());
            DisplayCurrentHealth(playerHealthSO.GetCurrentHealth());
        }

        private void DisplayCurrentHealth(int currentHealth)
        {
            if (lastDisplayedHealth == currentHealth) return;

            int i = 0;
            foreach (var healthItemTemplate in healthContainer.Children())
            {
                var healthItem = healthItemTemplate.Children().First();
                if (i <= currentHealth - 1)
                {
                    healthItem.RemoveFromClassList(UIConstants.HEALTH_HUD_INACTIVE_CLASS);
                }
                else
                {
                    healthItem.AddToClassList(UIConstants.HEALTH_HUD_INACTIVE_CLASS);
                }

                i++;
            }

            lastDisplayedHealth = currentHealth;
        }

        private void CheckMaxHealth(int maxHealth)
        {
            while (healthContainer.childCount != maxHealth)
            {
                if (healthContainer.childCount > maxHealth)
                {
                    healthContainer.RemoveAt(healthContainer.childCount - 1);
                }
                else
                {
                    healthContainer.Add(healthItemTemplate.Instantiate());
                }
            }
        }
    }
}

