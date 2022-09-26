using LootLocker.Requests;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonsShooter.Core.ScriptableObjects
{
	[CreateAssetMenu(fileName = "LootlockerLeaderboardsService", menuName = "ScriptableObjects/LootlockerLeaderboardsService")]
	public class LootlockerLeaderboardsServiceSO : LeaderboardsServiceSO
    {
        private bool isSessionCallInProgress = false;

        private void OnDisable()
        {
            IsSessionStarted = false;
        }

        public override void Init()
        {
            if (!IsSessionStarted && !isSessionCallInProgress)
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

        public override void GetLeaderboardsList(Action<List<LeaderboardsItemData>> callback)
        {
            throw new NotImplementedException();
        }

        private void StartGuestSession()
        {
            isSessionCallInProgress = true;
            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (response.success)
                {
                    IsSessionStarted = true;
                }

                isSessionCallInProgress = false;
            });
        }
    }
}
