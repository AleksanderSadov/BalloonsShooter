using BalloonsShooter.Core;

namespace BalloonsShooter.Gameplay.Events
{
    public class EntityDestroyedEvent<T> : ApplicationEvent
    {
        public T entity;

        public EntityDestroyedEvent(T entity)
        {
            this.entity = entity;
        }
    }
}

