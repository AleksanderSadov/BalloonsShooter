using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.Archetypes;
using BalloonsShooter.Gameplay.Events;
using BalloonsShooter.Gameplay.Helpers;
using BalloonsShooter.Gameplay.Interfaces;
using BalloonsShooter.Gameplay.Models;
using BalloonsShooter.Gameplay.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Manager
{
    public class BalloonsManager : MonoBehaviour
    {
        public Transform balloonPlaneSpawner;
        public Balloon balloonPrefab;

        private readonly BalloonsModel balloonsModel = new();
        private ISpawner<Balloon> balloonsSpawner;
        private bool shouldSpawn = true;
        private BalloonsCountSO balloonsCount;
        private BalloonsSpawnChancesSO balloonsSpawnChances;

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

        private void Start()
        {
            balloonsCount = ServiceLocator<BalloonsCountSO>.GetService();
            balloonsSpawnChances = ServiceLocator<BalloonsSpawnChancesSO>.GetService();
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
            foreach (Balloon balloon in activeBalloons)
            {
                balloon.Move(Vector3.up, balloon.type.FloatSpeed * Time.deltaTime);
            }
        }

        private void SpawnRequiredBalloons()
        {
            if (!shouldSpawn) return;

            List<Balloon> activeBalloons = balloonsModel.EnabledEntitiesCached;
            int spawnCount = balloonsCount.GetCurrentRequiredBalloonsCount() - activeBalloons.Count;
            for (int i = 0; i < spawnCount; i++)
            {
                Balloon balloon = balloonsSpawner.Spawn();
                balloon.type = balloonsSpawnChances.GetRandomBalloonType();
                balloon.GetComponent<MeshRenderer>().material = balloon.type.Material;
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

