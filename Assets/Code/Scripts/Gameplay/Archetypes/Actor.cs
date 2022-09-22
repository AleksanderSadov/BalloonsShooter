using BalloonsShooter.Gameplay.Events;
using BalloonsShooter.Gameplay.Managers;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Archetypes
{
    public abstract class Actor<T> : MonoBehaviour where T : MonoBehaviour 
    {
        private void Awake()
        {
            EventsManager.Broadcast(new ComponentCreatedEvent<T>(gameObject.GetComponent<T>()));
        }

        private void OnEnable()
        {
            EventsManager.Broadcast(new ComponentEnabledEvent<T>(gameObject.GetComponent<T>()));
        }

        private void OnDisable()
        {
            EventsManager.Broadcast(new ComponentDisabledEvent<T>(gameObject.GetComponent<T>()));
        }

        private void OnDestroy()
        {
            EventsManager.Broadcast(new ComponentDestroyedEvent<T>(gameObject.GetComponent<T>()));
        }
    }
}

