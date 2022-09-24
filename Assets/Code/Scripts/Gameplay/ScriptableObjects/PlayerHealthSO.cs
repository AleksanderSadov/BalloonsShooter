using BalloonsShooter.Gameplay.Archetypes;
using BalloonsShooter.Gameplay.Events;
using BalloonsShooter.Gameplay.Managers;
using UnityEngine;

namespace BalloonsShooter.Gameplay.ScriptableObjects
{
	[CreateAssetMenu(fileName = "PlayerHealth", menuName = "ScriptableObjects/PlayerHealth", order = 2)]
	public class PlayerHealthSO : ScriptableObject
	{
        [SerializeField]
		private int initialHealth = 3;
		[SerializeField]
		private int balloonFloatedAwayDecrementHealth = -1;

		[Space(20)]
		[SerializeField]
		private int runtimeHealth;

        private void OnEnable()
        {
			EventsManager.AddListener<GameStartedEvent>(OnGameStarted);
			EventsManager.AddListener<GameEndedEvent>(OnGameEnded);
			EventsManager.AddListener<DeathCollisionEvent<Balloon>>(OnBalloonDeathCollision);
		}

        private void OnDisable()
        {
			EventsManager.RemoveListener<GameStartedEvent>(OnGameStarted);
			EventsManager.RemoveListener<GameEndedEvent>(OnGameEnded);
			EventsManager.RemoveListener<DeathCollisionEvent<Balloon>>(OnBalloonDeathCollision);
		}

        public float GetCurrentHealth()
        {
			return runtimeHealth;
        }

		private void OnValidate()
		{
			initialHealth = Mathf.Clamp(initialHealth, 0, int.MaxValue);
			balloonFloatedAwayDecrementHealth = Mathf.Clamp(balloonFloatedAwayDecrementHealth, int.MinValue, 0);
		}

		private void OnGameStarted(GameStartedEvent evt)
		{
			runtimeHealth = initialHealth;
		}

		private void OnGameEnded(GameEndedEvent evt)
		{
			runtimeHealth = initialHealth;
		}

		private void OnBalloonDeathCollision(DeathCollisionEvent<Balloon> evt)
		{
			runtimeHealth += balloonFloatedAwayDecrementHealth;
		}
	}
}
