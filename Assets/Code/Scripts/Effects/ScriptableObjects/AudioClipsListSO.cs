using System.Collections.Generic;
using UnityEngine;

namespace BalloonsShooter.Effects.ScriptableObjects
{
	[CreateAssetMenu(fileName = "AudioClipsList", menuName = "ScriptableObjects/AudioClipsList")]
	public class AudioClipsListSO : ScriptableObject
	{
        [SerializeField]
		private List<AudioClip> audioClips = new();

        public List<AudioClip> AudioClips { get => audioClips; private set => audioClips = value; }

        public AudioClip GetRandomClip()
        {
			if (AudioClips.Count == 0) return null;
			int randomClipIndex = Random.Range(0, AudioClips.Count);
			return AudioClips[randomClipIndex];
		}
	}
}
