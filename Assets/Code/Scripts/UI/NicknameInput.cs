using BalloonsShooter.Gameplay.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace BalloonsShooter.UI
{
    public class NicknameInput : MonoBehaviour
    {
        [SerializeField]
        private UIDocument document;
        [SerializeField]
        private PlayerNicknameSO playerNicknameSO;

        private TextField nicknameInput;

        private void Awake()
        {
            nicknameInput = document.rootVisualElement.Q<TextField>(UIConstants.NICKNAME_INPUT_NAME);
            if (!string.IsNullOrEmpty(playerNicknameSO.nickname)) nicknameInput.value = playerNicknameSO.nickname;
        }

        private void OnEnable()
        {
            nicknameInput.RegisterCallback<ChangeEvent<string>>(OnInputValueChange);
        }

        private void OnDisable()
        {
            nicknameInput.UnregisterCallback<ChangeEvent<string>>(OnInputValueChange);
        }

        private void OnInputValueChange(ChangeEvent<string> evt)
        {
            playerNicknameSO.nickname = evt.newValue.Trim();
        }
    }
}

