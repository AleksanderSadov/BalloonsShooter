using BalloonsShooter.Gameplay.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace BalloonsShooter.UI
{
    public class CurrentScoreValue : MonoBehaviour
    {
        [SerializeField]
        private UIDocument document;
        [SerializeField]
        private GameScoreSO gameScore;

        private Label currentScoreValue;

        private void Awake()
        {
            currentScoreValue = document.rootVisualElement.Q<Label>(UIConstants.CURRENT_SCORE_VALUE_NAME);
        }

        private void Update()
        {
            currentScoreValue.text = gameScore.GetCurrentScore().ToString();
        }
    }
}

