using UnityEngine;

namespace BalloonsShooter.Gameplay.ScriptableObjects
{
	public abstract class LeaderboardsServiceSO : ScriptableObject
	{
        public bool IsSessionStarted { get; protected set; }
        public bool IsSubmitInProgress { get; protected set; }

        public abstract void Init();
        public abstract void SubmitScore(string nickname, int userScore, string leaderboardId, System.Action<bool> callback);
    }
}
