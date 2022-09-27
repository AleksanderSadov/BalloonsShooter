using BalloonsShooter.Leaderboards;
using UnityEngine.UIElements;

namespace BalloonsShooter.UI
{
    public class LeaderboardsItemController
    {
        public string positionPrefix;

        private Label position;
        private Label nickname;
        private Label score;

        public LeaderboardsItemController(string positionPrefix)
        {
            this.positionPrefix = positionPrefix;
        }

        public void SetVisualElement(VisualElement visualElement)
        {
            position = visualElement.Q<Label>(UIConstants.LEADERBOARDS_LIST_ITEM_POSITION_NAME);
            nickname = visualElement.Q<Label>(UIConstants.LEADERBOARDS_LIST_ITEM_NICKNAME_NAME);
            score = visualElement.Q<Label>(UIConstants.LEADERBOARDS_LIST_ITEM_SCORE_NAME);
        }

        public void SetItemData(LeaderboardsItemData itemData)
        {
            position.text = positionPrefix + itemData.position.ToString();
            nickname.text = itemData.nickname;
            score.text = itemData.score.ToString();
        }
    }
}

