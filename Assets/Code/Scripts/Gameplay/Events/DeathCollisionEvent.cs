using BalloonsShooter.Core;

namespace BalloonsShooter.Gameplay.Events
{
    public class DeathCollisionEvent<T> : ApplicationEvent
    {
        public T entity;

        public DeathCollisionEvent(T entity)
        {
            this.entity = entity;
        }
    }
}

