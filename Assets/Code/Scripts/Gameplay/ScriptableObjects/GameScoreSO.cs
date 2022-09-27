using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.Archetypes;
using BalloonsShooter.Gameplay.Events;
using UnityEngine;

namespace BalloonsShooter.Gameplay.ScriptableObjects
{
	[CreateAssetMenu(fileName = "GameScore", menuName = "ScriptableObjects/GameScore", order = 1)]
	public class GameScoreSO : ScriptableObject
	{
        [SerializeField]
		private int initialScore;

        [Space(20)]
		[SerializeField]
		private int runtimeScore;

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

        public int GetCurrentScore()
        {
			return runtimeScore;
        }

		private void OnGameStarted(GameStartedEvent evt)
        {
			runtimeScore = initialScore;
        }

		private void OnBalloonDeathCollision(DeathCollisionEvent<Balloon> evt)
		{
			runtimeScore += evt.entity.type.ScoreOnFloatAway;
		}

		private void OnBalloonClicked(EntityClickedEvent<Balloon> evt)
		{
			runtimeScore += evt.entity.type.ScoreOnClick;
		}
	}
}
