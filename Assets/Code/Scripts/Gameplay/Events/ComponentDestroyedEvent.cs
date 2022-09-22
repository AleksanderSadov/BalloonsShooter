namespace BalloonsShooter.Gameplay.Events
{
    public class ComponentDestroyedEvent<T> : GameEvent
    {
        public T entity;

        public ComponentDestroyedEvent(T entity)
        {
            this.entity = entity;
        }
    }
}

