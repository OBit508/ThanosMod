using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanosMod.Thanos.RPCs
{
    internal class CollectGemRpc : CustomRpc<(GemType type, PlayerControl player)>
    {
        public override void Write(MessageWriter writer, (GemType type, PlayerControl player) value)
        {
            writer.Write((int)value.type);
            writer.WritePlayer(value.player);
            ShipStatusPatch.Gems[value.type].Collect(value.player);
        }
        public override void Handle(MessageReader reader)
        {
            GemType type = (GemType)reader.ReadInt32();
            PlayerControl player = reader.ReadPlayer();
            ShipStatusPatch.Gems[type].Collect(player);
        }
    }
}
