using BalloonsShooter.Core;

namespace BalloonsShooter.Gameplay.Events
{
    public class EntityEnabledEvent<T> : ApplicationEvent
    {
        public T entity;

        public EntityEnabledEvent(T entity)
        {
            this.entity = entity;
        }
    }
}

