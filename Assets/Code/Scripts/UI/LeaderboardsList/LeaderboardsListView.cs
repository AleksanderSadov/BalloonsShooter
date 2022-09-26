using BalloonsShooter.Core;
using BalloonsShooter.Core.ScriptableObjects;
using BalloonsShooter.Gameplay.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BalloonsShooter.UI
{
    public class LeaderboardsListView : MonoBehaviour
    {
        [SerializeField]
        private UIDocument document;
        [SerializeField]
        private string leaderboardId;
        [SerializeField]
        private PlayerNicknameSO currentPlayerNickname;
        [SerializeField]
        private VisualTreeAsset leaderboardsItemTemplate;
        [SerializeField]
        private string positionPrefix;

        private Label submitLeaderboardsHint;
        private Label leaderboardsLoadingHint;
        private ListView leaderboardsListView;
        private List<LeaderboardsItemData> leaderboardsListData;
        private LeaderboardsServiceSO leaderboardsService;

        private void Awake()
        {
            submitLeaderboardsHint = document.rootVisualElement.Q<Label>(UIConstants.LEADERBOARDS_LIST_VIEW_SUBMIT_HINT_NAME);
            leaderboardsLoadingHint = document.rootVisualElement.Q<Label>(UIConstants.LEADERBOARDS_LIST_VIEW_LOADING_HINT_NAME);
            leaderboardsListView = document.rootVisualElement.Q<ListView>(UIConstants.LEADERBOARDS_LIST_VIEW_NAME);
            leaderboardsService = ServiceLocator<LeaderboardsServiceSO>.GetService();
            leaderboardsService.Init();
        }

        public void LoadLeaderboardsList()
        {
            submitLeaderboardsHint.style.display = DisplayStyle.None;
            leaderboardsLoadingHint.style.display = DisplayStyle.Flex;
            leaderboardsListView.style.display = DisplayStyle.None;
            leaderboardsService.GetLeaderboardsList(currentPlayerNickname.nickname, leaderboardId, (list) =>
            {
                leaderboardsLoadingHint.style.display = DisplayStyle.None;
                leaderboardsListView.style.display = DisplayStyle.Flex;
                leaderboardsListData = list;
                DisplayLeaderboardsList();
                StartCoroutine(ScrollToCurrentPlayer(waitSeconds: 0.1f));
            });
        }

        private void DisplayLeaderboardsList()
        {
            leaderboardsListView.makeItem = () =>
            {
                var newListEntry = leaderboardsItemTemplate.Instantiate();
                var newListEntryController = new LeaderboardsItemController(positionPrefix);
                newListEntry.userData = newListEntryController;
                newListEntryController.SetVisualElement(newListEntry);
                return newListEntry;
            };

            leaderboardsListView.bindItem = (item, index) =>
            {
                (item.userData as LeaderboardsItemController).SetItemData(leaderboardsListData[index]);
            };

            leaderboardsListView.itemsSource = leaderboardsListData;
        }

        private IEnumerator<WaitForSeconds> ScrollToCurrentPlayer(float waitSeconds)
        {
            // TODO Find list view ready callback. Now just wait and assume list view is ready
            yield return new WaitForSeconds(waitSeconds);
            var playerIndex = leaderboardsListData.FindIndex(item => item.nickname.Equals(currentPlayerNickname.nickname));
            leaderboardsListView.ScrollToItem(playerIndex);
        }
    }
}

