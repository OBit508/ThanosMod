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
    internal class SnapButton : CustomAbilityButton
    {
        public override bool CanClick => ThanosComponent.Instance.CanSnap;
        public override bool CanUse => CanClick;
        public override bool Active => CanClick;
        public override float Cooldown => ThanosRole.CooldownSnap;
        public override string OverrideText => "";
        public override Sprite ButtonSprite => Resources.Assets.SnapSprite;
        public override void Click()
        {
            CustomRpcManager.Instance<SnapRpc>().Send(null, PlayerControl.LocalPlayer.NetId);
        }
        public override void Update()
        {
            base.Update();
            Button.transform.localPosition = new Vector2(-4.2f, 0.5f);
            Button.gameObject.SetActive(Active && !MeetingHud.Instance);
            if (Button.transform.parent != HudManager.Instance.transform)
            {
                Button.transform.SetParent(HudManager.Instance.transform);
            }
        }
    }
}
