using UnityEngine;

namespace BalloonsShooter.UI
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
