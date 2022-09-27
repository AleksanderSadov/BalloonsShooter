using BalloonsShooter.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace BalloonsShooter.UI
{
    public class RestartGameButton : MonoBehaviour
    {
        [SerializeField]
        private UIDocument document;

        private Button restartGameButton;

        private void Awake()
        {
            restartGameButton = document.rootVisualElement.Q<Button>(UIConstants.RESTART_GAME_BUTTON_NAME);
        }

        private void OnEnable()
        {
            restartGameButton.RegisterCallback<ClickEvent>(OnRestartButtonPressed, TrickleDown.TrickleDown);
        }

        private void OnDisable()
        {
            restartGameButton.UnregisterCallback<ClickEvent>(OnRestartButtonPressed, TrickleDown.TrickleDown);
        }

        private void OnRestartButtonPressed(ClickEvent evt)
        {
            SceneManager.LoadScene(CoreConstants.GAME_SCENE_NAME, LoadSceneMode.Single);
        }
    }
}

