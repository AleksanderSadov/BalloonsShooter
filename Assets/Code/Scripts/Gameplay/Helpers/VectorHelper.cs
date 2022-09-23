using UnityEngine;

namespace BalloonsShooter.Gameplay.Helpers
{
    public static class VectorHelper
    {
        public static Vector3 GetRandomVector(Vector3 min, Vector3 max)
        {
            float randomX = Random.Range(min.x, max.x);
            float randomY = Random.Range(min.y, max.y);
            float randomZ = Random.Range(min.z, max.z);

            Vector3 randomVector = new(randomX, randomY, randomZ);

            return randomVector;
        }
    }
}

