using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace BalloonsShooter.UI
{
    public class CurrentScoreValue : MonoBehaviour
    {
        [SerializeField]
        private UIDocument document;

        private Label currentScoreValue;
        private GameScoreSO gameScore;

        private void Awake()
        {
            currentScoreValue = document.rootVisualElement.Q<Label>(UIConstants.CURRENT_SCORE_VALUE_NAME);
        }

        private void Start()
        {
            gameScore = ServiceLocator<GameScoreSO>.GetService();
        }

        private void Update()
        {
            currentScoreValue.text = gameScore.RuntimeScore.ToString();
        }
    }
}

