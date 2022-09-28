using BalloonsShooter.Core;
using BalloonsShooter.Gameplay;
using BalloonsShooter.Gameplay.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace BalloonsShooter.UI
{
    public class RestartGameButton : MonoBehaviour
    {
        [SerializeField]
        private UIDocument document;
        [SerializeField]
        private LoadSceneMode unloadSceneMode;

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
            var gameStartScene = SceneManager.GetSceneByName(GameConstants.GAME_SCENE_NAME);
            if (gameStartScene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(GameConstants.GAME_OVER_ADDITIVE_SCENE_NAME).completed += (operation) =>
                {
                    EventsManager.Broadcast(new GameStartedEvent());
                };
            }
            else
            {
                Debug.Log("Restart Game Clicked");
            }
        }
    }
}

