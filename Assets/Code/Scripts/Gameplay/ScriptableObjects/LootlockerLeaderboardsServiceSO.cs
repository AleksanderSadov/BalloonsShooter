using LootLocker.Requests;
using UnityEngine;

namespace BalloonsShooter.Gameplay.ScriptableObjects
{
	[CreateAssetMenu(fileName = "LootlockerLeaderboardsService", menuName = "ScriptableObjects/LootlockerLeaderboardsService")]
	public class LootlockerLeaderboardsServiceSO : LeaderboardsServiceSO
    {
        private void OnDisable()
        {
            IsSessionStarted = false;
        }

        public override void Init()
        {
            if (!IsSessionStarted)
            {
                StartGuestSession();
            }
        }

        public override void SubmitScore(string nickname, int userScore, string leaderboardId, System.Action<bool> callback)
        {
            IsSubmitInProgress = true;
            LootLockerSDKManager.SubmitScore(nickname, userScore, leaderboardId, (response) =>
            {
                IsSubmitInProgress = false;
                callback(response.success);
            });
        }

        private void StartGuestSession()
        {
            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (response.success)
                {
                    IsSessionStarted = true;
                }
            });
        }
    }
}
