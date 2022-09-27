using BalloonsShooter.Gameplay.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace BalloonsShooter.UI
{
    public class HealthHud : MonoBehaviour
    {
        [SerializeField]
        private UIDocument document;
        [SerializeField]
        private VisualTreeAsset healthItemTemplate;
        [SerializeField]
        private PlayerHealthSO playerHealthSO;

        private VisualElement healthContainer;

        private int lastDisplayedHealth = int.MinValue;

        private void Awake()
        {
            healthContainer = document.rootVisualElement.Q<VisualElement>(UIConstants.GAME_HUD_HEALTH_CONTAINER_NAME);
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
            foreach (var healthItem in healthContainer.Children())
            {
                if (i <= currentHealth - 1)
                {
                    healthItem.style.display = DisplayStyle.Flex;
                }
                else
                {
                    healthItem.style.display = DisplayStyle.None;
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

