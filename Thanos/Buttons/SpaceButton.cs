using FungleAPI.Networking;
using FungleAPI.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanosMod.Thanos.RPCs;
using UnityEngine;

namespace ThanosMod.Thanos.Buttons
{
    internal class SpaceButton : CustomAbilityButton
    {
        public override bool CanClick => ThanosComponent.OwnedGems.Contains(ShipStatusPatch.Gems[GemType.Space]);
        public override bool CanUse => CanClick;
        public override bool Active => CanClick;
        public override float Cooldown => ThanosRole.CooldownSpaceStone;
        public override string OverrideText => "";
        public override Sprite ButtonSprite => Resources.Assets.SpaceSprite;
        public override bool HaveUses => ThanosRole.MaxPortal != 0;
        public override int NumUses => (int)ThanosRole.MaxPortal;
        public override void Click()
        {
            CustomRpcManager.Instance<SpaceRpc>().Send(PlayerControl.LocalPlayer.GetTruePosition(), PlayerControl.LocalPlayer.NetId);
        }
        public override void Update()
        {
            base.Update();
            Button.transform.localPosition = new Vector2(-4.7f, -1.5f);
            Button.gameObject.SetActive(Active && !MeetingHud.Instance);
            if (Button.transform.parent != HudManager.Instance.transform)
            {
                Button.transform.SetParent(HudManager.Instance.transform);
            }
        }
    }
}
