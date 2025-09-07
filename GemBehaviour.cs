using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FungleAPI.Attributes;
using ThanosMod.Thanos;
using FungleAPI.Networking;
using ThanosMod.Thanos.RPCs;
using FungleAPI.Roles;

namespace ThanosMod
{
    [RegisterTypeInIl2Cpp]
    public class GemBehaviour : MonoBehaviour
    {
        public GemType Type;
        public Vector2 CurrentPosition;
        public PlayerControl CurrentOwner;
        public SpriteRenderer Rend;
        public SpriteRenderer AnimRend;
        public int sprite;
        public bool Visible => PlayerControl.LocalPlayer.Data.Role.GetTeam() == FungleAPI.Role.Teams.ModdedTeam.Impostors || PlayerControl.LocalPlayer.Data.Role.GetTeam() != FungleAPI.Role.Teams.ModdedTeam.Impostors && ThanosUtils.VisibleToEveryone(Type);
        public bool spawned => transform.position != Vector3.zero;
        public void Start()
        {
            Rend = GetComponent<SpriteRenderer>();
            AnimRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
            transform.localScale = new Vector3(ThanosRole.StoneSize / 2, ThanosRole.StoneSize / 2, ThanosRole.StoneSize / 2);
            Rend.sprite = ThanosUtils.GetSpriteByType(Type);
            CurrentPosition = ShipStatusPatch.ValidSpawns[new System.Random().Next(0, ShipStatusPatch.ValidSpawns.Count - 1)];
            transform.position = CurrentPosition;
            ShipStatusPatch.ValidSpawns.Remove(CurrentPosition);
            if (Type == GemType.Soul)
            {
                ShipStatusPatch.Arrow.target = CurrentPosition;
            }
            AnimRend.enabled = false;
        }
        public void Update()
        {
            try
            {
                Rend.enabled = Visible && CurrentOwner == null;
                if (AnimRend.enabled)
                {
                    sprite++;
                    if (sprite == Resources.Assets.PickupAnim.Count)
                    {
                        sprite = 0;
                        AnimRend.enabled = false;
                        return;
                    }
                    AnimRend.sprite = Resources.Assets.PickupAnim[sprite];

                }
                if (Type == GemType.Soul)
                {
                    if (CurrentOwner != null && CurrentOwner.Data.Role.GetTeam() == FungleAPI.Role.Teams.ModdedTeam.Crewmates)
                    {
                        CurrentOwner.cosmetics.nameText.color = Palette.Orange;
                    }
                    ShipStatusPatch.Arrow.gameObject.SetActive(CurrentOwner == null && PlayerControl.LocalPlayer.Data.Role.GetTeam() == FungleAPI.Role.Teams.ModdedTeam.Crewmates && Visible);
                }
            }
            catch
            {
            }
        }
        public void Collect(PlayerControl player)
        {
            if (CurrentOwner == null)
            {
                CurrentOwner = player;
                ThanosComponent.OwnedGems.Add(this);
                if (Visible)
                {
                    Rend.enabled = false;
                }
                Animation.Show(Resources.Assets.PickupAnim, transform.position);
                HudManager.Instance.SetHudActive(PlayerControl.LocalPlayer, PlayerControl.LocalPlayer.Data.Role, true);
            }
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            PlayerControl player = other.GetComponent<PlayerControl>();
            if (spawned && player != null && (AmongUsClient.Instance.AmHost && AmongUsClient.Instance.NetworkMode != NetworkModes.FreePlay || AmongUsClient.Instance.AmHost && AmongUsClient.Instance.NetworkMode == NetworkModes.FreePlay && player.AmOwner) && (player.Data.Role.GetTeam() == FungleAPI.Role.Teams.ModdedTeam.Impostors || player.Data.Role.GetTeam() != FungleAPI.Role.Teams.ModdedTeam.Impostors && ThanosUtils.VisibleToEveryone(Type)) && (player.Data.Role.GetTeam() == FungleAPI.Role.Teams.ModdedTeam.Impostors || Type == GemType.Soul))
            {
                CustomRpcManager.Instance<CollectGemRpc>().Send((Type, player), PlayerControl.LocalPlayer.NetId);
            }
        }
    }
}
