using BalloonsShooter.Core.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BalloonsShooter.Core
{
    public abstract class Entity<T> : MonoBehaviour, IPointerDownHandler where T : MonoBehaviour 
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

        public void OnPointerDown(PointerEventData eventData)
        {
            EventsManager.Broadcast(new EntityClickedEvent<T>(entityComponent));
        }
    }
}

