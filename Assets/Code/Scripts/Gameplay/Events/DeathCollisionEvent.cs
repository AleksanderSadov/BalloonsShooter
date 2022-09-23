namespace BalloonsShooter.Gameplay.Events
{
    public class DeathCollisionEvent<T> : GameEvent
    {
        public T entity;

        public DeathCollisionEvent(T entity)
        {
            this.entity = entity;
        }
    }
}

