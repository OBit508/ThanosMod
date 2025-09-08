using BepInEx.Unity.IL2CPP.Utils.Collections;
using FungleAPI.Components;
using FungleAPI.Networking.RPCs;
using FungleAPI.Role.Teams;
using FungleAPI.Role;
using FungleAPI.Utilities;
using FungleAPI.Utilities.Assets;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ThanosMod.Thanos;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace ThanosMod
{
    internal static class ThanosUtils
    {
        public static Vent LastVent;
        public static void UseSnap()
        {
            System.Collections.IEnumerator Snap()
            {
                Camera.main.GetComponent<FollowerCamera>().ShakeScreen(5, 2);
                yield return HudManager.Instance.CoFadeFullScreen(Color.clear, Color.white, 5);
                if (AmongUsClient.Instance.AmHost)
                {
                    GameManager.Instance.RpcEndGame(GameOverReason.ImpostorsByKill, false);
                }
            }
            GameManager.Instance.StartCoroutine(Snap().WrapToIl2Cpp());
        }
        public static void UsePower(List<PlayerControl> validTargets, Vector2 pos)
        {
            foreach (PlayerControl player in validTargets)
            {
                RpcCustomMurder.CustomMurder(player, player, player.protectedByGuardianId == -1 ? MurderResultFlags.Succeeded : MurderResultFlags.FailedProtected, false, true, false, false, true);
            }
            Animation.Show(Resources.Assets.PowerAnim, pos);
        }
        public static void UseReality(bool invisible, PlayerControl player)
        {
            player.GetComponent<ThanosComponent>().IsInvisible = invisible;
            Animation.Show(Resources.Assets.RealityAnim, player.transform);
        }
        public static void UseSpace(Vector2 pos)
        {
            Vent vent = GameObject.Instantiate<Vent>(ShipStatus.Instance.AllVents[0], ShipStatus.Instance.transform);
            vent.Id = ShipStatus.Instance.AllVents.Count;
            ShipStatus.Instance.AllVents = ShipStatus.Instance.AllVents.Concat(new Vent[] { vent }).ToArray();
            vent.EnterVentAnim = null;
            vent.ExitVentAnim = null;
            if (LastVent != null)
            {
                VentHelper.ShipVents[LastVent].Vents.Add(vent);
            }
            vent.Right = LastVent;
            vent.Center = null;
            vent.Left = null;
            vent.myRend.enabled = false;
            new GameObject("Portal")
            {
                transform =
                {
                    position = pos
                }
            }.AddComponent<SpriteRenderer>().sprite = Resources.Assets.PortalSprite;
            vent.transform.position = pos;
            Animation.Show(Resources.Assets.SpaceAnim, pos);
            LastVent = vent;
        }
        public static void UseTime(bool rewinding, PlayerControl player)
        {
            if (rewinding)
            {
                HudManager.Instance.StartCoroutine(HudManager.Instance.CoFadeFullScreen(Color.clear, new Color(0f, 0.639f, 0.211f, 0.3f)));
            }
            else
            {
                HudManager.Instance.StartCoroutine(HudManager.Instance.CoFadeFullScreen(new Color(0f, 0.639f, 0.211f, 0.3f), Color.clear));
            }
            foreach (PlayerControl p in PlayerControl.AllPlayerControls)
            {
                p.GetComponent<ThanosComponent>().isRewinding = rewinding;
                if (!p.GetComponent<ThanosComponent>().isRewinding)
                {
                    p.GetComponent<ThanosComponent>().TimePoints.Clear();
                }
            }
            Animation.Show(Resources.Assets.TimeAnim, player.transform);
        }
        public static Sprite GetSpriteByType(StoneType type)
        {
            Sprite sprite;
            switch (type)
            {
                case StoneType.Mind: sprite = Resources.Assets.MindSprite; break;
                case StoneType.Power: sprite = Resources.Assets.PowerSprite; break;
                case StoneType.Reality: sprite = Resources.Assets.RealitySprite; break;
                case StoneType.Soul: sprite = Resources.Assets.SoulSprite; break;
                case StoneType.Space: sprite = Resources.Assets.SpaceSprite; break;
                default: sprite = Resources.Assets.TimeSprite; break;
            }
            return sprite;
        }
        public static bool VisibleToEveryone(StoneType Type)
        {
            switch (Type)
            {
                case StoneType.Mind: return ThanosRole.VisibilityMind;
                case StoneType.Power: return ThanosRole.VisibilityPower;
                case StoneType.Reality: return ThanosRole.VisibilityReality;
                case StoneType.Soul: return ThanosRole.VisibilitySoul;
                case StoneType.Space: return ThanosRole.VisibilitySpace;
                default: return ThanosRole.VisibilityTime;
            }
        }
        public static List<Sprite> LoadTileTextureEmbed(string resource, float PixelPerUnit, int TileX, int TileY)
        {
            try
            {
                List<Sprite> list = new List<Sprite>();
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(resource);
                byte[] array = new byte[manifestResourceStream.Length];
                manifestResourceStream.Read(array, 0, (int)manifestResourceStream.Length);
                Texture2D texture2D = new Texture2D(2, 2, TextureFormat.ARGB32, true);
                LoadImage(texture2D, array, true);
                int num = (int)((float)texture2D.width / (float)TileX);
                int num2 = (int)((float)texture2D.height / (float)TileY);
                for (int i = 1; i <= TileX; i++)
                {
                    for (int j = 1; j <= TileY; j++)
                    {
                        list.Add(Sprite.Create(texture2D, new Rect((float)(texture2D.width - num * i), (float)(texture2D.height - num2 * j), (float)num, (float)num2), new Vector2(0.5f, 0.5f), PixelPerUnit, 100U, SpriteMeshType.FullRect, Vector4.zero).DontUnload());
                    }
                }
                return list;
            }
            catch
            {
            }
            return null;
        }
        private static bool LoadImage(Texture2D tex, byte[] data, bool markNonReadable)
        {
            bool flag = iCall_LoadImage == null;
            if (flag)
            {
                iCall_LoadImage = IL2CPP.ResolveICall<d_LoadImage>("UnityEngine.ImageConversion::LoadImage");
            }
            Il2CppStructArray<byte> il2CppStructArray = data;
            return iCall_LoadImage(tex.Pointer, il2CppStructArray.Pointer, markNonReadable);
        }
        internal static d_LoadImage iCall_LoadImage;
        internal delegate bool d_LoadImage(IntPtr tex, IntPtr data, bool markNonReadable);
    }
}
