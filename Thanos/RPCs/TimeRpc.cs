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
    internal class TimeRpc : CustomRpc<(bool rewinding, PlayerControl player)>
    {
        public override void Write(MessageWriter writer, (bool rewinding, PlayerControl player) value)
        {
            writer.Write(value.rewinding);
            writer.Write(value.player);
            ThanosUtils.UseTime(value.rewinding, value.player);
        }
        public override void Handle(MessageReader reader)
        {
            bool rewinding = reader.ReadBoolean();
            PlayerControl player = reader.ReadPlayer();
            ThanosUtils.UseTime(rewinding, player);
        }
    }
}
