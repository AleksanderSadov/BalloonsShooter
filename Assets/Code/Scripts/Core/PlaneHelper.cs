using UnityEngine;

namespace BalloonsShooter.Core
{
    public class PlaneHelper
    {
        private readonly Transform plane;
        public float PlaneWidthCached { get; private set; }
        public float PlaneHeightCached { get; private set; }
        public float HalfPlaneWidthCached { get; private set; }
        public float HalfPlaneHeightCached { get; private set; }
        public float LeftBorderCached { get; private set; }
        public float RightBorderCached { get; private set; }

        public PlaneHelper(Transform plane)
        {
            this.plane = plane;
            PlaneWidthCached = plane.localScale.x * CoreConstants.PLANE_DEFAULT_SIZE.x;
            PlaneHeightCached = plane.localScale.z * CoreConstants.PLANE_DEFAULT_SIZE.z;
            HalfPlaneWidthCached = PlaneWidthCached / 2;
            HalfPlaneHeightCached = PlaneHeightCached / 2;
            LeftBorderCached = plane.position.x - HalfPlaneWidthCached;
            RightBorderCached = plane.position.x + HalfPlaneWidthCached;
        }

        public Vector3 GetRandomPositionOnPlane()
        {
            Vector3 randomVector = VectorHelper.GetRandomVector(
                new Vector3(-HalfPlaneWidthCached, -HalfPlaneHeightCached, 0),
                new Vector3(HalfPlaneWidthCached, HalfPlaneHeightCached, 0)
            );
            Vector3 randomPosition = plane.position + randomVector;

            return randomPosition;
        }
    }
}

