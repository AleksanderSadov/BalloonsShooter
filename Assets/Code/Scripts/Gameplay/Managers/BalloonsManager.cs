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
        private float balloonsLeftMargin;
        private float balloonsRightMargin;

        private void OnEnable()
        {
            EventsManager.AddListener<GameStartedEvent>(OnGameStarted);
            EventsManager.AddListener<GameEndedEvent>(OnGameEnded);
            EventsManager.AddListener<DeathCollisionEvent<Balloon>>(OnBalloonDeathZoneCollision);
            EventsManager.AddListener<EntityClickedEvent<Balloon>>(OnBalloonClicked);
        }

        private void Start()
        {
            balloonsCount = ServiceLocator<BalloonsCountSO>.GetService();
            balloonsSpawnChances = ServiceLocator<BalloonsSpawnChancesSO>.GetService();

            int maxBalloonsCount = balloonsCount.GetMaxBalloonsCount();
            balloonsSpawner = new PlaneSpawnerHelper<Balloon>(
                balloonPrefab,
                balloonPlaneSpawner,
                defaultCapacity: maxBalloonsCount,
                maxCapacity: maxBalloonsCount * 2
            );

            float spawnerHalfWidth = balloonPlaneSpawner.localScale.x * GameConstants.PLANE_DEFAULT_SIZE.x / 2;
            balloonsLeftMargin = balloonPlaneSpawner.position.x - spawnerHalfWidth;
            balloonsRightMargin = balloonPlaneSpawner.position.x + spawnerHalfWidth;
        }

        private void Update()
        {
            SpawnRequiredBalloons();
            MoveActiveBalloons();
        }

        private void OnDisable()
        {
            EventsManager.RemoveListener<GameStartedEvent>(OnGameStarted);
            EventsManager.RemoveListener<GameEndedEvent>(OnGameEnded);
            EventsManager.RemoveListener<DeathCollisionEvent<Balloon>>(OnBalloonDeathZoneCollision);
            EventsManager.RemoveListener<EntityClickedEvent<Balloon>>(OnBalloonClicked);
        }

        private void MoveActiveBalloons()
        {
            List<Balloon> activeBalloons = balloonsModel.EnabledEntitiesCached;
            foreach (Balloon balloon in activeBalloons)
            {
                balloon.transform.Translate(balloon.type.FloatSpeed * Time.deltaTime * Vector3.up);

                if (balloon.transform.position.x < balloonsLeftMargin)
                {
                    RestrictBalloonMovement(balloon, balloonsLeftMargin);
                }
                else if (balloon.transform.position.x > balloonsRightMargin)
                {
                    RestrictBalloonMovement(balloon, balloonsRightMargin);
                }
            }
        }

        private void RestrictBalloonMovement(Balloon balloon, float positionX)
        {
            Vector3 originalPosition = balloon.transform.position;
            Vector3 restrictedPosition = new Vector3(positionX, originalPosition.y, originalPosition.z);
            balloon.transform.position = restrictedPosition;
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
                balloon.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        private void OnGameStarted(GameStartedEvent evt)
        {
            shouldSpawn = true;
        }

        private void OnGameEnded(GameEndedEvent evt)
        {
            shouldSpawn = false;
            List<Balloon> allBalloons = balloonsModel.AllEntitiesCached;
            foreach (Balloon balloon in allBalloons) balloonsSpawner.Kill(balloon);
        }

        private void OnBalloonDeathZoneCollision(DeathCollisionEvent<Balloon> evt)
        {
            balloonsSpawner.Kill(evt.entity);
        }

        private void OnBalloonClicked(EntityClickedEvent<Balloon> evt)
        {
            balloonsSpawner.Kill(evt.entity);
        }
    }
}

