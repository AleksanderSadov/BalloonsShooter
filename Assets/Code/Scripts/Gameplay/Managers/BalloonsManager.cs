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
        private bool shouldSpawn = true;

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
            EventsManager.AddListener<EntityClickedEvent<Balloon>>(OnBalloonClicked);
            EventsManager.AddListener<PlayerDeathEvent>(OnPLayerDeathEvent);
        }

        private void Update()
        {
            SpawnRequiredBalloons();
            MoveActiveBalloons();
        }

        private void OnDisable()
        {
            EventsManager.RemoveListener<DeathCollisionEvent<Balloon>>(OnBalloonDeathZoneCollision);
            EventsManager.RemoveListener<EntityClickedEvent<Balloon>>(OnBalloonClicked);
        }

        private void MoveActiveBalloons()
        {
            List<Balloon> activeBalloons = balloonsModel.EnabledEntitiesCached;
            moveSystem.Move(activeBalloons, Vector3.up, 5 * Time.deltaTime);
        }

        private void SpawnRequiredBalloons()
        {
            if (!shouldSpawn) return;

            List<Balloon> activeBalloons = balloonsModel.EnabledEntitiesCached;
            while (activeBalloons.Count < 3)
            {
                Balloon balloon = balloonsSpawner.Spawn();
            }
        }

        private void OnBalloonDeathZoneCollision(DeathCollisionEvent<Balloon> evt)
        {
            balloonsSpawner.Kill(evt.entity);
        }

        private void OnBalloonClicked(EntityClickedEvent<Balloon> evt)
        {
            balloonsSpawner.Kill(evt.entity);
        }

        private void OnPLayerDeathEvent(PlayerDeathEvent evt)
        {
            shouldSpawn = false;
            List<Balloon> allBalloons = balloonsModel.AllEntitiesCached;
            foreach (Balloon balloon in allBalloons) Destroy(balloon.gameObject);
            balloonsSpawner.FreeSpawner();
        }
    }
}

