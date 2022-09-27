using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.ScriptableObjects;
using BalloonsShooter.Leaderboards;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace BalloonsShooter.UI
{
    public class SubmitScoreButton : MonoBehaviour
    {
        [SerializeField]
        private UIDocument document;
        [SerializeField]
        private string leaderboardId;
        [SerializeField]
        private UnityEvent OnSubmit;

        private Button submitScoreButton;
        private LeaderboardsServiceSO leaderboardsService;
        private PlayerNicknameSO playerNickname;
        private GameScoreSO gameScore;
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
            submitScoreButton = document.rootVisualElement.Q<Button>(UIConstants.SUBMIT_SCORE_BUTTON_NAME);
        }

        private void Start()
        {
            playerNickname = ServiceLocator<PlayerNicknameSO>.GetService();
            gameScore = ServiceLocator<GameScoreSO>.GetService();
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
            leaderboardsService.SubmitScore(playerNickname.nickname, gameScore.GetCurrentScore(), leaderboardId, (success) => 
            {
                OnSubmit?.Invoke();
            });
        }
    }
}

