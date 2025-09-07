using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ThanosMod.Thanos
{
    internal class TimePoint
    {
        public TimePoint(bool isDead, Vector2 velocity, Vector2 position)
        {
            IsDead = isDead;
            Velocity = velocity;
            Position = position;
        }
        public bool IsDead;
        public Vector2 Velocity;
        public Vector2 Position;
    }
}
