using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.Archetypes;
using BalloonsShooter.Gameplay.Events;
using UnityEngine;

namespace BalloonsShooter.Gameplay.ScriptableObjects
{
	[CreateAssetMenu(fileName = "PlayerHealth", menuName = "ScriptableObjects/PlayerHealth", order = 2)]
	public class PlayerHealthSO : ScriptableObject
	{
        [SerializeField]
		private int initialHealth = 3;
		[SerializeField]
		private int maxHealth = 5;
		[SerializeField]
		private int balloonFloatedAwayDecrementHealth = -1;

		[Space(20)]
		[SerializeField]
		private int runtimeHealth;

		private int previousHealth;

        private void OnEnable()
        {
			EventsManager.AddListener<GameStartedEvent>(OnGameStarted);
			EventsManager.AddListener<DeathCollisionEvent<Balloon>>(OnBalloonDeathCollision);
		}

        private void OnDisable()
        {
			EventsManager.RemoveListener<GameStartedEvent>(OnGameStarted);
			EventsManager.RemoveListener<DeathCollisionEvent<Balloon>>(OnBalloonDeathCollision);
		}

        public int GetCurrentHealth()
        {
			return runtimeHealth;
        }

		public int GetMaxHealth()
		{
			return maxHealth;
		}

		private void OnValidate()
		{
			initialHealth = Mathf.Clamp(initialHealth, 1, int.MaxValue);
			maxHealth = Mathf.Clamp(maxHealth, initialHealth, int.MaxValue);
			runtimeHealth = Mathf.Clamp(runtimeHealth, 0, maxHealth);
			balloonFloatedAwayDecrementHealth = Mathf.Clamp(balloonFloatedAwayDecrementHealth, int.MinValue, 0);
		}

		private void OnGameStarted(GameStartedEvent evt)
		{
			runtimeHealth = initialHealth;
			previousHealth = initialHealth;
		}

		private void OnBalloonDeathCollision(DeathCollisionEvent<Balloon> evt)
		{
			UpdateHealth(balloonFloatedAwayDecrementHealth);
			CheckHealthAndFirePlayerDeath();
		}

		private void UpdateHealth(int valueChange)
        {
			previousHealth = runtimeHealth;
			int newHealth = runtimeHealth + valueChange;
			runtimeHealth = Mathf.Clamp(newHealth, 0, maxHealth);
		}

		private void CheckHealthAndFirePlayerDeath()
        {
			if (previousHealth > 0 && runtimeHealth <= 0)
            {
				EventsManager.Broadcast(new PlayerDeathEvent());
			}
        }
	}
}
