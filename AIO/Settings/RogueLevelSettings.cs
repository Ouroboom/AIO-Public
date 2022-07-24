using MarsSettingsGUI;
using System;
using System.ComponentModel;
using System.Configuration;

namespace AIO.Settings
{
    [Serializable]
    public class RogueLevelSettings : BasePersistentSettings<RogueLevelSettings>
    {
        [Setting]
        [DefaultValue(true)]
        [Category("Fighting")]
        [DisplayName("Ranged Pull")]
        [Description("Should we use ranged pull when we have ranged weapon?")]
        public bool PullRanged { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("Fighting")]
        [DisplayName("Stealth")]
        [Description("Use Stealth?")]
        public bool Stealth { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("Fighting")]
        [DisplayName("Distracting  (not functional atm")]
        [Description("Use distracting while stealthed?")]
        public bool Distract { get; set; }

        [Setting]
        [DefaultValue(2)]
        [Category("Fighting")]
        [DisplayName("Evasion")]
        [Description("Enemycount for using Evasion?")]
        public int Evasion { get; set; }

        [Setting]
        [DefaultValue(2)]
        [Category("Fighting")]
        [DisplayName("Blade Flurry")]
        [Description("Enemycount for using Blade Flurry?")]
        public int BladeFlurry { get; set; }

        [Setting]
        [DefaultValue(2)]
        [Category("Fighting")]
        [DisplayName("Killing Spree")]
        [Description("Enemycount for using Killing Spree?")]
        public int KillingSpree { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("Fighting")]
        [DisplayName("Adrenaline Rush")]
        [Description("Enemycount for using Adrenaline Rush?")]
        public int AdrenalineRush { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("Fighting")]
        [DisplayName("Eviscerate")]
        [Description("Combopoints for using Eviscerate?")]
        public int Eviscerate { get; set; }

        [DropdownList(new string[] { "RogueCombat", "RogueAssassination", "RogueSubtlety" })]
        public override string ChooseTalent { get; set; }

        [DropdownList(new string[] { "Auto", "Combat", "Assassination", "Subtlety" })]
        public override string ChooseRotation { get; set; }

        public RogueLevelSettings()
        {
            ChooseTalent = "RogueCombat";
            Evasion = 2;
            BladeFlurry = 2;
            KillingSpree = 2;
            AdrenalineRush = 3;
            Eviscerate = 3;
            ChooseTalent = "RogueCombat";
            Stealth = false;
            Distract = false;
            PullRanged = true;
        }
    }
}

