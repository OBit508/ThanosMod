using BepInEx;
using BepInEx.Unity.IL2CPP;
using FungleAPI;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using ThanosMod.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using xCloud;

namespace ThanosMod
{
    [BepInProcess("Among Us.exe")]
    [BepInPlugin(ModId, ModName, ModVersion)]
    [BepInDependency(FungleAPIPlugin.ModId)]
    public class ThanosModPlugin : BasePlugin
    {
        public static ModPlugin Plugin;
        public Harmony Harmony { get; } = new Harmony(ModId);
        public override void Load()
        {
            Plugin = ModPlugin.RegisterMod(this, ModVersion, delegate
            {
                Resources.Assets.LoadAssets();
            });
            Harmony.PatchAll();
        }
        public static bool AssetsLoaded;
        public const string ModName = "ThanosMod";
        public const string Owner = "rafael";
        public const string ModDescription = "This is an Update from original ThanosMod";
        public const string ModId = "com." + Owner + "." + ModName;
        public const string ModVersion = "1.0.0";
    }
}
