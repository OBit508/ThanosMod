using FungleAPI.Role;
using FungleAPI.Role.Teams;
using FungleAPI.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FungleAPI.Configuration;
using ThanosMod.Thanos.Buttons;
using FungleAPI.Utilities;

namespace ThanosMod.Thanos
{
    internal class ThanosRole : ImpostorRole, ICustomRole
    {
        [ModdedNumberOption("Recarga do estalo de dedos", 10, 50)]
        public static float CooldownSnap => 15;
        [ModdedNumberOption("Recarga para volta no tempo", 5, 60)]
        public static float CooldownTimeStone => 30;
        [ModdedNumberOption("Recarga para invisibilidade", 5, 60)]
        public static float CooldownRealityStone => 20;
        [ModdedNumberOption("Recarga para criar portal", 5, 60)]
        public static float CooldownSpaceStone => 20;
        [ModdedNumberOption("Recarga para trocar aparencia", 5, 60)]
        public static float CooldownMindStone => 15;
        [ModdedNumberOption("Recarga para explosão", 5, 60)]
        public static float CooldownPowerStone => 30;
        [ModdedNumberOption("Duração da volta no tempo", 5, 10)]
        public static float TimeDuration => 10;
        [ModdedNumberOption("Duração da invisibilidade", 5, 60)]
        public static float RealityDuration => 10;
        [ModdedNumberOption("Duração da troca de aparencia", 5, 60)]
        public static float MindDuration => 10;
        [ModdedNumberOption("Máximo de portais", 0, 60, 1, null, true, NumberSuffixes.None)]
        public static float MaxPortal => 6;
        [ModdedNumberOption("Tamanho das Jóias", 0.5f, 10, 0.5f, null, false, NumberSuffixes.None)]
        public static float StoneSize => 1;
        [ModdedToggleOption("Jóia do Tempo visível para todos")]
        public static bool VisibilityTime => true;
        [ModdedToggleOption("Jóia do Porder visível para todos")]
        public static bool VisibilityPower => true;
        [ModdedToggleOption("Jóia da Mente visível para todos")]
        public static bool VisibilityMind => true;
        [ModdedToggleOption("Jóia da Alma visível para todos")]
        public static bool VisibilitySoul => true;
        [ModdedToggleOption("Jóia do Espaço visível para todos")]
        public static bool VisibilitySpace => true;
        [ModdedToggleOption("Jóia da Reálidade visível para todos")]
        public static bool VisibilityReality => true;
        public ModdedTeam Team { get; } = ModdedTeam.Impostors;
        public StringNames RoleName { get; } = new Translator("Thanos").StringName;
        public StringNames RoleBlur { get; } = new Translator("Colote as 6 jóias do infinito.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("Colete as 6 jóias do infinito para estalar os dedos e ganhar").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("O Thanos deve coletar todas as 6 jóias do infinito para estalar os dedos e acabar com o jogo.").StringName;
        public Color RoleColor { get; } = Palette.Purple;
        public RoleConfig Configuration => new RoleConfig(this)
        {
            GhostRole = AmongUs.GameOptions.RoleTypes.ImpostorGhost,
            Buttons = new CustomAbilityButton[] { CustomAbilityButton.Instance<MindButton>(), CustomAbilityButton.Instance<PowerButton>(), CustomAbilityButton.Instance<RealityButton>(), CustomAbilityButton.Instance<SpaceButton>(), CustomAbilityButton.Instance<TimeButton>(), CustomAbilityButton.Instance<SnapButton>() },
            Screenshot = Resources.Assets.ScreenShot
        };
        public override void AppendTaskHint(Il2CppSystem.Text.StringBuilder taskStringBuilder)
        {
            taskStringBuilder.AppendFormat("\n{0} {1}\n{2}", RoleName.GetString(), DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.RoleHint), RoleBlurMed.GetString());
        }
        public override bool DidWin(GameOverReason gameOverReason)
        {
            return CustomRoleManager.DidWin(this, gameOverReason);
        }
    }
}
