namespace BalloonsShooter.Core.Events
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

