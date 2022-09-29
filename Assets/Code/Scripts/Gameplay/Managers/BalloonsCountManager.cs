using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.Events;
using BalloonsShooter.Gameplay.ScriptableObjects;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Manager
{
    public class BalloonsCountManager : MonoBehaviour
    {
		private BalloonsCountSO balloonsCountSO;
		private bool shouldRunDifficultyTimer = false;

		private void OnEnable()
		{
			EventsManager.AddListener<GameStartedEvent>(OnGameStarted);
			EventsManager.AddListener<GameEndedEvent>(OnGameEnded);
		}

        private void Start()
        {
			balloonsCountSO = ServiceLocator<BalloonsCountSO>.GetService();
        }

        private void Update()
        {
			if (!shouldRunDifficultyTimer) return;

			var increaseAmount = balloonsCountSO.IncreaseBalloonsCountPerSecond * Time.deltaTime;
			var newBalloonsCount = balloonsCountSO.RuntimeRequiredBalloonsCount + increaseAmount;
			balloonsCountSO.RuntimeRequiredBalloonsCount = Mathf.Clamp(newBalloonsCount, 1, balloonsCountSO.MaxBalloonsCount);

			if (newBalloonsCount >= balloonsCountSO.MaxBalloonsCount)
			{
				shouldRunDifficultyTimer = false;
			}
		}

        private void OnDisable()
		{
			EventsManager.RemoveListener<GameStartedEvent>(OnGameStarted);
			EventsManager.RemoveListener<GameEndedEvent>(OnGameEnded);
		}

		private void OnGameStarted(GameStartedEvent evt)
		{
			balloonsCountSO.RuntimeRequiredBalloonsCount = balloonsCountSO.InitialBalloonsCount;
			shouldRunDifficultyTimer = true;
		}

		private void OnGameEnded(GameEndedEvent evt)
		{
			shouldRunDifficultyTimer = false;
			balloonsCountSO.RuntimeRequiredBalloonsCount = balloonsCountSO.InitialBalloonsCount;
		}
	}
}

