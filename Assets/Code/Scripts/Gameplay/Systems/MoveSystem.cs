using BalloonsShooter.Gameplay.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Systems
{
    public class MoveSystem
    {
        public void Move<T>(List<T> entities, Vector3 direction, float speed) where T : IMove
        {
            foreach (T entity in entities)
            {
                entity.Move(direction, speed);
            }
        }
    }
}

