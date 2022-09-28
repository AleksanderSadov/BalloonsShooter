using BalloonsShooter.Effects.ScriptableObjects;
using UnityEngine;

namespace BalloonsShooter.Effects.Managers
{
    [RequireComponent(typeof(AudioSource))]
	public class BackgroundMusicManager : MonoBehaviour
	{
        [SerializeField]
		private AudioClipsListSO musicList;

		private AudioSource musicSource;

        private void Awake()
        {
			musicSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!musicSource.isPlaying)
            {
                musicSource.PlayOneShot(musicList.GetRandomClip());
            }
        }
	}
}
