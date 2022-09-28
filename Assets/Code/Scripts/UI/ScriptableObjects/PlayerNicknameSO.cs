using UnityEngine;

namespace BalloonsShooter.UI
{
	[CreateAssetMenu(fileName = "PlayerNickname", menuName = "ScriptableObjects/PlayerNickname")]
	public class PlayerNicknameSO : ScriptableObject
	{
        [SerializeField]
        private string nickname;

        public string Nickname { get => nickname; set => nickname = value; }
    }
}
