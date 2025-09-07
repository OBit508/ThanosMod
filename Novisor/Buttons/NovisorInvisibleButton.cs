using FungleAPI.Networking;
using FungleAPI.Patches;
using FungleAPI.Roles;
using NovisorMod.RPCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NovisorMod.Novisor.Buttons
{
    internal class NovisorInvisibleButton : CustomAbilityButton
    {
        public override bool CanClick => PlayerControl.LocalPlayer.GetComponent<NovisorComponent>().Transformed;
        public override bool CanUse => CanClick;
        public override float Cooldown => NovisorRole.TransformationCooldown;
        public override bool HaveUses { get; }
        public override int NumUses { get; }
        public override bool TransformButton { get; } = true;
        public override float TransformDuration => NovisorRole.TransformationDuration;
        public override string OverrideText => "";
        public override Sprite ButtonSprite => Resources.Assets.InvisButton;
        public override void Click()
        {
            PlayerControl.LocalPlayer.GetComponent<NovisorComponent>().Invisible = true;
            CustomRpcManager.Instance<InvisibilityRpc>().Send((PlayerControl.LocalPlayer, true), PlayerControl.LocalPlayer.NetId);
        }
        public override void Destransform()
        {
            PlayerControl.LocalPlayer.GetComponent<NovisorComponent>().Invisible = false;
            CustomRpcManager.Instance<InvisibilityRpc>().Send((PlayerControl.LocalPlayer, false), PlayerControl.LocalPlayer.NetId);
        }
    }
}
