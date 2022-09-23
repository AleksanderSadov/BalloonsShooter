namespace BalloonsShooter.Gameplay.Events
{
    public class EntityDisabledEvent<T> : GameEvent
    {
        public T entity;

        public EntityDisabledEvent(T entity)
        {
            this.entity = entity;
        }
    }
}

