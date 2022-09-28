using UnityEngine;

namespace BalloonsShooter.Gameplay.ScriptableObjects
{
	[CreateAssetMenu(fileName = "BalloonType", menuName = "ScriptableObjects/BalloonType")]
	public class BalloonTypeSO : ScriptableObject
	{
		[SerializeField]
		private Material material;
		[SerializeField]
		private float floatSpeed;
		[SerializeField]
		private int damageOnClick;
		[SerializeField]
		private int damageOnFloatAway;
		[SerializeField]
		private int scoreOnClick;
		[SerializeField]
		private int scoreOnFloatAway;

		public Material Material { get => material; private set => material = value; }
		public float FloatSpeed { get => floatSpeed; private set => floatSpeed = value; }
		public int DamageOnClick { get => damageOnClick; private set => damageOnClick = value; }
		public int DamageOnFloatAway { get => damageOnFloatAway; private set => damageOnFloatAway = value; }
		public int ScoreOnClick { get => scoreOnClick; private set => scoreOnClick = value; }
		public int ScoreOnFloatAway { get => scoreOnFloatAway; private set => scoreOnFloatAway = value; }
	}
}
