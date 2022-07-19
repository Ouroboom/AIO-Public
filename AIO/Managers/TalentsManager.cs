﻿using AIO.Combat.Common;
using robotManager.Helpful;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using wManager.Events;
using wManager.Wow.Helpers;
using static AIO.Constants;
using Timer = robotManager.Helpful.Timer;

internal class TalentsManager : ICycleable
{
    private static string[] Codes = null;
    private readonly Timer AssignTimer = new Timer();

    public static bool HaveTalent(int tree, int talentNumber)
    {
        int _currentRank = Lua.LuaDoString<int>("_, _, _, _, currentRank, _, _, _ = GetTalentInfo(" + tree + ", " + talentNumber + "); return currentRank;");
        return _currentRank > 0;
    }

    public static void Set(bool assignTalents, bool useDefaultTalents, string[] customTalentsCodes, string specialisation = null)
    {
        if (!assignTalents)
        {
            Codes = null;
            return;
        }

        if (useDefaultTalents)
        {
            SetTalentCodes(specialisation);
            Main.Log("Your are using the following default talents build:");
        }
        else
        {
            SetTalentCodes(customTalentsCodes);
            Main.Log("Your are using the following custom talents build:");
        }

        if (Codes.Any())
        {
            Main.Log(Codes.Last());
        }
        else
        {
            Main.LogError("No talent code");
        }
    }

    private static void SetTalentCodes(string specialisation = null)
    {
        switch (specialisation)
        {
            // FURY WARRIOR
            case "WarriorFury":
                Codes = new string[]
                {
                    "0000000000000000000000000000000305020030000000000000000000000000000000000000000000000",
                    "0000000000000000000000000000000305022030500010000000000000000000000000000000000000000",
                    "0000000000000000000000000000000305022030500310000000000000000000000000000000000000000",
                    "0000000000000000000000000000000305022030501310050120000000000000000000000000000000000",
                    "0000000000000000000000000000000305022030501310052120500300000000000000000000000000000",
                    "0000000000000000000000000000000305022030501310053120500300000000000000000000000000000",
                    "0000000000000000000000000000000305022030502310053120500351000000000000000000000000000",
                    "0000000000000000000000000000000305022030504310053120500351000000000000000000000000000",
                    "3500200023300000000000000000000305022030504310053120500351000000000000000000000000000"
                };
                break;

            // Prot WARRIOR
            case "WarriorProtection":
                Codes = new string[]
                {
                    "0000000000000000000000000000000000000000000000000000000000053020200000000000000000000",
                    "0000000000000000000000000000000000000000000000000000000000053020220000000000000000000",
                    "0000000000000000000000000000000000000000000000000000000000053051222000010000000000000",
                    "0000000000000000000000000000000000000000000000000000000000053351223000010501000000000",
                    "0000000000000000000000000000000000000000000000000000000000053351223000010521230000000",
                    "0000000000000000000000000000000000000000000000000000000000053351223000110521330113321",
                    "0000000000000000000000000000000000000000000000000000000000053351225000210521330113321",
                    "0000000000000000000000000000000300000000000000000000000000053351225000210521330113321",
                    "0502000000000000000000000000000305000000000000000000000000053351225000212521330113321"
                };
                break;

            // Arms WARRIOR
            case "WarriorArms":
                Codes = new string[]
                {
                    "3022032123330100202000000000000000000000000000000000000000000000000000000000000000000",
                    "3022032123331100202012000000000000000000000000000000000000000000000000000000000000000",
                    "3022032123333100202012013231251000000000000000000000000000000000000000000000000000000",
                    "3022032123335100202012013231251000000000000000000000000000000000000000000000000000000",
                    "3022032123335100202012013231251325050100000000000000000000000000000000000000000000000"
                };
                break;

            // BLOOD DEATHKNIGHT
            case "DeathKnightBlood":
                Codes = new string[]
                {
                    "2305020530003303231023101351000000000000000000000000000002302203050030000000000000000000",
                    "2305020530003303231023101351000000000000000000000000000002302203050030000000000000000000"
                };
                break;

            // Frost DEATHKNIGHT
            case "DeathKnightFrost":
                Codes = new string[]
                {
                    "0000000000000000000000000000320023503422030123000331013510000000000000000000000000000000",
                    "0000000000000000000000000000320023503422030123000331013512302003050030000000000000000000",
                    "0100000000000000000000000000320023503422030123000331013512302003050030000000000000000000"
                };
                break;

            // Unholy DEATHKNIGHT
            case "DeathKnightUnholy":
                Codes = new string[]
                {
                    "0000000000000000000000000000000000000000000000000000000002302003320032102000000000000000",
                    "0000000000000000000000000000000000000000000000000000000002302003320032122000100000000000",
                    "0000000000000000000000000000000000000000000000000000000002302003350032132000100000000000",
                    "0000000000000000000000000000000000000000000000000000000002302003350032132000150003100000",
                    "0000000000000000000000000000000000000000000000000000000002302003350032152000150003100000",
                    "0000000000000000000000000000000000000000000000000000000002302003350032152000150003133151",
                    "2305020500000000000000000000000000000000000000000000000002302003350032152000150003133151"
                };
                break;

            // AFFLICTION WARLOCK
            case "WarlockAffliction":
                Codes = new string[]
                {
                    "033002200100000000000000000000000000000000000000000000000000000000000000000000000",
                    "235002200100300000000000000000000000000000000000000000000000000000000000000000000",
                    "235002200102301000000000000000000000000000000000000000000000000000000000000000000",
                    "235002200102341023001000000000000000000000000000000000000000000000000000000000000",
                    "235002200102341023301000000000000000000000000000000000000000000000000000000000000",
                    "235002200102341023351010110000000000000000000000000000000000000000000000000000000",
                    "235002200102341023351013115100000000000000000000000000000000000000000000000000000",
                    "235002200102341023351013115100020000000000000000000000000000000000000000000000000",
                    "235002200102351023351033115100322030113020000000000000000000000000000000000000000"
                };
                break;

            // DEMONOLOGY WARLOCK
            case "WarlockDemonology":
                Codes = new string[]
                {
                    "000000000000000000000000000000320330113520253013300100000000000000000000000000000",
                    "000000000000000000000000000000320330113520253013523134100000000000000000000000000",
                    "000000000000000000000000000000320330113520253013523134155000005000000000000000000"
                };
                break;

            // DESTRUCTION WARLOCK
            case "WarlockDestruction":
                Codes = new string[]
                {
                    "000000000000000000000000000000000000000000000000000000005000000000000000000000000",
                    "000000000000000000000000000000020000000000000000000000005000000000000000000000000",
                    "000000000000000000000000000000020000000000000000000000005203205210331051335230351",
                    "000000000000000000000000000003320030002000000000000000005203205210331051335230351"
                };
                break;

            // BM HUNTER
            case "HunterBeastMastery":
                Codes = new string[]
                {
                    "050002000000000000000000000000000000000000000000000000000000000000000000000000000",
                    "052012015250120501000000000000000000000000000000000000000000000000000000000000000",
                    "052012015250120531005010000000000000000000000000000000000000000000000000000000000",
                    "152012015250120531005310510050052000000000000000000000000000000000000000000000000",
                    "152012015250120531305310510052052300000000000000000000000000000000000000000000000"
                };
                break;

            // MS HUNTER
            case "HunterMarksmanship":
                Codes = new string[]
                {
                    "000000000000000000000000000320050312300000000000000000000000000000000000000000000",
                    "000000000000000000000000000321050312300130000000000000000000000000000000000000000",
                    "000000000000000000000000000322050312300132301300000000000000000000000000000000000",
                    "000000000000000000000000000323050312300132301350010000000000000000000000000000000",
                    "000000000000000000000000000323050312300132301350311510000000000000000000000000000",
                    "000000000000000000000000000353050312300132301350313510000000000000000000000000000",
                    "000000000000000000000000000353050312300132301350313515000002000000000000000000000",
                    "500002000000000000000000000353050312300132301350313515000002000000000000000000000",
                    "501002000000000000000000000353050312300132301350313515000002000000000000000000000"
                };
                break;

            // SURVIVAL HUNTER
            case "HunterSurvival":
                Codes = new string[]
                {
                    "000000000000000000000000000000000000000000000000000005000032500033330523134301331",
                    "000000000000000000000000000053051010000000000000000005000032500033330523134301331"
                };
                break;


            // COMBAT ROGUE
            case "RogueCombat":
                Codes = new string[]
                {
                    "00000000000000000000000000002303201000000000000000000000000000000000000000000000000",
                    "00000000000000000000000000002303521000040100000000000000000000000000000000000000000",
                    "00000000000000000000000000002513521000050100000000000000000000000000000000000000000",
                    "00000000000000000000000000002513521000050102200000000000000000000000000000000000000",
                    "00000000000000000000000000002523521000050102201000000000000000000000000000000000000",
                    "00000000000000000000000000002523521000050102231000000000000000000000000000000000000",
                    "00000000000000000000000000002523521000150102231005010000000000000000000000000000000",
                    "00000000000000000000000000002523521000150102231005212510000000000000000000000000000",
                    "02000000000000000000000000002523521000150102231005212510000000000000000000000000000",
                    "32500010504000000000000000002523521000150102231005212510000000000000000000000000000"
                };
                break;

            // ASSA ROGUE
            case "RogueAssassination":
                Codes = new string[]
                {
                    //"00532300535010052010333105100000000000000000000000000000000000000000000000000000000",
                    //"00532300535010052010333105100500500300000000000000000000000000000000000000000000000",
                    //"00532300535010052010333105100500500300000000000000000005020000000000000000000000000"
                    "00000000000000000000000000002303201000000000000000000000000000000000000000000000000",
                    "00000000000000000000000000002303521000040100000000000000000000000000000000000000000",
                    "00000000000000000000000000002513521000050100000000000000000000000000000000000000000",
                    "00000000000000000000000000002513521000050102200000000000000000000000000000000000000",
                    "00000000000000000000000000002523521000050102201000000000000000000000000000000000000",
                    "00000000000000000000000000002523521000050102231000000000000000000000000000000000000",
                    "00000000000000000000000000002523521000150102231005010000000000000000000000000000000",
                    "00000000000000000000000000002523521000150102231005212510000000000000000000000000000",
                    "02000000000000000000000000002523521000150102231005212510000000000000000000000000000",
                    "32500010504000000000000000002523521000150102231005212510000000000000000000000000000"
                };
                break;

            // SUB ROGUE
            case "RogueSublety":
                Codes = new string[]
                {
                    "00000000000000000000000000000000000000000000000000000005020232010322120150135031251",
                    "00530310000000000000000000000000000000000000000000000005020232010322120150135031251",
                    "00532310000000000000000000000000000000000000000000000005020232010322120150135031251",
                    "00532310000000000000000000000000000000000000000000000005020232010322120350135231251",
                    "00532310000000000000000000000000000000000000000000000005020232020322120350135231251"

                };
                break;

            // ENHANCEMENT SHAMAN
            case "ShamanEnhancement":
                Codes = new string[]
                {
                    "00000000000000000000000003020500010000000000000000000000000000000000000000000000",
                    "00000000000000000000000003020500310000000000000000000000000000000000000000000000",
                    "00000000000000000000000003020501310500130000000000000000000000000000000000000000",
                    "00000000000000000000000003020502310500130300000000000000000000000000000000000000",
                    "00000000000000000000000003020502310500132300100000000000000000000000000000000000",
                    "00000000000000000000000003020502310500132303110120000000000000000000000000000000",
                    "00000000000000000000000003020502310500132303112120100000000000000000000000000000",
                    "00000000000000000000000003020502310500132303112123100000000000000000000000000000",
                    "00000000000000000000000003020502310500133303112123105100000000000000000000000000",
                    "00000000000000000000000003020503310500133303113123105100000000000000000000000000",
                    "05003000000000000000000003020503310500133303113123105100000000000000000000000000",
                    "05003000000000000000000003020503310500133303113123105100000000000000000000000000",
                    "05203015000000000000000003020503310502133303113123105100000000000000000000000000"
                };
                break;

            // ELE SHAMAN
            case "ShamanElemental":
                Codes = new string[]
                {
                    "35300015032133513223013510000000000000000000000000000000000000000000000000000000",
                    "35300015032133513223013510050500310000000000000000000000000000000000000000000000",
                    "35300015032133513223013510050520310000000000000000000000000000000000000000000000"
                };
                break;

            // RESTO SHAMAN
            case "ShamanRestoration":
                Codes = new string[]
                {
                    "00000000000000000000000000000000000000000000000000000050005301300000000000000000",
                    "00000000000000000000000000000000000000000000000000000050005301330310000000000000",
                    "00000000000000000000000000000000000000000000000000000050005331330310501000000000",
                    "00000000000000000000000000000000000000000000000000000050005331332310501022331251",
                    "00000000000000000000000000050500100000000000000000000050005331335310501022331251",
                    "00000000000000000000000000050503100000000000000000000050005331335310501022331251"
                };
                break;

            // FERAL DRUID
            case "DruidFeral":
                Codes = new string[]
                {
                    "0000000000000000000000000000500200000000000000000000000000000000000000000000000000000",
                    "0000000000000000000000000000503200030000000000000000000000000000000000000000000000000",
                    "0000000000000000000000000000503202030302000000000000000000000000000000000000000000000",
                    "0000000000000000000000000000503202030322000000000000000000000000000000000000000000000",
                    "0000000000000000000000000000503202032322010050120000000000000000000000000000000000000",
                    "0000000000000000000000000000503202032322010052120030000000000000000000000000000000000",
                    "0000000000000000000000000000503202032322010053120030000000000000000000000000000000000",
                    "0000000000000000000000000000503202032322011053120030010000000000000000000000000000000",
                    "0000000000000000000000000000503202032322011053120030311501000000000000000000000000000",
                    "0000000000000000000000000000503202032322011053120030313511000000000000000000000000000",
                    "0000000000000000000000000000503202032322011053120030313511203500010000000000000000000",
                    "0000000000000000000000000000503202032322011053120030313511203503012000000000000000000",
                    "0000000000000000000000000000503202032322012053120030313511203503012000000000000000000"
                };
                break;


            // BALANCE DRUID
            case "DruidBalance":
                Codes = new string[]
                {
                    "5002000000000000000000000000000000000000000000000000000000000000000000000000000000000",
                    "5032000100000000000000000000000000000000000000000000000000000000000000000000000000000",
                    "5032003115001000000000000000000000000000000000000000000000000000000000000000000000000",
                    "5032003115131032000000000000000000000000000000000000000000000000000000000000000000000",
                    "5032003115131032013000000000000000000000000000000000000000000000000000000000000000000",
                    "5032003115131033213003011000000000000000000000000000000000000000000000000000000000000",
                    "5032003115131033213003311231000000000000000000000000000000000000000000000000000000000",
                    "5032003115131033213003311231000000000000000000000000000000205003012000000000000000000",
                    "5032003115131033213003311231000000000000000000000000000000205003312000000000000000000",
                    "5032003115331033213005311231000000000000000000000000000000205003312000000000000000000"
                };
                break;


            // RESTO DRUID
            case "DruidRestoration":
                Codes = new string[]
                {
                    "0000000000000000000000000000000000000000000000000000000000230033310030000000000000000",
                    "0000000000000000000000000000000000000000000000000000000000230033312030000000000000000",
                    "0000000000000000000000000000000000000000000000000000000000230033312031400530000000000",
                    "0000000000000000000000000000000000000000000000000000000000230033312031500531000000000",
                    "0000000000000000000000000000000000000000000000000000000000230033312031500531003000000",
                    "0000000000000000000000000000000000000000000000000000000000230033312031500531053013000",
                    "0000000000000000000000000000000000000000000000000000000000230033312031500531053313051",
                    "0532000100000000000000000000000000000000000000000000000000230033312031500531053313051",
                    "0532000100000000000000000000000000000000000000000000000000230033312031502531253313051"
                };
                break;


            // FROST MAGE
            case "MageFrost":
                Codes = new string[]
                {
                    "00000000000000000000000000000000000000000000000000000000000503020010000000000000000000",
                    "00000000000000000000000000000000000000000000000000000000000503020310000000000000000000",
                    "00000000000000000000000000000000000000000000000000000000000503030310000000000000000000",
                    "00000000000000000000000000000000000000000000000000000000000503030310003000000000000000",
                    "00000000000000000000000000000000000000000000000000000000002503030310003100000000000000",
                    "00000000000000000000000000000000000000000000000000000000003503030310203110030000000000",
                    "00000000000000000000000000000000000000000000000000000000003503030310203110230142200000",
                    "00000000000000000000000000000000000000000000000000000000003503030310203110230152201000",
                    "00000000000000000000000000000000000000000000000000000000003503030310203130230152201000",
                    "00000000000000000000000000000000000000000000000000000000003523030310203130230152201051",
                    "23000503200003000000000000000000000000000000000000000000003523030310203130230152201051",
                    "23000503310003000000000000000000000000000000000000000000003523030310203130230152201051"
                };
                break;

            // ARCANE MAGE
            case "MageArcane":
                Codes = new string[]
                {
                    "23500503110023015032310250532100000000000000000000000000000000000000000000000000000000",
                    "23500503110023015032310250532100000000000000000000000000002030230010000000000000000000",
                    "23500503110023015032310250532102000000000000000000000000002030230010000000000000000000"
                };
                break;

            // FIRE MAGE
            case "MageFire":
                Codes = new string[]
                {
                    "00000000000000000000000000000000550300023000000000000000000000000000000000000000000000",
                    "00000000000000000000000000000001550300123000300000000000000000000000000000000000000000",
                    "00000000000000000000000000000001550300123002300501000000000000000000000000000000000000",
                    "00000000000000000000000000000001550300123003300531023003000000000000000000000000000000",
                    "00000000000000000000000000000001550300123003310531023213310000000000000000000000000000",
                    "00000000000000000000000000000001550300123003310531023213510000000000000000000000000000",
                    "23000500010000000000000000000001550300123003310531023213510000000000000000000000000000",
                    "23000503110003000000000000000001550300123003310531023213510000000000000000000000000000"
                };
                break;


            // RETRIBUTION PALADIN
            case "PaladinRetribution":
                Codes = new string[]
                {
                    "000000000000000000000000000000000000000000000000000005230041003231000000000000",
                    "000000000000000000000000000000000000000000000000000005230051003231300000000000",
                    "000000000000000000000000000000000000000000000000000005230051203231301133201300",
                    "000000000000000000000000000000000000000000000000000005230051203231302133201330",
                    "000000000000000000000000000000000000000000000000000005230051203231302133221331",
                    "000000000000000000000000000500000000000000000000000005230051203231302133221331",
                    "000000000000000000000000000500000000000000000000000005232251223331322133231331",
                    "050000000000000000000000000500000000000000000000000005232251223331322133231331"
                };
                break;

            // PROTECTION PALADIN
            case "PaladinProtection":
                Codes = new string[]
                {
                    "000000000000000000000000000500513120310000000000000000000000000000000000000000",
                    "000000000000000000000000000500513520310000000000000000000000000000000000000000",
                    "000000000000000000000000000500513520310230000000000000000000000000000000000000",
                    "000000000000000000000000000500513520310231133201000000000000000000000000000000",
                    "000000000000000000000000000500513520310231133201200000000000000000000000000000",
                    "000000000000000000000000000500513520310231133231230000000000000000000000000000",
                    "000000000000000000000000000500513520310231133331230100000000000000000000000000",
                    "000000000000000000000000000500513520310231133331230152230050000300000000000000",
                    "000000000000000000000000000500513520310231133331230152230050000300000000000000"
                };
                break;

            // HOLY PALADIN
            case "PaladinHoly":
                Codes = new string[]
                {
                    "503501510200130531005152210000000000000000000000000000000000000000000000000000",
                    "503501510200130531005152210000000000000000000000000005032050000000000000000000",
                    "503501510200130531005152215000000000000000000000000005032050000000000000000000"
                };
                break;

            // SHADOW PRIEST
            case "PriestShadow":
                Codes = new string[]
                {
                    "0000000000000000000000000000000000000000000000000000000300000000000000000000000000",
                    "0000000000000000000000000000000000000000000000000000000302023001000000000000000000",
                    "0000000000000000000000000000000000000000000000000000000302023041003000000000000000",
                    "0000000000000000000000000000000000000000000000000000000302023051013010000000000000",
                    "0000000000000000000000000000000000000000000000000000000304023051013012023100000000",
                    "0000000000000000000000000000000000000000000000000000000304023051013012023140300000",
                    "0000000000000000000000000000000000000000000000000000000304023051013012023151301000",
                    "0000000000000000000000000000000000000000000000000000000304023051013012023152301000",
                    "0000000000000000000000000000000000000000000000000000000305023051213012023152301051",
                    "0503200100000000000000000000000000000000000000000000000305023051213012023152301051",
                    "0503203100000000000000000000000000000000000000000000000325023051223012323152301051"
                };
                break;

            // HOLY PRIEST
            case "PriestHoly":
                Codes = new string[]
                {
                    "0000000000000000000000000000032050030000000000000000000000000000000000000000000000",
                    "0000000000000000000000000000034050032302122330000000000000000000000000000000000000",
                    "0000000000000000000000000000034050032302142330000300000000000000000000000000000000",
                    "0000000000000000000000000000034050032302152430000331351000000000000000000000000000",
                    "0000000000000000000000000000235050032302152530000331351000000000000000000000000000",
                    "0503203100000000000000000000235050032302152530000331351000000000000000000000000000"
                };
                break;

            // DISCI PRIEST
            case "PriestDiscipline":
                Codes = new string[]
                {
                    "0503203130300512331323231251000000000000000000000000000000000000000000000000000000",
                    "0503203130300512331323231251005501030000000000000000000000000000000000000000000000"
                };
                break;

            default:
                break;
        }
    }

    // Set the custom talents codes to use
    private static void SetTalentCodes(string[] talentsCodes)
    {
        Codes = talentsCodes;
    }

    // Talent pulse
    private void OnMovementPulse(List<Vector3> points, CancelEventArgs cancelable)
    {
        if (Codes == null)
        {
            return;
        }
        if (!Me.IsAlive)
        {
            return;
        }
        if (!AssignTimer.IsReady)
        {
            return;
        }
        AssignTimer.Reset(5 * 60000);

        Main.LogDebug("Assigning Talents");
        AssignTalents(Codes);
    }

    private static int AvailableTalentPoints => Lua.LuaDoString<int>("local unspentTalentPoints, _ = UnitCharacterPoints('player'); return unspentTalentPoints;");

    private static (string, int, int) GetTalentInfo(int page, int talentNumber)
    {
        var talentInfo = Lua.LuaDoString<string>("return GetTalentInfo(" + page + ", " + talentNumber + ");").Split(new string[] { "#||#" }, StringSplitOptions.None);
        return (talentInfo[0], int.Parse(talentInfo[4]), int.Parse(talentInfo[5]));
    }

    // Talent assignation 
    private static void AssignTalents(IEnumerable<string> TalentCodes)
    {
        if (AvailableTalentPoints <= 0)
        {
            Main.LogDebug("No talent point to spend");

            return;
        }


        IEnumerable<string> talentCodes = TalentCodes as string[] ?? TalentCodes.ToArray();
        if (!talentCodes.Any())
        {
            Main.LogError("No talent code");

            return;
        }

        // Number of talents in each tree
        List<int> NumTalentsInTrees = new List<int>
        {
            Lua.LuaDoString<int>("return GetNumTalents(1)"),
            Lua.LuaDoString<int>("return GetNumTalents(2)"),
            Lua.LuaDoString<int>("return GetNumTalents(3)")
        };

        var (talentNames, talentRanks, maxTalentRanks) = (new string[4, 60], new int[4, 60], new int[4, 60]);

        // loop in 3 trees
        for (int k = 1; k <= 3; k++)
        {
            // loop for each talent
            for (int i = 0; i < NumTalentsInTrees[k - 1]; i++)
            {
                (talentNames[k, i], talentRanks[k, i], maxTalentRanks[k, i]) = GetTalentInfo(k, i + 1);
            }
        }

        // Loop for each TalentCode in list
        foreach (string talentsCode in talentCodes)
        {
            // check if talent code length is correct
            if ((NumTalentsInTrees[0] + NumTalentsInTrees[1] + NumTalentsInTrees[2]) != talentsCode.Length)
            {
                Main.LogError("WARNING: Your talents code length is incorrect. Please use " +
                    "http://armory.twinstar.cz/talent-calc.php to generate valid codes.");
                Main.LogError("Talents code : " + talentsCode);

                return;
            }

            // TalentCode per tree
            List<string> TalentCodeTrees = new List<string>()
                {
                    talentsCode.Substring(0, NumTalentsInTrees[0]),
                    talentsCode.Substring(NumTalentsInTrees[0], NumTalentsInTrees[1]),
                    talentsCode.Substring(NumTalentsInTrees[0] + NumTalentsInTrees[1], NumTalentsInTrees[2])
                };

            // loop in 3 trees
            for (int k = 1; k <= 3; k++)
            {
                // loop for each talent
                for (int i = 0; i < NumTalentsInTrees[k - 1]; i++)
                {
                    int _talentNumber = i + 1;
                    int _pointsToAssignInTalent = Convert.ToInt16(TalentCodeTrees[k - 1].Substring(i, 1));

                    var (_talentName, _currentRank, _realMaxRank) = (talentNames[k, i], talentRanks[k, i], maxTalentRanks[k, i]);

                    if (_currentRank > _pointsToAssignInTalent && talentCodes.Last().Equals(talentsCode))
                    {
                        Main.LogError("WARNING: Your assigned talent points don't match your talent code. Please reset your talents or review your talents code." +
                            " You have " + _currentRank + " point(s) in " + _talentName + " where you should have " + _pointsToAssignInTalent + " point(s)");
                        Main.LogError("Talents code : " + talentsCode);

                        return;
                    }
                    if (_pointsToAssignInTalent > _realMaxRank)
                    {
                        Main.LogError($"WARNING : You're trying to assign {_pointsToAssignInTalent} points into {_talentName}," +
                            $" maximum is {_realMaxRank} points for this talent. Please check your talent code.");
                        Main.LogError("Talents code : " + talentsCode);

                        return;
                    }

                    if (_currentRank == _pointsToAssignInTalent) continue;

                    // loop for individual talent rank
                    for (int j = 0; j < _pointsToAssignInTalent - _currentRank; j++)
                    {
                        Lua.LuaDoString("LearnTalent(" + k + ", " + _talentNumber + ")");
                        Thread.Sleep(500 + Usefuls.Latency);

                        (_, talentRanks[k, i], _) = GetTalentInfo(k, _talentNumber);
                        Main.Log("Assigned talent: " + _talentName + " : " + talentRanks[k, i] + "/" + _pointsToAssignInTalent);

                        if (AvailableTalentPoints <= 0)
                        {
                            return;
                        }
                    }
                }
            }
        }
    }

    public void Initialize() => MovementEvents.OnMovementPulse += OnMovementPulse;

    public void Dispose() => MovementEvents.OnMovementPulse -= OnMovementPulse;
}
