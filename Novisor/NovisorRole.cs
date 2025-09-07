using FungleAPI.Role;
using FungleAPI.Role.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FungleAPI.Translation;
using FungleAPI.Configuration;
using FungleAPI.Roles;
using NovisorMod.Novisor.Buttons;
using NovisorMod.RPCs;

namespace NovisorMod.Novisor
{
    internal class NovisorRole : RoleBehaviour, ICustomRole
    {
        [ModdedNumberOption("Recarga para clonar", 5, 60, 5)]
        public static float SplitCooldown => 25;
        [ModdedNumberOption("Recarga para assombrar", 5, 60, 5)]
        public static float HauntCooldown => 25;
        [ModdedNumberOption("Recarga da invisibilidade", 5, 60, 5)]
        public static float InvisibleCooldown => 25;
        [ModdedNumberOption("Duração da invisibilidade", 5, 60, 5)]
        public static float InvisibleDuration => 25;
        [ModdedNumberOption("Recarga da transformação", 5, 60, 5)]
        public static float TransformationCooldown => 25;
        [ModdedNumberOption("Duração da transformação", 5, 60, 5)]
        public static float TransformationDuration => 25;
        public ModdedTeam Team { get; } = ModdedTeam.Impostors;
        public StringNames RoleName { get; } = new Translator("Novisor").StringName;
        public StringNames RoleBlur { get; } = new Translator("Haunt the crew").StringName;
        public StringNames RoleBlurMed => RoleBlur;
        public StringNames RoleBlurLong => RoleBlur;
        public Color RoleColor { get; } = Color.red;
        public RoleConfig Configuration => new RoleConfig(this)
        {
            CanKill = true,
            UseVanillaKillButton = true,
            CanSabotage = true,
            CanVent = true,
            Buttons = new CustomAbilityButton[] { CustomAbilityButton.Instance<NovisorHauntButton>(), CustomAbilityButton.Instance<NovisorTransformButton>(), CustomAbilityButton.Instance<NovisorSplitButton>(), CustomAbilityButton.Instance<NovisorInvisibleButton>() }
        };
    }
}
