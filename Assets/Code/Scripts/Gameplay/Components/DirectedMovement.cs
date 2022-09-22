using UnityEngine;

namespace BalloonsShooter.Gameplay.Components
{
    public class DirectedMovement : MonoBehaviour
    {
        public void Move(Vector3 direction, float speed)
        {
            transform.Translate(speed * direction);
        }
    }
}

