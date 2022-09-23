using BalloonsShooter.Gameplay.Components;
using BalloonsShooter.Gameplay.Interfaces;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Archetypes
{
    public class Balloon : Entity<Balloon>, IMove
    {
        private DirectedMovement directedMovement;

        protected override void Awake()
        {
            base.Awake();
            directedMovement = GetComponent<DirectedMovement>();
        }

        public void Move(Vector3 direction, float speed) => directedMovement.Move(direction, speed);
    }
}

