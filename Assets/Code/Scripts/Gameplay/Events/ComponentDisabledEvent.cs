namespace BalloonsShooter.Gameplay.Events
{
    public class ComponentDisabledEvent<T> : GameEvent
    {
        public T entity;

        public ComponentDisabledEvent(T entity)
        {
            this.entity = entity;
        }
    }
}

