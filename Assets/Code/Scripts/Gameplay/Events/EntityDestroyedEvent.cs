namespace BalloonsShooter.Gameplay.Events
{
    public class EntityDestroyedEvent<T> : GameEvent
    {
        public T entity;

        public EntityDestroyedEvent(T entity)
        {
            this.entity = entity;
        }
    }
}

