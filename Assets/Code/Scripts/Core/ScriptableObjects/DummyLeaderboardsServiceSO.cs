using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonsShooter.Core.ScriptableObjects
{
	[CreateAssetMenu(fileName = "DummyLeaderboardsServiceSO", menuName = "ScriptableObjects/DummyLeaderboardsServiceSO")]
	public class DummyLeaderboardsServiceSO : LeaderboardsServiceSO
    {
        public float simulateCallWaitSeconds = 1;
        public List<LeaderboardsItemData> leaderboardsList = new List<LeaderboardsItemData>();

        private void OnDisable()
        {
            IsSessionStarted = false;
        }

        public override void Init()
        {
            IsSessionStarted = true;
        }

        public override void SubmitScore(string nickname, int userScore, string leaderboardId, System.Action<bool> callback)
        {
            IsSubmitInProgress = true;
            MonoInstance.Instance.StartCoroutine(SimulateSubmitScore(simulateCallWaitSeconds, callback));
        }

        public override void GetLeaderboardsList(System.Action<List<LeaderboardsItemData>> callback)
        {
            MonoInstance.Instance.StartCoroutine(SimulateGetLeaderboardsList(simulateCallWaitSeconds, callback));
        }

        private IEnumerator SimulateSubmitScore(float waitSeconds, System.Action<bool> callback)
        {
            yield return new WaitForSeconds(waitSeconds);
            IsSubmitInProgress = false;
            callback(true);
        }

        private IEnumerator SimulateGetLeaderboardsList(float waitSeconds, System.Action<List<LeaderboardsItemData>> callback)
        {
            yield return new WaitForSeconds(waitSeconds);
            callback(leaderboardsList);
        }
    }
}
