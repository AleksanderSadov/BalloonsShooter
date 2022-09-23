namespace BalloonsShooter.Gameplay.Events
{
    public class EntityCreatedEvent<T> : GameEvent
    {
        public T entity;

        public EntityCreatedEvent(T entity)
        {
            this.entity = entity;
        }
    }
}

