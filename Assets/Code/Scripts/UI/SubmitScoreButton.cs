using BalloonsShooter.Gameplay.Helpers;
using BalloonsShooter.Gameplay.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace BalloonsShooter.UI
{
    public class SubmitScoreButton : MonoBehaviour
    {
        [SerializeField]
        private UIDocument document;
        [SerializeField]
        private string submitScoreButtonName;
        [SerializeField]
        private string leaderboardId;
        [SerializeField]
        private PlayerNicknameSO playerNickname;
        [SerializeField]
        private GameScoreSO gameScore;

        private Button submitScoreButton;
        private LeaderboardsServiceSO leaderboardsService;
        private bool ShouldEnableButton 
        {
            get
            {
                return enabled
                    && leaderboardsService.IsSessionStarted
                    && !leaderboardsService.IsSubmitInProgress
                    && !string.IsNullOrEmpty(playerNickname.nickname);
            }
        }

        private void Awake()
        {
            submitScoreButton = document.rootVisualElement.Q<Button>(submitScoreButtonName);
            leaderboardsService = ServiceLocator<LeaderboardsServiceSO>.GetService();
            leaderboardsService.Init();
        }

        private void OnEnable()
        {
            submitScoreButton.RegisterCallback<ClickEvent>(OnSubmitButtonPressed, TrickleDown.TrickleDown);
        }

        private void Update()
        {
            submitScoreButton.SetEnabled(ShouldEnableButton);
        }

        private void OnDisable()
        {
            submitScoreButton.UnregisterCallback<ClickEvent>(OnSubmitButtonPressed, TrickleDown.TrickleDown);
        }

        private void OnSubmitButtonPressed(ClickEvent evt)
        {
            leaderboardsService.SubmitScore(playerNickname.nickname, gameScore.GetCurrentScore(), leaderboardId, (success) => { });
        }
    }
}

