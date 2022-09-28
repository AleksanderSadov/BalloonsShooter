using BalloonsShooter.Core;
using BalloonsShooter.Effects.ScriptableObjects;
using BalloonsShooter.Gameplay.Events;
using UnityEngine;

namespace BalloonsShooter.Effects.Managers
{
	public class SkyboxManager : MonoBehaviour
	{
        [SerializeField]
		private SkyboxListSO skyboxList;

		private void OnEnable()
		{
			EventsManager.AddListener<GameStartedEvent>(OnGameStarted);
		}

        private void Update()
        {
			RenderSettings.skybox.SetFloat("_Rotation", skyboxList.RotationSpeed * Time.time);
		}

        private void OnDisable()
		{
			EventsManager.RemoveListener<GameStartedEvent>(OnGameStarted);
		}

		private void OnGameStarted(GameStartedEvent evt) => ChangeSkybox();

		private void ChangeSkybox()
        {
			var randomSkybox = skyboxList.GetRandomSkybox();
			if (randomSkybox != null) RenderSettings.skybox = Instantiate(randomSkybox);
		}
	}
}
