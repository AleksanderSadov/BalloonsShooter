using BalloonsShooter.Gameplay.Components;
using BalloonsShooter.Gameplay.Interfaces;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Archetypes
{
    public class Balloon : Actor<Balloon>, IMove
    {
        private DirectedMovement movement;

        protected override void Awake()
        {
            base.Awake();
            movement = GetComponent<DirectedMovement>();
        }

        public void Move(Vector3 direction, float speed) => movement.Move(direction, speed);
    }
}

