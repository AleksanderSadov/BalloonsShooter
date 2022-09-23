using BalloonsShooter.Gameplay.Archetypes;
using BalloonsShooter.Gameplay.Helpers;
using BalloonsShooter.Gameplay.Models;
using BalloonsShooter.Gameplay.Systems;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Manager
{
    public class BalloonsManager : MonoBehaviour
    {
        public Transform balloonPlaneSpawner;
        public Balloon balloonPrefab;

        private readonly MoveSystem moveSystem = new();
        private readonly BalloonsModel balloonsModel = new();
        private BalloonsSpawner balloonsSpawner;

        private void Awake()
        {
            balloonsSpawner = new(
                balloonPrefab,
                balloonPlaneSpawner,
                defaultCapacity: 10,
                maxCapacity: 100
            );
        }

        private void Update()
        {
            //MoveActiveBalloons();
            SpawnRequiredBalloons();
        }

        private void MoveActiveBalloons()
        {
            List<Balloon> activeBalloons = balloonsModel.EnabledEntitiesCached;
            moveSystem.Move(activeBalloons, Vector3.up, 1 * Time.deltaTime);
        }

        private void SpawnRequiredBalloons()
        {
            List<Balloon> activeBalloons = balloonsModel.EnabledEntitiesCached;
            if (activeBalloons.Count < 3)
            {
                Balloon balloon = balloonsSpawner.Spawn();
            }
        }
    }
}

