using FungleAPI.Patches;
using FungleAPI.Roles;
using FungleAPI.Networking;
using FungleAPI.Utilities;
using Il2CppSystem.Threading.Tasks;
using NovisorMod.Resources;
using NovisorMod.RPCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NovisorMod.Novisor.Buttons
{
    internal class NovisorHauntButton : CustomAbilityButton
    {
        public static ShapeshifterMinigame ShapPanelPrefab = RoleManager.Instance.AllRoles.FirstOrDefault(role => role.Role == AmongUs.GameOptions.RoleTypes.Shapeshifter).SafeCast<ShapeshifterRole>().ShapeshifterMenu;
        public static ShapeshifterMinigame ShapMinigame;
        public override bool CanClick => PlayerControl.LocalPlayer.GetComponent<NovisorComponent>().HauntingTarget == null && PlayerControl.LocalPlayer.GetComponent<NovisorComponent>().Transformed;
        public override bool CanUse => CanClick;
        public override float Cooldown => NovisorRole.HauntCooldown;
        public override string OverrideText => "";
        public override Sprite ButtonSprite => Resources.Assets.HauntButton;
        public override void Click()
        {
            ShapMinigame = GameObject.Instantiate<ShapeshifterMinigame>(ShapPanelPrefab, Camera.main.transform);
            Minigame.Instance = ShapMinigame;
            ShapMinigame.timeOpened = Time.realtimeSinceStartup;
            if (PlayerControl.LocalPlayer)
            {
                if (MapBehaviour.Instance)
                {
                    MapBehaviour.Instance.Close();
                }
                PlayerControl.LocalPlayer.MyPhysics.SetNormalizedVelocity(Vector2.zero);
            }
            ShapMinigame.StartCoroutine(ShapMinigame.CoAnimateOpen());
            List<PlayerControl> list = new List<PlayerControl>();
            PlayerControl.AllPlayerControls.ForEach(new Action<PlayerControl>(delegate (PlayerControl player)
            {
                if (player != PlayerControl.LocalPlayer && !player.Data.IsDead)
                {
                    list.Add(player);
                }
            }));
            ShapMinigame.potentialVictims = new Il2CppSystem.Collections.Generic.List<ShapeshifterPanel>();
            for (int i = 0; i < list.Count; i++)
            {
                PlayerControl player = list[i];
                int num = i % 3;
                int num2 = i / 3;
                bool flag = PlayerControl.LocalPlayer.Data.Role.NameColor == player.Data.Role.NameColor;
                ShapeshifterPanel shapeshifterPanel = GameObject.Instantiate<ShapeshifterPanel>(ShapMinigame.PanelPrefab, ShapMinigame.transform);
                shapeshifterPanel.transform.localPosition = new Vector3(ShapMinigame.XStart + (float)num * ShapMinigame.XOffset, ShapMinigame.YStart + (float)num2 * ShapMinigame.YOffset, -1f);
                shapeshifterPanel.SetPlayer(i, player.Data, new Action(delegate
                {
                    ShapMinigame.ForceClose();
                    PlayerControl.LocalPlayer.GetComponent<NovisorComponent>().HauntingTarget = player;
                    CustomRpcManager.Instance<HauntRpc>().Send((PlayerControl.LocalPlayer, player), PlayerControl.LocalPlayer.NetId);
                }));
                shapeshifterPanel.NameText.color = (flag ? player.Data.Role.NameColor : Color.white);
                ShapMinigame.potentialVictims.Add(shapeshifterPanel);
            }
        }
    }
}
