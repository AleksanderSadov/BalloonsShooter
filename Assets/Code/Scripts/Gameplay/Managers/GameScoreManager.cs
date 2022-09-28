using BalloonsShooter.Core;
using BalloonsShooter.Core.Events;
using BalloonsShooter.Gameplay.Archetypes;
using BalloonsShooter.Gameplay.Events;
using BalloonsShooter.Gameplay.ScriptableObjects;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Manager
{
    public class GameScoreManager : MonoBehaviour
    {
		private GameScoreSO gameScoreSO;

		private void OnEnable()
		{
			EventsManager.AddListener<GameStartedEvent>(OnGameStarted);
			EventsManager.AddListener<DeathCollisionEvent<Balloon>>(OnBalloonDeathCollision);
			EventsManager.AddListener<EntityClickedEvent<Balloon>>(OnBalloonClicked);
		}

        private void Start()
        {
			gameScoreSO = ServiceLocator<GameScoreSO>.GetService();
        }

        private void OnDisable()
		{
			EventsManager.RemoveListener<GameStartedEvent>(OnGameStarted);
			EventsManager.RemoveListener<DeathCollisionEvent<Balloon>>(OnBalloonDeathCollision);
			EventsManager.RemoveListener<EntityClickedEvent<Balloon>>(OnBalloonClicked);
		}

		private void OnGameStarted(GameStartedEvent evt)
		{
			gameScoreSO.RuntimeScore = gameScoreSO.InitialScore;
		}

		private void OnBalloonDeathCollision(DeathCollisionEvent<Balloon> evt)
		{
			gameScoreSO.RuntimeScore += evt.entity.type.ScoreOnFloatAway;
		}

		private void OnBalloonClicked(EntityClickedEvent<Balloon> evt)
		{
			gameScoreSO.RuntimeScore += evt.entity.type.ScoreOnClick;
		}
	}
}

