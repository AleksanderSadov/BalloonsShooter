using UnityEngine;

namespace BalloonsShooter.Gameplay.ScriptableObjects
{
	[CreateAssetMenu(fileName = "PlayerNickname", menuName = "ScriptableObjects/PlayerNickname")]
	public class PlayerNicknameSO : ScriptableObject
	{
        public string nickname;

        private void OnEnable()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }
	}
}
