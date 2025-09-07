using FungleAPI.Networking.RPCs;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanosMod.Thanos.RPCs
{
    internal class SnapRpc : CustomRpc<string>
    {
        public override void Write(MessageWriter writer, string value)
        {
            ThanosUtils.UseSnap();
        }
        public override void Handle(MessageReader reader)
        {
            ThanosUtils.UseSnap();
        }
    }
}
