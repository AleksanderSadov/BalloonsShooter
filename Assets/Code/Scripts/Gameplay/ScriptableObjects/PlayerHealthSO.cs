using UnityEngine;

namespace BalloonsShooter.Gameplay.ScriptableObjects
{
	[CreateAssetMenu(fileName = "PlayerHealth", menuName = "ScriptableObjects/PlayerHealth", order = 2)]
	public class PlayerHealthSO : ScriptableObject
	{
		[SerializeField]
		private int maxHealth = 5;
		[SerializeField]
		private int initialHealth = 3;
		[SerializeField]
		private bool isInvincible = false;

		[Space(20)]
		[SerializeField]
		private int runtimeHealth;

        public int MaxHealth { get => maxHealth; private set => maxHealth = value; }
        public int InitialHealth { get => initialHealth; private set => initialHealth = value; }
        public bool IsInvincible { get => isInvincible; private set => isInvincible = value; }
        public int RuntimeHealth { get => runtimeHealth; set => runtimeHealth = value; }

        private void OnValidate()
		{
			MaxHealth = Mathf.Clamp(MaxHealth, 1, int.MaxValue);
			InitialHealth = Mathf.Clamp(InitialHealth, 1, MaxHealth);
			RuntimeHealth = Mathf.Clamp(RuntimeHealth, 0, MaxHealth);
		}
	}
}
