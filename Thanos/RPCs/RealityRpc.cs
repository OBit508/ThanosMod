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
    internal class RealityRpc : CustomRpc<(bool invisible, PlayerControl thanos)>
    {
        public override void Write(MessageWriter writer, (bool invisible, PlayerControl thanos) value)
        {
            writer.Write(value.invisible);
            writer.Write(value.thanos);
            ThanosUtils.UseReality(value.invisible, value.thanos);
        }
        public override void Handle(MessageReader reader)
        {
            bool invisible = reader.ReadBoolean();
            PlayerControl thanos = reader.ReadPlayer();
            ThanosUtils.UseReality(invisible, thanos);
        }
    }
}
