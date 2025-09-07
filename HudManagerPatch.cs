using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanosMod.Thanos;
using UnityEngine;

namespace ThanosMod
{
    [HarmonyPatch(typeof(HudManager))]
    internal static class HudManagerPatch
    {
        public static SpriteRenderer SoulGem;
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void StartPostfix(HudManager __instance)
        {
            SoulGem = new GameObject("SoulGem").AddComponent<SpriteRenderer>();
            SoulGem.sprite = Resources.Assets.SoulSprite;
            SoulGem.transform.SetParent(__instance.transform);
            SoulGem.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            SoulGem.transform.localPosition = new Vector2(-4.7f, -2.5f);
            SoulGem.gameObject.SetActive(false);
        }
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        public static void UpdatePostfix()
        {
            if (AmongUsClient.Instance.IsInGame)
            {
                SoulGem.gameObject.SetActive(!MeetingHud.Instance && ThanosComponent.OwnedGems.Contains(ShipStatusPatch.Gems[GemType.Soul]));
            }
        }
    }
}
