﻿using AIO.Combat.Addons;
using AIO.Combat.Common;
using AIO.Framework;
using AIO.Settings;
using System.Collections.Generic;
using System.ComponentModel;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace AIO.Combat.Druid
{
    using Settings = DruidLevelSettings;
    internal class DruidBehavior : BaseCombatClass
    {
        public static int HealingTouchValue = 0;
        public static int RegrowthValue = 0;
        public static int RejuvenationValue = 0;
        public static int TransformValue = 0;
        private float CombatRange;
        public override float Range => CombatRange;

        internal DruidBehavior() : base(
            Settings.Current,
            new Dictionary<string, BaseRotation>
            {
                {"LowLevel", new LowLevel() },
                {"FeralCombat", new FeralCombat() },
                {"Balance", new Balance() },
                {"Restoration", new Restoration() },
                {"GroupFeralTank", new GroupFeralTank()},
                {"Default", new FeralCombat() },
            },
            new Buffs(),
            new AutoPartyResurrect("Revive"),
            new AutoPartyResurrect("Rebirth", true, Settings.Current.RestorationRebirthAuto))
        {
            Addons.Add(new ConditionalCycleable(() => Settings.Current.HealOOC, new HealOOC(this)));
        }

        public override void Initialize()
        {
            base.Initialize();

            switch (Specialisation)
            {
                case "FeralCombat":
                    CombatRange = (SpellManager.KnowSpell("Growl") || SpellManager.KnowSpell("Cat Form")) ? 5.0f : 29.0f;
                    break;
                case "GroupFeralTank":
                    CombatRange = 5.0f;
                    break;
                default:
                    CombatRange = 29.0f;
                    break;
            }
        }
        protected override void OnFightStart(WoWUnit unit, CancelEventArgs cancelable)
        {
            HealingTouchValue = RotationSpell.GetSpellCost("Healing Touch");
            RejuvenationValue = RotationSpell.GetSpellCost("Rejuvenation");
            RegrowthValue = RotationSpell.GetSpellCost("Regrowth");
            TransformValue = RotationSpell.GetSpellCost("Bear Form");
        }
    }
}