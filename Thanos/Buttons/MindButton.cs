using FungleAPI.Networking;
using FungleAPI.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanosMod.Thanos.RPCs;
using UnityEngine;

namespace ThanosMod.Thanos.Buttons
{
    internal class MindButton : CustomAbilityButton
    {
        public override bool CanClick => ThanosComponent.OwnedGems.Contains(ShipStatusPatch.Gems[StoneType.Mind]);
        public override bool CanUse => CanClick;
        public override bool Active => CanClick;
        public override float Cooldown => ThanosRole.CooldownMindStone;
        public override string OverrideText => "";
        public override Sprite ButtonSprite => Resources.Assets.MindSprite;
        public override bool TransformButton => true;
        public override float TransformDuration => ThanosRole.MindDuration;
        public override void Click()
        {
            List<PlayerControl> players = PlayerControl.AllPlayerControls.ToArray().ToList();
            players.RemoveAll(obj => obj.AmOwner || obj.Data.IsDead);
            CustomRpcManager.Instance<MindRpc>().Send((PlayerControl.LocalPlayer, players[new System.Random().Next(0, players.Count - 1)]), PlayerControl.LocalPlayer.NetId);
        }
        public override void Destransform()
        {
            CustomRpcManager.Instance<MindRpc>().Send((PlayerControl.LocalPlayer, null), PlayerControl.LocalPlayer.NetId);
        }
        public override void Update()
        {
            base.Update();
            Button.transform.localPosition = new Vector2(-3.7f, -0.5f);
            Button.gameObject.SetActive(Active && !MeetingHud.Instance);
            if (Button.transform.parent != HudManager.Instance.transform)
            {
                Button.transform.SetParent(HudManager.Instance.transform);
            }
        }
    }
}
