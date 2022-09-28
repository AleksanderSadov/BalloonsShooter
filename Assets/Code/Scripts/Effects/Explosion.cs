using BalloonsShooter.Core;
using BalloonsShooter.Effects.Events;
using UnityEngine;

namespace BalloonsShooter.Effects
{
    public class Explosion : MonoBehaviour
    {
        public GameObject parent;

        public void ExplosionEnded()
        {
            EventsManager.Broadcast(new ExplosionEndedEvent(parent));
        }
    }
}

