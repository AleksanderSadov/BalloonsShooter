using BalloonsShooter.Core;
using BalloonsShooter.Core.ScriptableObjects;
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
        private VisualTreeAsset leaderboardsItemTemplate;
        [SerializeField]
        private string positionPrefix;
        [SerializeField]
        private int listItemHeight = 45;

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
            leaderboardsService.GetLeaderboardsList((list) =>
            {
                leaderboardsLoadingHint.style.display = DisplayStyle.None;
                leaderboardsListData = list;
                DisplayLeaderboardsList();
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

            leaderboardsListView.fixedItemHeight = listItemHeight;

            leaderboardsListView.itemsSource = leaderboardsListData;
        }
    }
}

