using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.Events;
using System.Collections;
using UnityEngine;

namespace BalloonsShooter.Gameplay.ScriptableObjects
{
	[CreateAssetMenu(fileName = "BalloonsCount", menuName = "ScriptableObjects/BalloonsCount")]
	public class BalloonsCountSO : ScriptableObject
	{
		[SerializeField]
		private int maxBalloonsCount = 1;
		[SerializeField]
		private int initialBalloonsCount = 1;
		[SerializeField]
		private float increaseBalloonsCountPerSecond = 0.1f;

		[Space(20)]
		[System.NonSerialized]
		private float runtimeRequiredBalloonsCount = 0;

		private bool shouldRunDifficultyTimer = false;

		private void OnEnable()
		{
			EventsManager.AddListener<GameStartedEvent>(OnGameStarted);
			EventsManager.AddListener<GameEndedEvent>(OnGameEnded);
		}

		private void OnDisable()
		{
			EventsManager.RemoveListener<GameStartedEvent>(OnGameStarted);
			EventsManager.RemoveListener<GameEndedEvent>(OnGameEnded);
			StopDifficultyTimer();
		}

		private void OnValidate()
		{
			maxBalloonsCount = Mathf.Clamp(maxBalloonsCount, 1, int.MaxValue);
			initialBalloonsCount = Mathf.Clamp(initialBalloonsCount, 1, maxBalloonsCount);
			runtimeRequiredBalloonsCount = Mathf.Clamp(initialBalloonsCount, 0, maxBalloonsCount);
		}

		public int GetCurrentRequiredBalloonsCount()
		{
			return (int) Mathf.Floor(runtimeRequiredBalloonsCount);
		}

		private void StartIncreaseDifficultyTimer()
		{
			shouldRunDifficultyTimer = true;
			MonoInstance.Instance.StartCoroutine(DifficultyTimer());
		}

		private IEnumerator DifficultyTimer()
        {
			while (shouldRunDifficultyTimer)
            {
				var increaseAmount = increaseBalloonsCountPerSecond * Time.deltaTime;
				var newBalloonsCount = runtimeRequiredBalloonsCount + increaseAmount;
				runtimeRequiredBalloonsCount = Mathf.Clamp(newBalloonsCount, 1, maxBalloonsCount);

				if (newBalloonsCount >= maxBalloonsCount)
                {
					shouldRunDifficultyTimer = false;
                }

				yield return null;
			}
        }

		private void OnGameStarted(GameStartedEvent evt)
        {
			runtimeRequiredBalloonsCount = initialBalloonsCount;
			StartIncreaseDifficultyTimer();
		}

		private void OnGameEnded(GameEndedEvent evt)
		{
			StopDifficultyTimer();
		}

		private void StopDifficultyTimer()
        {
			shouldRunDifficultyTimer = false;
		}
	}
}
