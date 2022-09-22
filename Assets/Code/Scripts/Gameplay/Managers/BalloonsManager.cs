using BalloonsShooter.Gameplay.Archetypes;
using BalloonsShooter.Gameplay.Models;
using BalloonsShooter.Gameplay.Systems;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Manager
{
    public class BalloonsManager : MonoBehaviour
    {
        private readonly MoveSystem moveSystem = new();
        private readonly BalloonsModel balloonsModel = new();

        private void Update()
        {
            List<Balloon> activeBalloons = balloonsModel.EnabledEntitiesCached;
            moveSystem.Move(activeBalloons, Vector3.up, 1 * Time.deltaTime);
        }
    }
}

