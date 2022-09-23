using BalloonsShooter.Gameplay.Archetypes;
using BalloonsShooter.Gameplay.Events;
using BalloonsShooter.Gameplay.Helpers;
using BalloonsShooter.Gameplay.Interfaces;
using BalloonsShooter.Gameplay.Managers;
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
        private ISpawner<Balloon> balloonsSpawner;

        private void Awake()
        {
            balloonsSpawner = new PlaneSpawnerHelper<Balloon>(
                balloonPrefab,
                balloonPlaneSpawner,
                defaultCapacity: 10,
                maxCapacity: 100
            );
        }

        private void OnEnable()
        {
            EventsManager.AddListener<DeathCollisionEvent<Balloon>>(OnBalloonDeathZoneCollision);
        }

        private void Update()
        {
            SpawnRequiredBalloons();
            MoveActiveBalloons();
        }

        private void OnDisable()
        {
            EventsManager.RemoveListener<DeathCollisionEvent<Balloon>>(OnBalloonDeathZoneCollision);
        }

        private void MoveActiveBalloons()
        {
            List<Balloon> activeBalloons = balloonsModel.EnabledEntitiesCached;
            moveSystem.Move(activeBalloons, Vector3.up, 5 * Time.deltaTime);
        }

        private void SpawnRequiredBalloons()
        {
            List<Balloon> activeBalloons = balloonsModel.EnabledEntitiesCached;
            if (activeBalloons.Count < 3)
            {
                Balloon balloon = balloonsSpawner.Spawn();
            }
        }

        private void OnBalloonDeathZoneCollision(DeathCollisionEvent<Balloon> evt)
        {
            balloonsSpawner.Kill(evt.entity);
        }
    }
}

