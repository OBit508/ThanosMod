using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FungleAPI.Utilities.Assets;
using FungleAPI.Utilities.Prefabs;

namespace ThanosMod.Resources
{
    internal static class Assets
    {
        public static void LoadAssets()
        {
            MindAnim = ThanosUtils.LoadTileTextureEmbed("ThanosMod.Resources.Animations.mind.png", 48, 1, 15);
            PickupAnim = ThanosUtils.LoadTileTextureEmbed("ThanosMod.Resources.Animations.pickup.png", 96, 1, 28);
            PowerAnim = ThanosUtils.LoadTileTextureEmbed("ThanosMod.Resources.Animations.power.png", 10, 1, 24);
            RealityAnim = ThanosUtils.LoadTileTextureEmbed("ThanosMod.Resources.Animations.reality.png", 48, 1, 8);
            SpaceAnim = ThanosUtils.LoadTileTextureEmbed("ThanosMod.Resources.Animations.space.png", 64, 1, 30);
            TimeAnim = ThanosUtils.LoadTileTextureEmbed("ThanosMod.Resources.Animations.time.png", 64, 1, 13);
            MindSprite = ResourceHelper.LoadSprite(ThanosModPlugin.Plugin, "ThanosMod.Resources.Sprites.mind", 320);
            PortalSprite = ResourceHelper.LoadSprite(ThanosModPlugin.Plugin, "ThanosMod.Resources.Sprites.portal", 320);
            PowerSprite = ResourceHelper.LoadSprite(ThanosModPlugin.Plugin, "ThanosMod.Resources.Sprites.power", 320);
            RealitySprite = ResourceHelper.LoadSprite(ThanosModPlugin.Plugin, "ThanosMod.Resources.Sprites.reality", 320);
            SnapSprite = ResourceHelper.LoadSprite(ThanosModPlugin.Plugin, "ThanosMod.Resources.Sprites.snap", 320);
            SoulSprite = ResourceHelper.LoadSprite(ThanosModPlugin.Plugin, "ThanosMod.Resources.Sprites.soul", 320);
            SpaceSprite = ResourceHelper.LoadSprite(ThanosModPlugin.Plugin, "ThanosMod.Resources.Sprites.space", 300);
            TimeSprite = ResourceHelper.LoadSprite(ThanosModPlugin.Plugin, "ThanosMod.Resources.Sprites.time", 320);
            ScreenShot = ResourceHelper.LoadSprite(ThanosModPlugin.Plugin, "ThanosMod.Resources.Sprites.screenshot", 100);
            ArrowBehaviour arrowPrefab = new GameObject("Arrow").AddComponent<ArrowBehaviour>();
            arrowPrefab.gameObject.AddComponent<SpriteRenderer>().sprite = ResourceHelper.LoadSprite(ThanosModPlugin.Plugin, "ThanosMod.Resources.Sprites.arrow", 150);
            arrowPrefab.gameObject.layer = 5;
            ArrowPrefab = new Prefab<ArrowBehaviour>(arrowPrefab);
            StoneBehaviour gemPrefab = new GameObject("Stone").AddComponent<StoneBehaviour>();
            gemPrefab.Rend = gemPrefab.gameObject.AddComponent<SpriteRenderer>();
            gemPrefab.AnimRend = new GameObject("AnimRem")
            {
                transform =
                {
                    parent = gemPrefab.transform
                }
            }.AddComponent<SpriteRenderer>();
            gemPrefab.AnimRend.enabled = false;
            gemPrefab.gameObject.AddComponent<CircleCollider2D>().isTrigger = true;
            GemPrefab = new Prefab<StoneBehaviour>(gemPrefab);
        }
        public static List<Sprite> MindAnim;
        public static List<Sprite> PickupAnim;
        public static List<Sprite> PowerAnim;
        public static List<Sprite> RealityAnim;
        public static List<Sprite> SpaceAnim;
        public static List<Sprite> TimeAnim;
        public static Sprite MindSprite;
        public static Sprite PortalSprite;
        public static Sprite PowerSprite;
        public static Sprite RealitySprite;
        public static Sprite SnapSprite;
        public static Sprite SoulSprite;
        public static Sprite SpaceSprite;
        public static Sprite TimeSprite;
        public static Sprite ScreenShot;
        public static Prefab<ArrowBehaviour> ArrowPrefab;
        public static Prefab<StoneBehaviour> GemPrefab;
    }
}
