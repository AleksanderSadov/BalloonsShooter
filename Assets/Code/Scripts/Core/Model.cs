using BalloonsShooter.Core.Events;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonsShooter.Core
{
    public class Model<T> where T : MonoBehaviour
    {
        public List<T> AllEntitiesCached { get; private set; } = new();
        public List<T> EnabledEntitiesCached { get; private set; } = new();

        public Model()
        {
            EventsManager.AddListener<EntityCreatedEvent<T>>(OnEntityCreated);
            EventsManager.AddListener<EntityEnabledEvent<T>>(OnEntityEnabled);
            EventsManager.AddListener<EntityDisabledEvent<T>>(OnEntityDisabled);
            EventsManager.AddListener<EntityDestroyedEvent<T>>(OnEntityDestroyed);
        }

        ~Model()
        {
            EventsManager.RemoveListener<EntityCreatedEvent<T>>(OnEntityCreated);
            EventsManager.RemoveListener<EntityEnabledEvent<T>>(OnEntityEnabled);
            EventsManager.RemoveListener<EntityDisabledEvent<T>>(OnEntityDisabled);
            EventsManager.RemoveListener<EntityDestroyedEvent<T>>(OnEntityDestroyed);
        }

        private void OnEntityCreated(EntityCreatedEvent<T> evt)
        {
            AllEntitiesCached.Add(evt.entity);
        }

        private void OnEntityEnabled(EntityEnabledEvent<T> evt)
        {
            EnabledEntitiesCached.Add(evt.entity);
        }

        private void OnEntityDisabled(EntityDisabledEvent<T> evt)
        {
            EnabledEntitiesCached.Remove(evt.entity);
        }

        private void OnEntityDestroyed(EntityDestroyedEvent<T> evt)
        {
            AllEntitiesCached.Remove(evt.entity);
            EnabledEntitiesCached.Remove(evt.entity);
        }
    }
}

