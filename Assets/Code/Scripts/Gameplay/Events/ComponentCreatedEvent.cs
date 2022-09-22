namespace BalloonsShooter.Gameplay.Events
{
    public class ComponentCreatedEvent<T> : GameEvent
    {
        public T entity;

        public ComponentCreatedEvent(T entity)
        {
            this.entity = entity;
        }
    }
}

