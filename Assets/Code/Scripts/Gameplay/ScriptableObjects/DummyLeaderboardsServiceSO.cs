using BalloonsShooter.Gameplay.Helpers;
using System.Collections;
using UnityEngine;

namespace BalloonsShooter.Gameplay.ScriptableObjects
{
	[CreateAssetMenu(fileName = "DummyLeaderboardsServiceSO", menuName = "ScriptableObjects/DummyLeaderboardsServiceSO")]
	public class DummyLeaderboardsServiceSO : LeaderboardsServiceSO
    {
        public float simulateCallWaitSeconds = 1;

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

        private IEnumerator SimulateSubmitScore(float waitSeconds, System.Action<bool> callback)
        {
            yield return new WaitForSeconds(waitSeconds);
            IsSubmitInProgress = false;
            callback(true);
        }
    }
}
