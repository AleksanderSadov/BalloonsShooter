using System.Collections.Generic;
using UnityEngine;

namespace BalloonsShooter.Gameplay.ScriptableObjects
{
	[CreateAssetMenu(fileName = "BalloonsSpawnChances", menuName = "ScriptableObjects/BalloonsSpawnChances")]
	public class BalloonsSpawnChancesSO : ScriptableObject
	{
		[SerializeField]
		private List<BalloonSpawnChance> balloonTypes;

        [System.Serializable]
		private class BalloonSpawnChance
        {
			public BalloonTypeSO type;
			public int weightedSpawnChance;
		}

		public BalloonTypeSO GetRandomBalloonType()
        {
			float totalWeight = 0;
			foreach (BalloonSpawnChance balloon in balloonTypes)
            {
				totalWeight += balloon.weightedSpawnChance;
            }

			float randomWeight = Random.Range(0, totalWeight);
			foreach (BalloonSpawnChance balloon in balloonTypes)
			{
				if (randomWeight < balloon.weightedSpawnChance)
                {
					return balloon.type;
                }

				randomWeight -= balloon.weightedSpawnChance;
			}

			return null;
		}
	}
}
