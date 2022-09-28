using UnityEngine;

namespace BalloonsShooter.Gameplay.ScriptableObjects
{
	[CreateAssetMenu(fileName = "GameScore", menuName = "ScriptableObjects/GameScore")]
	public class GameScoreSO : ScriptableObject
	{
        [SerializeField]
		private int initialScore;

        [Space(20)]
		[SerializeField]
		private int runtimeScore;

        public int InitialScore { get => initialScore; private set => initialScore = value; }
        public int RuntimeScore { get => runtimeScore; set => runtimeScore = value; }
    }
}
