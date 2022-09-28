using BalloonsShooter.Core;
using BalloonsShooter.Core.Events;
using BalloonsShooter.Gameplay.Archetypes;
using BalloonsShooter.Gameplay.Events;
using BalloonsShooter.Gameplay.ScriptableObjects;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Manager
{
    public class PlayerHealthManager : MonoBehaviour
    {
		private PlayerHealthSO playerHealthSO;
		private int previousHealth;

		private void OnEnable()
		{
			EventsManager.AddListener<GameStartedEvent>(OnGameStarted);
			EventsManager.AddListener<DeathCollisionEvent<Balloon>>(OnBalloonDeathCollision);
			EventsManager.AddListener<EntityClickedEvent<Balloon>>(OnBalloonClicked);
		}

        private void Start()
        {
			playerHealthSO = ServiceLocator<PlayerHealthSO>.GetService();
        }

        private void OnDisable()
		{
			EventsManager.RemoveListener<GameStartedEvent>(OnGameStarted);
			EventsManager.RemoveListener<DeathCollisionEvent<Balloon>>(OnBalloonDeathCollision);
			EventsManager.RemoveListener<EntityClickedEvent<Balloon>>(OnBalloonClicked);
		}

		private void OnGameStarted(GameStartedEvent evt)
		{
			playerHealthSO.RuntimeHealth = playerHealthSO.InitialHealth;
			previousHealth = playerHealthSO.InitialHealth;
		}

		private void OnBalloonDeathCollision(DeathCollisionEvent<Balloon> evt)
		{
			HandleDamage(evt.entity.type.DamageOnFloatAway);
		}

		private void OnBalloonClicked(EntityClickedEvent<Balloon> evt)
		{
			HandleDamage(evt.entity.type.DamageOnClick);
		}

		private void HandleDamage(int damage)
		{
			if (damage > 0 && playerHealthSO.IsInvincible) return;

			UpdateHealth(damage);

			if (previousHealth > 0 && playerHealthSO.RuntimeHealth <= 0)
			{
				EventsManager.Broadcast(new PlayerDeathEvent());
			}
		}

		private void UpdateHealth(int damage)
		{
			previousHealth = playerHealthSO.RuntimeHealth;
			int newHealth = playerHealthSO.RuntimeHealth - damage;
			playerHealthSO.RuntimeHealth = Mathf.Clamp(newHealth, 0, playerHealthSO.MaxHealth);
		}
	}
}

