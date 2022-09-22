using UnityEngine;

namespace BalloonsShooter.Gameplay.Interfaces
{
    public interface IMove
    {
        public void Move(Vector3 direction, float speed);
    }
}

