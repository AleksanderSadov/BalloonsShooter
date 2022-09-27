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
		private int maxHealth = 5;
		[SerializeField]
		private int initialHealth = 3;
		[SerializeField]
		private bool isInvincible = false;

		[Space(20)]
		[SerializeField]
		private int runtimeHealth;

		private int previousHealth;

        private void OnEnable()
        {
			EventsManager.AddListener<GameStartedEvent>(OnGameStarted);
			EventsManager.AddListener<DeathCollisionEvent<Balloon>>(OnBalloonDeathCollision);
			EventsManager.AddListener<EntityClickedEvent<Balloon>>(OnBalloonClicked);
		}

        private void OnDisable()
        {
			EventsManager.RemoveListener<GameStartedEvent>(OnGameStarted);
			EventsManager.RemoveListener<DeathCollisionEvent<Balloon>>(OnBalloonDeathCollision);
			EventsManager.RemoveListener<EntityClickedEvent<Balloon>>(OnBalloonClicked);
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
			maxHealth = Mathf.Clamp(maxHealth, 1, int.MaxValue);
			initialHealth = Mathf.Clamp(initialHealth, 1, maxHealth);
			runtimeHealth = Mathf.Clamp(runtimeHealth, 0, maxHealth);
		}

		private void OnGameStarted(GameStartedEvent evt)
		{
			runtimeHealth = initialHealth;
			previousHealth = initialHealth;
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
			if (damage > 0 && isInvincible) return;

			UpdateHealth(damage);

			if (previousHealth > 0 && runtimeHealth <= 0)
			{
				EventsManager.Broadcast(new PlayerDeathEvent());
			}
		}

		private void UpdateHealth(int damage)
        {
			previousHealth = runtimeHealth;
			int newHealth = runtimeHealth - damage;
			runtimeHealth = Mathf.Clamp(newHealth, 0, maxHealth);
		}
	}
}
