using UnityEngine;

namespace BalloonsShooter.Effects.ScriptableObjects
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
