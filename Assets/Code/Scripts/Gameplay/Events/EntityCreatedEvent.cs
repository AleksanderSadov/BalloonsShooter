using BalloonsShooter.Core;

namespace BalloonsShooter.Gameplay.Events
{
    public class EntityCreatedEvent<T> : ApplicationEvent
    {
        public T entity;

        public EntityCreatedEvent(T entity)
        {
            this.entity = entity;
        }
    }
}

