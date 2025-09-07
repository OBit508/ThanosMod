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
    internal class PowerRpc : CustomRpc<(List<PlayerControl> validTargets, Vector2 pos)>
    {
        public override void Write(MessageWriter writer, (List<PlayerControl> validTargets, Vector2 pos) value)
        {
            writer.Write(value.validTargets.Count);
            foreach (PlayerControl player in value.validTargets)
            {
                writer.WritePlayer(player);
            }
            writer.WriteVector2(value.pos);
            ThanosUtils.UsePower(value.validTargets, value.pos);
        }
        public override void Handle(MessageReader reader)
        {
            List<PlayerControl> validTargets = new List<PlayerControl>();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                validTargets.Add(reader.ReadPlayer());
            }
            Vector2 pos = reader.ReadVector2();
            ThanosUtils.UsePower(validTargets, pos);
        }
    }
}
