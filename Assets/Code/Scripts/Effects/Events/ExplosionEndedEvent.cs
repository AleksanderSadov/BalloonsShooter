using BalloonsShooter.Core;
using UnityEngine;

namespace BalloonsShooter.Effects.Events
{
    public class ExplosionEndedEvent : ApplicationEvent
    {
        public GameObject parent;

        public ExplosionEndedEvent(GameObject parent)
        {
            this.parent = parent;
        }
    }
}

