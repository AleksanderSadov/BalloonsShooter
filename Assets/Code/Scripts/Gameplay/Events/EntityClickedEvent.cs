namespace BalloonsShooter.Gameplay.Events
{
    public class EntityClickedEvent<T> : GameEvent
    {
        public T entity;

        public EntityClickedEvent(T entity)
        {
            this.entity = entity;
        }
    }
}

