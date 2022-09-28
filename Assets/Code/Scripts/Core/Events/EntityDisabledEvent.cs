namespace BalloonsShooter.Core.Events
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

