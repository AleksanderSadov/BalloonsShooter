using BalloonsShooter.Core;

namespace BalloonsShooter.Gameplay.Events
{
    public class EntityClickedEvent<T> : ApplicationEvent
    {
        public T entity;

        public EntityClickedEvent(T entity)
        {
            this.entity = entity;
        }
    }
}

