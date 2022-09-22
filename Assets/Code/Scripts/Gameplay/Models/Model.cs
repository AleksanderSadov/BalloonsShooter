using BalloonsShooter.Gameplay.Events;
using BalloonsShooter.Gameplay.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Models
{
    public class ComponentModel<T> where T : MonoBehaviour
    {
        public List<T> AllEntitiesCached { get; private set; } = new();
        public List<T> EnabledEntitiesCached { get; private set; } = new();

        public ComponentModel()
        {
            EventsManager.AddListener<ComponentCreatedEvent<T>>(OnComponentCreated);
            EventsManager.AddListener<ComponentEnabledEvent<T>>(OnComponentEnabled);
            EventsManager.AddListener<ComponentDisabledEvent<T>>(OnComponentDisabled);
            EventsManager.AddListener<ComponentDestroyedEvent<T>>(OnComponentDestroyed);
        }

        ~ComponentModel()
        {
            EventsManager.RemoveListener<ComponentCreatedEvent<T>>(OnComponentCreated);
            EventsManager.RemoveListener<ComponentEnabledEvent<T>>(OnComponentEnabled);
            EventsManager.RemoveListener<ComponentDisabledEvent<T>>(OnComponentDisabled);
            EventsManager.RemoveListener<ComponentDestroyedEvent<T>>(OnComponentDestroyed);
        }

        private void OnComponentCreated(ComponentCreatedEvent<T> evt)
        {
            AllEntitiesCached.Add(evt.entity);
        }

        private void OnComponentEnabled(ComponentEnabledEvent<T> evt)
        {
            EnabledEntitiesCached.Add(evt.entity);
        }

        private void OnComponentDisabled(ComponentDisabledEvent<T> evt)
        {
            EnabledEntitiesCached.Remove(evt.entity);
        }

        private void OnComponentDestroyed(ComponentDestroyedEvent<T> evt)
        {
            AllEntitiesCached.Remove(evt.entity);
            EnabledEntitiesCached.Remove(evt.entity);
        }
    }
}

