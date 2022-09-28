using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonsShooter.Gameplay.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Skybox", menuName = "ScriptableObjects/Skybox")]
	public class SkyboxSO : ScriptableObject
	{
		[SerializeField]
		private float rotationSpeed;
		[SerializeField]
		private List<Material> skyboxMaterials = new();

		private Coroutine coroutine;
		private bool shouldRunSkyboxRotation;

		private void OnEnable()
		{
			EventsManager.AddListener<GameStartedEvent>(OnGameStarted);
		}

		private void OnDisable()
		{
			EventsManager.RemoveListener<GameStartedEvent>(OnGameStarted);
			shouldRunSkyboxRotation = false;
		}

        private void OnValidate()
        {
			rotationSpeed = Mathf.Clamp(rotationSpeed, 0, 360);
        }

        private void OnGameStarted(GameStartedEvent evt)
        {
			var randomSkybox = GetRandomSkybox();
			if (randomSkybox != null) RenderSettings.skybox = randomSkybox;

			shouldRunSkyboxRotation = true;
			if (coroutine != null) MonoInstance.Instance.StopCoroutine(coroutine);
			coroutine = MonoInstance.Instance.StartCoroutine(DifficultyTimer());
		}

		public Material GetRandomSkybox()
        {
			if (skyboxMaterials.Count == 0) return null;
			int randomIndex = Random.Range(0, skyboxMaterials.Count - 1);
			return skyboxMaterials[randomIndex];
        }

		private IEnumerator DifficultyTimer()
		{
			while (shouldRunSkyboxRotation)
			{
				RenderSettings.skybox.SetFloat("_Rotation", rotationSpeed * Time.time);
				yield return null;
			}
		}
	}
}
