﻿using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using System.Collections.Generic;
using static AIO.Constants;

namespace AIO.Combat.Mage
{
    using Settings = MageLevelSettings;
    internal class Arcane : BaseRotation
    {
        protected override List<RotationStep> Rotation => new List<RotationStep> {
            new RotationStep(new RotationSpell("Shoot"), 0.9f, (s,t) => Settings.Current.UseWand && Me.ManaPercentage < Settings.Current.UseWandTresh && !RotationCombatUtil.IsAutoRepeating("Shoot"), RotationCombatUtil.BotTarget),
            new RotationStep(new RotationSpell("Auto Attack"), 1f, (s,t) => !Me.IsCast && !RotationCombatUtil.IsAutoAttacking() && !RotationCombatUtil.IsAutoRepeating("Shoot"), RotationCombatUtil.BotTarget),
            new RotationStep(new RotationSpell("Icy Veins"), 3f, (s,t) => Me.BuffStack(36032) >= 1 && t.IsBoss, RotationCombatUtil.BotTarget),
            new RotationStep(new RotationSpell("Arcane Power"), 4f, (s,t) => Me.BuffStack(36032) >= 1 && t.IsBoss, RotationCombatUtil.BotTarget),
            new RotationStep(new RotationSpell("Mirror Image"), 5f, (s,t) => Me.BuffStack(36032) >= 1 && t.IsBoss, RotationCombatUtil.BotTarget),
            new RotationStep(new RotationSpell("Presence of Mind"), 6f, (s,t) => Me.BuffStack(36032) >=2 && t.IsBoss, RotationCombatUtil.BotTarget),
            new RotationStep(new RotationSpell("Arcane Missiles"), 7f, (s,t) =>Me.ManaPercentage > Settings.Current.UseWandTresh && Me.BuffStack(36032) >=3 && Me.HaveBuff(44401), RotationCombatUtil.BotTarget),
            new RotationStep(new RotationSpell("Arcane Blast"), 8f, (s,t) => Me.ManaPercentage > Settings.Current.UseWandTresh, RotationCombatUtil.BotTarget)
        };
    }
}
