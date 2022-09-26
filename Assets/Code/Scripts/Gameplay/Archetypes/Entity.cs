using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.Events;
using BalloonsShooter.Gameplay.Helpers;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Archetypes
{
    public abstract class Entity<T> : MonoBehaviour where T : MonoBehaviour 
    {
        private T entityComponent;

        protected virtual void Awake()
        {
            entityComponent = GetComponent<T>();
            EventsManager.Broadcast(new EntityCreatedEvent<T>(entityComponent));
        }

        protected virtual void OnEnable()
        {
            EventsManager.Broadcast(new EntityEnabledEvent<T>(entityComponent));
        }

        protected virtual void OnDisable()
        {
            EventsManager.Broadcast(new EntityDisabledEvent<T>(entityComponent));
        }

        protected virtual void OnDestroy()
        {
            EventsManager.Broadcast(new EntityDestroyedEvent<T>(entityComponent));
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            CheckCollisionWithDeathZone(other);
        }

        private void OnMouseDown()
        {
            EventsManager.Broadcast(new EntityClickedEvent<T>(entityComponent));    
        }

        private void CheckCollisionWithDeathZone(Collider collider)
        {
            if (collider.CompareTag(GameConstants.TAGS_DEATH_ZONE))
            {
                EventsManager.Broadcast(new DeathCollisionEvent<T>(entityComponent));
            }
        }
    }
}

