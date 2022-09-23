using UnityEngine;

namespace BalloonsShooter.Gameplay.Interfaces
{
    public interface ISpawner<T> where T : MonoBehaviour
    {
        public T Spawn();
        public void Kill(T entity);
    }
}

