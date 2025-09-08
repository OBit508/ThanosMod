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
    internal class RequestSpawnRpc : CustomRpc<string>
    {
        public override void Write(MessageWriter writer, string value)
        {
        }
        public override void Handle(MessageReader reader)
        {
            if (AmongUsClient.Instance.AmHost)
            {
                CustomRpcManager.Instance<SpawnStonesRpc>().Send(null, PlayerControl.LocalPlayer.NetId);
            }
        }
    }
}
