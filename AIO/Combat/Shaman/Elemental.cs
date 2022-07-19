﻿using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using System.Collections.Generic;
using System.Linq;
using wManager.Wow.Helpers;
using static AIO.Constants;

namespace AIO.Combat.Shaman
{
    using Settings = ShamanLevelSettings;
    internal class Elemental : BaseRotation
    {
        protected override List<RotationStep> Rotation => new List<RotationStep> {
            new RotationStep(new RotationSpell("Auto Attack"), 1f, (s,t) => !Me.IsCast && !RotationCombatUtil.IsAutoAttacking(), RotationCombatUtil.BotTarget),
            new RotationStep(new RotationSpell("Cure Toxins"), 2f, (s,t) => !Me.IsInGroup && (Me.HasDebuffType("Disease") || Me.HasDebuffType("Poison")), RotationCombatUtil.FindMe),
            new RotationStep(new RotationSpell("Cure Toxins"), 3f, (s,t) => (t.HasDebuffType("Disease") || t.HasDebuffType("Poison")) && Settings.Current.ElementalCureToxin, RotationCombatUtil.FindPartyMember),
            new RotationStep(new RotationSpell("Healing Wave"), 4f, (s,t) => !Me.IsInGroup && Me.HealthPercent < 40 && t.HealthPercent > 10, RotationCombatUtil.FindMe),
            new RotationStep(new RotationSpell("Wind Shear"), 15f, (s,t) => t.IsCasting() && t.IsTargetingMeOrMyPetOrPartyMember && t.GetDistance < 20, RotationCombatUtil.FindEnemyCasting),
            new RotationStep(new RotationSpell("Flame Shock"), 16f, (s,t) => Settings.Current.ElementalFlameShock && !t.HaveMyBuff("Flame Shock"), RotationCombatUtil.BotTarget),
            new RotationStep(new RotationSpell("Earth Shock"), 16.1f, (s,t) => Settings.Current.ElementalEarthShock && !t.HaveMyBuff("Earth Shock"), RotationCombatUtil.BotTarget),
            new RotationStep(new RotationBuff("Elemental Mastery"), 17f, RotationCombatUtil.Always, RotationCombatUtil.FindMe),
            new RotationStep(new RotationSpell("Lava Burst"), 18f, (s,t) => t.HaveMyBuff("Flame Shock"), RotationCombatUtil.BotTarget),
            new RotationStep(new RotationSpell("Chain Lightning"), 19f, (s,t) => RotationFramework.Enemies.Count(o => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <=10) >= Settings.Current.ElementalChainlightningTresshold, RotationCombatUtil.BotTarget),
            new RotationStep(new RotationSpell("Lightning Bolt"), 19.1f, (s,t) => !SpellManager.KnowSpell("Chain Lightning"),  RotationCombatUtil.BotTarget),
            new RotationStep(new RotationSpell("Lightning Bolt"), 20f, (s,t) => RotationFramework.Enemies.Count(o => o.IsTargetingMeOrMyPetOrPartyMember && o.Position.DistanceTo(t.Position) <=10) <= 2, RotationCombatUtil.BotTarget),
        };
    }
}