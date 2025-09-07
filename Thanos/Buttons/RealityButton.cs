using AsmResolver.PE.DotNet.ReadyToRun;
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
    internal class RealityButton : CustomAbilityButton
    {
        public override bool CanClick => ThanosComponent.OwnedGems.Contains(ShipStatusPatch.Gems[GemType.Reality]);
        public override bool CanUse => CanClick;
        public override bool Active => CanClick;
        public override float Cooldown => ThanosRole.RealityDuration;
        public override string OverrideText => "";
        public override Sprite ButtonSprite => Resources.Assets.RealitySprite;
        public override bool TransformButton => true;
        public override float TransformDuration => ThanosRole.RealityDuration;
        public override void Click()
        {
            CustomRpcManager.Instance<RealityRpc>().Send((true, PlayerControl.LocalPlayer), PlayerControl.LocalPlayer.NetId);
        }
        public override void Destransform()
        {
            CustomRpcManager.Instance<RealityRpc>().Send((false, PlayerControl.LocalPlayer), PlayerControl.LocalPlayer.NetId);
        }
        public override void Update()
        {
            base.Update();
            Button.transform.localPosition = new Vector2(-3.7f, -2.5f);
            Button.gameObject.SetActive(Active && !MeetingHud.Instance);
            if (Button.transform.parent != HudManager.Instance.transform)
            {
                Button.transform.SetParent(HudManager.Instance.transform);
            }
        }
    }
}
