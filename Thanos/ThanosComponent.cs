using FungleAPI.Components;
using FungleAPI.Roles;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ThanosMod.Thanos
{
    internal class ThanosComponent : PlayerComponent
    {
        public static ThanosComponent Instance => PlayerControl.LocalPlayer.GetComponent<ThanosComponent>();
        public static List<StoneBehaviour> OwnedGems = new List<StoneBehaviour>();
        public List<TimePoint> TimePoints = new List<TimePoint>();
        public bool IsInvisible;
        public bool isRewinding;
        public string SkinId;
        public string HatId;
        public string PetId;
        public string VisorId;
        public string PlayerName;
        public int ColorId;
        public PlayerControl Player;
        public bool CanSnap => OwnedGems.Count == 6;
        public void Awake()
        {
            Player = GetComponent<PlayerControl>();
        }
        public void Update()
        {
            if (!isRewinding)
            {
                TimePoints.Add(new TimePoint(Player.Data.IsDead, Player.MyPhysics.body.velocity, Player.transform.position));
                if (TimePoints.Count > 720)
                {
                    TimePoints.RemoveAt(0);
                }
            }
            else
            {
                if (TimePoints.Count > 0)
                {
                    TimePoint timePoint = TimePoints[TimePoints.Count - 1];
                    Player.transform.position = timePoint.Position;
                    Player.MyPhysics.body.velocity = timePoint.Velocity;
                    if (Player.Data.IsDead && !timePoint.IsDead)
                    {
                        Player.Revive();
                        Player.GetBody().Reported = true;
                        Player.GetBody().bodyRenderers.ToArray().ToList().ForEach(new Action<SpriteRenderer>(delegate (SpriteRenderer rend)
                        {
                            rend.color = Color.clear;
                        }));
                    }
                    TimePoints.Remove(timePoint);
                }
                else
                {
                    isRewinding = false;
                    TimePoints.Clear();
                }
            }
            Color color = Player.Visible ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
            if (IsInvisible && Player.Visible)
            {
                if (!Player.AmOwner)
                {
                    color = new Color(1, 1, 1, 0);
                }
                if (Player.Data.Role.GetTeam() == PlayerControl.LocalPlayer.Data.Role.GetTeam() || Player.Data.IsDead)
                {
                    color = new Color(1, 1, 1, 0.4f);
                }
            }
            Player.cosmetics.nameText.color = new Color(Player.cosmetics.nameText.color.r, Player.cosmetics.nameText.color.g, Player.cosmetics.nameText.color.b, color.a);
            Player.cosmetics.hat.FrontLayer.color = color;
            Player.cosmetics.hat.BackLayer.color = color;
            Player.cosmetics.skin.layer.color = color;
            Player.cosmetics.visor.Image.color = color;
            if (Player.cosmetics.CurrentPet != null)
            {
                Player.cosmetics.CurrentPet.renderers.ToArray().ToList().ForEach(new Action<SpriteRenderer>(delegate (SpriteRenderer rend)
                {
                    rend.color = color;
                }));
            }
            Player.cosmetics.currentBodySprite.BodySprite.color = color;
        }
        public void SaveSkin()
        {
            SkinId = Player.Data.DefaultOutfit.SkinId;
            HatId = Player.Data.DefaultOutfit.HatId;
            VisorId = Player.Data.DefaultOutfit.VisorId;
            PetId = Player.Data.DefaultOutfit.PetId;
            ColorId = Player.Data.DefaultOutfit.ColorId;
            PlayerName = Player.Data.DefaultOutfit.PlayerName;
        }
        public void CopySkin(PlayerControl player)
        {
            Player.cosmetics.SetColor(player.Data.DefaultOutfit.ColorId);
            Player.cosmetics.SetSkin(player.Data.DefaultOutfit.SkinId, player.Data.DefaultOutfit.ColorId);
            Player.cosmetics.SetHat(player.Data.DefaultOutfit.HatId, player.Data.DefaultOutfit.ColorId);
            Player.cosmetics.SetVisor(player.Data.DefaultOutfit.VisorId, player.Data.DefaultOutfit.ColorId);
            Player.SetPet(player.Data.DefaultOutfit.PetId);
            Player.cosmetics.SetName(player.Data.DefaultOutfit.PlayerName);
        }
        public void SetOriginalSkin()
        {
            Player.cosmetics.SetColor(ColorId);
            Player.cosmetics.SetSkin(SkinId, ColorId);
            Player.cosmetics.SetHat(HatId, ColorId);
            Player.cosmetics.SetVisor(VisorId, ColorId);
            Player.SetPet(PetId);
            Player.cosmetics.SetName(PlayerName);
        }
    }
}
