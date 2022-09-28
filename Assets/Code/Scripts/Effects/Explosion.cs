using UnityEngine;

namespace BalloonsShooter.Effects
{
    public class Explosion : MonoBehaviour
    {
        public float randomPitchStep;

        public System.Action<Explosion> OnExplosionEnded; 

        private AudioSource explosionSound;
        private float originalPitch;

        private void Awake()
        {
            explosionSound = GetComponent<AudioSource>();
            originalPitch = explosionSound.pitch;
        }

        public void ExplosionStarted()
        {
            explosionSound.pitch = originalPitch + Random.Range(originalPitch - randomPitchStep, originalPitch + randomPitchStep);
            explosionSound.Play();
        }

        public void ExplosionEnded()
        {
            explosionSound.Stop();
            OnExplosionEnded?.Invoke(this);
        }
    }
}

