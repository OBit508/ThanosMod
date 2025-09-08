using FungleAPI.Networking;
using FungleAPI.Roles;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanosMod.Thanos.RPCs;
using UnityEngine;

namespace ThanosMod.Thanos.Buttons
{
    internal class PowerButton : CustomAbilityButton
    {
        public override bool CanClick => ThanosComponent.OwnedGems.Contains(ShipStatusPatch.Gems[StoneType.Power]);
        public override bool CanUse => CanClick;
        public override bool Active => CanClick;
        public override float Cooldown => ThanosRole.CooldownPowerStone;
        public override string OverrideText => "";
        public override Sprite ButtonSprite => Resources.Assets.PowerSprite;
        public override void Click()
        {
            List<PlayerControl> players = new List<PlayerControl>();
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
            {
                if (!player.AmOwner && player.Data.Role.GetTeam() != PlayerControl.LocalPlayer.Data.Role.GetTeam() && Vector2.Distance(player.GetTruePosition(), PlayerControl.LocalPlayer.GetTruePosition()) <= 3 && !player.Data.IsDead)
                {
                    players.Add(player);
                }
            }
            CustomRpcManager.Instance<PowerRpc>().Send((players, PlayerControl.LocalPlayer.GetTruePosition()), PlayerControl.LocalPlayer.NetId);
        }
        public override void Update()
        {
            base.Update();
            Button.transform.localPosition = new Vector2(-3.7f, -1.5f);
            Button.gameObject.SetActive(Active && !MeetingHud.Instance);
            if (Button.transform.parent != HudManager.Instance.transform)
            {
                Button.transform.SetParent(HudManager.Instance.transform);
            }
        }
    }
}
