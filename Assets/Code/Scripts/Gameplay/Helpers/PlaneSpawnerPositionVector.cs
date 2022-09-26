using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.Interfaces;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Helpers
{
    public class PlaneSpawnerPositionVector : IRandomSpawnPosition
    {
        private readonly Transform spawnerPlane;
        private readonly float halfPlaneWidth;
        private readonly float halfPlaneHeight;

        public PlaneSpawnerPositionVector(Transform spawnerPlane)
        {
            this.spawnerPlane = spawnerPlane;
            halfPlaneWidth = spawnerPlane.localScale.x * GameConstants.PLANE_DEFAULT_SIZE.x / 2;
            halfPlaneHeight = spawnerPlane.localScale.z * GameConstants.PLANE_DEFAULT_SIZE.z / 2;
        }

        public Vector3 GetRandomSpawnPosition()
        {
            Vector3 startingPoint = spawnerPlane.position;

            Vector3 randomVector = VectorHelper.GetRandomVector(
                new Vector3(-halfPlaneWidth, -halfPlaneHeight, 0),
                new Vector3(halfPlaneWidth, halfPlaneHeight, 0)
            );
            Vector3 spawnPosition = startingPoint + randomVector;

            return spawnPosition;
        }
    }
}

