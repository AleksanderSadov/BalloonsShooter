using BalloonsShooter.Core;
using BalloonsShooter.Effects.Events;
using UnityEngine;

namespace BalloonsShooter.Effects
{
    public class Explosion : MonoBehaviour
    {
        public GameObject parent;
        public float randomPitchStep;

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
            EventsManager.Broadcast(new ExplosionEndedEvent(parent));
        }
    }
}

