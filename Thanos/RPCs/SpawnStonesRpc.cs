using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ThanosMod.Thanos.RPCs
{
    internal class SpawnStonesRpc : CustomRpc<string>
    {
        public override void Write(MessageWriter writer, string value)
        {
            foreach (StoneBehaviour stone in ShipStatusPatch.Gems.Values)
            {
                writer.WriteVector2(stone.CurrentPosition);
            }
        }
        public override void Handle(MessageReader reader)
        {
            foreach (StoneBehaviour stone in ShipStatusPatch.Gems.Values)
            {
                Vector2 position = reader.ReadVector2();
                stone.CurrentPosition = position;
                stone.transform.position = position;
                if (stone.Type == StoneType.Soul)
                {
                    ShipStatusPatch.Arrow.target = position;
                }
            }
        }
    }
}
