using System.Collections.Generic;
using UnityEngine;

namespace BalloonsShooter.Effects.ScriptableObjects
{
	[CreateAssetMenu(fileName = "SkyboxList", menuName = "ScriptableObjects/SkyboxList")]
	public class SkyboxListSO : ScriptableObject
	{
		[SerializeField]
		private float rotationSpeed;
		[SerializeField]
		private List<Material> skyboxMaterials = new();

        public float RotationSpeed { get => rotationSpeed; private set => rotationSpeed = value; }
        public List<Material> SkyboxMaterials { get => skyboxMaterials; private set => skyboxMaterials = value; }

        private void OnValidate()
        {
			RotationSpeed = Mathf.Clamp(RotationSpeed, 0, 360);
        }

		public Material GetRandomSkybox()
        {
			if (SkyboxMaterials.Count == 0) return null;
			int randomIndex = Random.Range(0, SkyboxMaterials.Count);
			return SkyboxMaterials[randomIndex];
        }
	}
}
