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
		private int MaxBalloonsCount = 1;
		[SerializeField]
		private int initialBalloonsCount = 1;
		[SerializeField]
		private int increaseBalloonsCountEverySeconds = 10;

		[Space(20)]
		[SerializeField]
		private int runtimeRequiredBalloonsCount = 0;

		private float lastTimeDifficultyIncreased;
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
			MaxBalloonsCount = Mathf.Clamp(MaxBalloonsCount, 1, int.MaxValue);
			initialBalloonsCount = Mathf.Clamp(initialBalloonsCount, 1, MaxBalloonsCount);
			runtimeRequiredBalloonsCount = Mathf.Clamp(initialBalloonsCount, 0, MaxBalloonsCount);
		}

		public int GetCurrentRequiredBalloonsCount()
		{
			return runtimeRequiredBalloonsCount;
		}

		private void StartIncreaseDifficultyTimer()
		{
			lastTimeDifficultyIncreased = Time.time;
			shouldRunDifficultyTimer = true;
			MonoInstance.Instance.StartCoroutine(DifficultyTimer());
		}

		private IEnumerator DifficultyTimer()
        {
			while (shouldRunDifficultyTimer)
            {
				if (Time.time - lastTimeDifficultyIncreased >= increaseBalloonsCountEverySeconds)
				{
					runtimeRequiredBalloonsCount++;
					lastTimeDifficultyIncreased = Time.time;
				}

				yield return new WaitForSeconds(0.1f);
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
