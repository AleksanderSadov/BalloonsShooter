using BalloonsShooter.Gameplay.Archetypes;
using BalloonsShooter.Gameplay.Helpers;
using BalloonsShooter.Gameplay.Interfaces;
using BalloonsShooter.Gameplay.Models;
using BalloonsShooter.Gameplay.Systems;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Manager
{
    public class BalloonsManager : MonoBehaviour
    {
        public Transform balloonPlaneSpawner;
        public GameObject balloonPrefab;

        private readonly MoveSystem moveSystem = new();
        private readonly BalloonsModel balloonsModel = new();
        private IRandomSpawnPosition spawnerPositionHelper;

        private void Awake()
        {
            spawnerPositionHelper = new PlaneSpawnerPositionVector(balloonPlaneSpawner);
        }

        private void Update()
        {
            //MoveBalloons();
            SpawnBalloons();
        }

        private void MoveBalloons()
        {
            List<Balloon> activeBalloons = balloonsModel.EnabledEntitiesCached;
            moveSystem.Move(activeBalloons, Vector3.up, 1 * Time.deltaTime);
        }

        private void SpawnBalloons()
        {
            List<Balloon> activeBalloons = balloonsModel.EnabledEntitiesCached;
            if (activeBalloons.Count < 3)
            {
                Vector3 spawnPosition = spawnerPositionHelper.GetRandomSpawnPosition();
                GameObject balloon = Instantiate(
                    balloonPrefab,
                    spawnPosition,
                    balloonPrefab.transform.rotation
                );
            }
        }
    }
}

