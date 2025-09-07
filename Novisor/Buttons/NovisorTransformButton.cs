using FungleAPI.Patches;
using FungleAPI.Roles;
using FungleAPI.Networking;
using NovisorMod.RPCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NovisorMod.Novisor.Buttons
{
    internal class NovisorTransformButton : CustomAbilityButton
    {
        public override bool CanUse => true;
        public override bool CanClick => true;
        public override float Cooldown => NovisorRole.TransformationCooldown;
        public override bool HaveUses { get; }
        public override int NumUses { get; }
        public override bool TransformButton { get; } = true;
        public override float TransformDuration => NovisorRole.TransformationDuration;
        public override string OverrideText => "";
        public override Sprite ButtonSprite => Resources.Assets.TransformButton;
        public override void Click()
        {
            NovisorComponent novisorComponent = PlayerControl.LocalPlayer.GetComponent<NovisorComponent>();
            if (!novisorComponent.Transformed)
            {
                NovisorUtils.TransformPlayer(PlayerControl.LocalPlayer);
                CustomRpcManager.Instance<TransformRpc>().Send((PlayerControl.LocalPlayer, true), PlayerControl.LocalPlayer.NetId);
                novisorComponent.Transformed = true;
            }
        }
        public override void Destransform()
        {
            NovisorComponent novisorComponent = PlayerControl.LocalPlayer.GetComponent<NovisorComponent>();
            if (novisorComponent.Transformed)
            {
                NovisorUtils.DestransformPlayer(PlayerControl.LocalPlayer);
                CustomRpcManager.Instance<TransformRpc>().Send((PlayerControl.LocalPlayer, false), PlayerControl.LocalPlayer.NetId);
                novisorComponent.Transformed = false;
            }
        }
    }
}
