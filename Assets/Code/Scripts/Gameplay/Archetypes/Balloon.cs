using BalloonsShooter.Gameplay.Components;
using BalloonsShooter.Gameplay.Interfaces;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Archetypes
{
    public class Balloon : Actor<Balloon>, IMove
    {
        private DirectedMovement movement;

        private void Awake()
        {
            movement = GetComponent<DirectedMovement>();
        }

        public void Move(Vector3 direction, float speed) => movement.Move(direction, speed);
    }
}

