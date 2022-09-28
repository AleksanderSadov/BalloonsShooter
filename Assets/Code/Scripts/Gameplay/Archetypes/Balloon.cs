using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.Events;
using BalloonsShooter.Gameplay.ScriptableObjects;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Archetypes
{
    public class Balloon : Entity<Balloon>
    {
        public BalloonTypeSO type;

        protected virtual void OnTriggerEnter(Collider other)
        {
            CheckCollisionWithDeathZone(other);
        }

        private void CheckCollisionWithDeathZone(Collider collider)
        {
            if (collider.CompareTag(GameConstants.TAGS_DEATH_ZONE))
            {
                EventsManager.Broadcast(new DeathCollisionEvent<Balloon>(this));
            }
        }
    }
}

