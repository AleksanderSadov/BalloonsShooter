namespace BalloonsShooter.Gameplay.Events
{
    public class ComponentEnabledEvent<T> : GameEvent
    {
        public T entity;

        public ComponentEnabledEvent(T entity)
        {
            this.entity = entity;
        }
    }
}

