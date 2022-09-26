using BalloonsShooter.Core;

namespace BalloonsShooter.Gameplay.Events
{
    public class EntityDisabledEvent<T> : ApplicationEvent
    {
        public T entity;

        public EntityDisabledEvent(T entity)
        {
            this.entity = entity;
        }
    }
}

