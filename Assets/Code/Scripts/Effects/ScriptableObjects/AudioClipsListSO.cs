using System.Collections.Generic;
using UnityEngine;

namespace BalloonsShooter.Effects.ScriptableObjects
{
	[CreateAssetMenu(fileName = "AudioClipsList", menuName = "ScriptableObjects/AudioClipsList")]
	public class AudioClipsListSO : ScriptableObject
	{
        [SerializeField]
		private List<AudioClip> audioClips = new();

		public AudioClip GetRandomClip()
        {
			if (audioClips.Count == 0) return null;
			int randomClipIndex = Random.Range(0, audioClips.Count);
			return audioClips[randomClipIndex];
		}
	}
}
