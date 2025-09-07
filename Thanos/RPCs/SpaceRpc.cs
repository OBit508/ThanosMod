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
    internal class SpaceRpc : CustomRpc<Vector2>
    {
        public override void Write(MessageWriter writer, Vector2 value)
        {
            writer.WriteVector2(value);
            ThanosUtils.UseSpace(value);
        }
        public override void Handle(MessageReader reader)
        {
            Vector2 pos = reader.ReadVector2();
            ThanosUtils.UseSpace(pos);
        }
    }
}
