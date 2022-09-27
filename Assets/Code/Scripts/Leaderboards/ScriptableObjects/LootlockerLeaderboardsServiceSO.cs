using LootLocker.Requests;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonsShooter.Leaderboards
{
	[CreateAssetMenu(fileName = "LootlockerLeaderboardsService", menuName = "ScriptableObjects/LootlockerLeaderboardsService")]
	public class LootlockerLeaderboardsServiceSO : LeaderboardsServiceSO
    {
        [SerializeField]
        private int leaderboardsListLimit;

        private bool isSessionCallInProgress = false;

        private void OnDisable()
        {
            IsSessionStarted = false;
        }

        private void OnValidate()
        {
            leaderboardsListLimit = Mathf.Clamp(leaderboardsListLimit, 0, int.MaxValue);
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

        public override void GetLeaderboardsList(string currentPlayerNickname, string leaderboardId, Action<List<LeaderboardsItemData>> callback)
        {
            GetSurroundingScore(currentPlayerNickname, leaderboardId, callback);
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

        private void GetSurroundingScore(string nickname, string leaderboardId, Action<List<LeaderboardsItemData>> callback)
        {
            var leaderboardsList = new List<LeaderboardsItemData>();

            LootLockerSDKManager.GetMemberRank(leaderboardId, nickname, (responseMemberRank) =>
            {
                if (responseMemberRank.success)
                {
                    int memberRank = responseMemberRank.rank;
                    int totalCount = leaderboardsListLimit;
                    int after = Mathf.Clamp((int) Math.Floor(memberRank - leaderboardsListLimit / 2f), 0, int.MaxValue);

                    LootLockerSDKManager.GetScoreList(leaderboardId, totalCount, after, (responseScoreList) =>
                    {
                        if (responseScoreList.success)
                        {
                            foreach (var responseItem in responseScoreList.items)
                            {
                                LeaderboardsItemData listItem = new()
                                {
                                    position = responseItem.rank,
                                    nickname = responseItem.member_id,
                                    score = responseItem.score
                                };
                                leaderboardsList.Add(listItem);
                            }
                        }

                        callback(leaderboardsList);
                    });
                }
                else
                {
                    callback(leaderboardsList);
                }
            });
        }
    }
}
