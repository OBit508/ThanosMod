using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Il2CppSystem.Linq.Expressions.Interpreter.CastInstruction.CastInstructionNoT;

namespace ThanosMod.Thanos.RPCs
{
    internal class MindRpc : CustomRpc<(PlayerControl thanos, PlayerControl target)>
    {
        public override void Write(MessageWriter writer, (PlayerControl thanos, PlayerControl target) value)
        {
            writer.WritePlayer(value.thanos);
            writer.Write(value.target == null);
            if (value.target != null)
            {
                writer.WritePlayer(value.target);
                value.thanos.GetComponent<ThanosComponent>().SaveSkin();
                value.thanos.GetComponent<ThanosComponent>().CopySkin(value.target);
            }
            else
            {
                value.thanos.GetComponent<ThanosComponent>().SetOriginalSkin();
            }
            Animation.Show(Resources.Assets.MindAnim, value.thanos.transform);
        }
        public override void Handle(MessageReader reader)
        {
            PlayerControl thanos = reader.ReadPlayer();
            bool targetIsNull = reader.ReadBoolean();
            if (!targetIsNull)
            {
                PlayerControl target = reader.ReadPlayer();
                thanos.GetComponent<ThanosComponent>().SaveSkin();
                thanos.GetComponent<ThanosComponent>().CopySkin(target);
            }
            else
            {
                thanos.GetComponent<ThanosComponent>().SetOriginalSkin();
            }
            Animation.Show(Resources.Assets.MindAnim, thanos.transform);
        }
    }
}
