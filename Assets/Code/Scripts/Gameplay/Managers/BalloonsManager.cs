using BalloonsShooter.Core;
using BalloonsShooter.Core.Events;
using BalloonsShooter.Gameplay.Archetypes;
using BalloonsShooter.Gameplay.Events;
using BalloonsShooter.Gameplay.Models;
using BalloonsShooter.Gameplay.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace BalloonsShooter.Gameplay.Manager
{
    public class BalloonsManager : MonoBehaviour
    {
        public Transform balloonPlaneSpawner;
        public Balloon balloonPrefab;

        private BalloonsModel balloonsModel;
        private ObjectPool<Balloon> balloonsObjectPool;
        private bool shouldSpawn = true;
        private BalloonsCountSO balloonsCount;
        private BalloonsSpawnChancesSO balloonsSpawnChances;
        private PlaneHelper spawnPlaneHelper;

        private void OnEnable()
        {
            EventsManager.AddListener<GameStartedEvent>(OnGameStarted);
            EventsManager.AddListener<GameEndedEvent>(OnGameEnded);
            EventsManager.AddListener<DeathCollisionEvent<Balloon>>(OnBalloonDeathZoneCollision);
            EventsManager.AddListener<EntityClickedEvent<Balloon>>(OnBalloonClicked);
        }

        private void Start()
        {
            balloonsModel = ServiceLocator<BalloonsModel>.GetService();
            balloonsCount = ServiceLocator<BalloonsCountSO>.GetService();
            balloonsSpawnChances = ServiceLocator<BalloonsSpawnChancesSO>.GetService();

            int maxBalloonsCount = balloonsCount.MaxBalloonsCount;
            balloonsObjectPool = new(
                createFunc: () =>
                {
                    Balloon balloon = Instantiate(balloonPrefab);
                    return balloon;
                },
                actionOnGet: (Balloon balloon) =>
                {
                    balloon.transform.localScale = balloonPrefab.transform.localScale;
                    balloon.transform.rotation = balloonPrefab.transform.rotation;
                    balloon.transform.position = spawnPlaneHelper.GetRandomPositionOnPlane();
                    balloon.type = balloonsSpawnChances.GetRandomBalloonType();
                    balloon.GetComponent<MeshRenderer>().material = balloon.type.Material;
                    balloon.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    balloon.gameObject.SetActive(true);
                },
                actionOnRelease: (Balloon balloon) =>
                {
                    balloon.gameObject.SetActive(false);
                },
                actionOnDestroy: (Balloon balloon) =>
                {
                    if (balloon.gameObject != null) Destroy(balloon.gameObject);
                },
                collectionCheck: false,
                defaultCapacity: maxBalloonsCount,
                maxSize: maxBalloonsCount * 2
            );

            spawnPlaneHelper = new(balloonPlaneSpawner);
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
                balloon.transform.Translate(balloon.type.FloatSpeed * Time.deltaTime * Vector3.up, Space.World);

                if (balloon.transform.position.x < spawnPlaneHelper.LeftBorderCached)
                {
                    RestrictBalloonMovement(balloon, spawnPlaneHelper.LeftBorderCached);
                }
                else if (balloon.transform.position.x > spawnPlaneHelper.RightBorderCached)
                {
                    RestrictBalloonMovement(balloon, spawnPlaneHelper.RightBorderCached);
                }
            }
        }

        private void RestrictBalloonMovement(Balloon balloon, float positionX)
        {
            Vector3 originalPosition = balloon.transform.position;
            Vector3 restrictedPosition = new(positionX, originalPosition.y, originalPosition.z);
            balloon.transform.position = restrictedPosition;
        }

        private void SpawnRequiredBalloons()
        {
            if (!shouldSpawn) return;

            var activeBalloonsCount = balloonsModel.EnabledEntitiesCached.Count;
            int spawnCount = (int) Mathf.Floor(balloonsCount.RuntimeRequiredBalloonsCount) - activeBalloonsCount;
            for (int i = 0; i < spawnCount; i++)
            {
                balloonsObjectPool.Get();
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
            foreach (Balloon balloon in allBalloons) balloonsObjectPool.Release(balloon);
        }

        private void OnBalloonDeathZoneCollision(DeathCollisionEvent<Balloon> evt) => ReleaseBalloon(evt.entity);
        private void OnBalloonClicked(EntityClickedEvent<Balloon> evt) => ReleaseBalloon(evt.entity);

        private void ReleaseBalloon(Balloon balloon)
        {
            balloonsObjectPool.Release(balloon);
        }
    }
}

