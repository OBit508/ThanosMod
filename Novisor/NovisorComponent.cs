using FungleAPI.MonoBehaviours;
using FungleAPI.Patches;
using FungleAPI.Roles;
using FungleAPI.Networking;
using FungleAPI.Utilities;
using NovisorMod.RPCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace NovisorMod.Novisor
{
    internal class NovisorComponent : PlayerComponent
    {
        public PlayerControl Player => GetComponent<PlayerControl>();
        public bool Transformed;
        public bool Invisible;
        public PlayerControl HauntingTarget;
        public void Update()
        {
            if (Transformed)
            {
                Player.cosmetics.nameText?.gameObject.SetActive(false);
                Player.cosmetics.hat?.gameObject.SetActive(false);
                Player.cosmetics.CurrentPet?.gameObject.SetActive(false);
                Player.cosmetics.skin?.gameObject.SetActive(false);
                Player.cosmetics.visor?.gameObject.SetActive(false);
                Player.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                if (HauntingTarget != null)
                {
                    Player.MyPhysics.StartCoroutine(Player.MyPhysics.WalkPlayerTo(HauntingTarget.GetTruePosition()));
                    Player.transform.position = Vector3.MoveTowards(Player.transform.position, HauntingTarget.transform.position, 5 * Time.deltaTime);
                    if (Player.AmOwner && Vector2.Distance(Player.GetTruePosition(), HauntingTarget.GetTruePosition()) <= 0.3f)
                    {
                        Player.RpcCustomMurderPlayer(HauntingTarget, MurderResultFlags.Succeeded, false, true, true, true, true);
                        HauntingTarget = null;
                        CustomRpcManager.Instance<HauntRpc>().Send((Player, null), Player.NetId);
                    }
                }
            }
            Color color = Player.Visible ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
            if (Invisible && Player.Visible)
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
    }
}
