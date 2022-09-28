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
		[SerializeField]
		private float runtimeRequiredBalloonsCount = 0;

        public int MaxBalloonsCount { get => maxBalloonsCount; private set => maxBalloonsCount = value; }
        public int InitialBalloonsCount { get => initialBalloonsCount; private set => initialBalloonsCount = value; }
        public float IncreaseBalloonsCountPerSecond { get => increaseBalloonsCountPerSecond; private set => increaseBalloonsCountPerSecond = value; }
        public float RuntimeRequiredBalloonsCount { get => runtimeRequiredBalloonsCount; set => runtimeRequiredBalloonsCount = value; }

        private void OnValidate()
		{
			MaxBalloonsCount = Mathf.Clamp(MaxBalloonsCount, 1, int.MaxValue);
			InitialBalloonsCount = Mathf.Clamp(InitialBalloonsCount, 1, MaxBalloonsCount);
			RuntimeRequiredBalloonsCount = Mathf.Clamp(InitialBalloonsCount, 0, MaxBalloonsCount);
		}
	}
}
