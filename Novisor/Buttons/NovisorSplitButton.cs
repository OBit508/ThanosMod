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
    internal class NovisorSplitButton : CustomAbilityButton
    {
        public override bool CanClick => PlayerControl.LocalPlayer.GetComponent<NovisorComponent>().Transformed;
        public override bool CanUse => CanClick;
        public override float Cooldown => NovisorRole.SplitCooldown;
        public override bool HaveUses { get; }
        public override int NumUses { get; }
        public override string OverrideText => "";
        public override Sprite ButtonSprite => Resources.Assets.SplitButton;
        public override void Click()
        {
            NovisorUtils.CreateGhosts(PlayerControl.LocalPlayer, PlayerControl.LocalPlayer.GetTruePosition());
            CustomRpcManager.Instance<GhostsRpc>().Send((PlayerControl.LocalPlayer, PlayerControl.LocalPlayer.GetTruePosition()), PlayerControl.LocalPlayer.NetId);
        }
    }
}
