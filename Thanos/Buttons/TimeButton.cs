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
    internal class TimeButton : CustomAbilityButton
    {
        public override bool CanClick => ThanosComponent.OwnedGems.Contains(ShipStatusPatch.Gems[StoneType.Time]);
        public override bool CanUse => CanClick;
        public override bool Active => CanClick;
        public override float Cooldown => ThanosRole.TimeDuration;
        public override string OverrideText => "";
        public override Sprite ButtonSprite => Resources.Assets.TimeSprite;
        public override bool TransformButton => true;
        public override float TransformDuration => ThanosRole.TimeDuration;
        public override void Click()
        {
            CustomRpcManager.Instance<TimeRpc>().Send((true, PlayerControl.LocalPlayer), PlayerControl.LocalPlayer.NetId);
        }
        public override void Destransform()
        {
            CustomRpcManager.Instance<TimeRpc>().Send((false, PlayerControl.LocalPlayer), PlayerControl.LocalPlayer.NetId);
        }
        public override void Update()
        {
            base.Update();
            Button.transform.localPosition = new Vector2(-4.7f, -0.5f);
            Button.gameObject.SetActive(Active && !MeetingHud.Instance);
            if (Button.transform.parent != HudManager.Instance.transform)
            {
                Button.transform.SetParent(HudManager.Instance.transform);
            }
        }
    }
}
