namespace BalloonsShooter.Gameplay.Events
{
    public class EntityEnabledEvent<T> : GameEvent
    {
        public T entity;

        public EntityEnabledEvent(T entity)
        {
            this.entity = entity;
        }
    }
}

