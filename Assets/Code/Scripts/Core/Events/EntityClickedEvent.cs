namespace BalloonsShooter.Core.Events
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

