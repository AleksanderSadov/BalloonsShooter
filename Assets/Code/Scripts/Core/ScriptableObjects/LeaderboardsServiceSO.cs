using System.Collections.Generic;
using UnityEngine;

namespace BalloonsShooter.Core.ScriptableObjects
{
	public abstract class LeaderboardsServiceSO : ScriptableObject
	{
        public bool IsSessionStarted { get; protected set; }
        public bool IsSubmitInProgress { get; protected set; }
        public bool IsGetLeaderboardsListInProgress { get; protected set; }

        public abstract void Init();
        public abstract void SubmitScore(string nickname, int userScore, string leaderboardId, System.Action<bool> callback);
        public abstract void GetLeaderboardsList(string currentPlayerNickname, string leaderboardId, System.Action<List<LeaderboardsItemData>> callback);
    }
}
