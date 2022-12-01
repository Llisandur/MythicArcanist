using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Designers.Mechanics.Recommendations;
using Kingmaker.ElementsSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Utility;
using UnityEngine;
using TabletopTweaks.Core.Utilities;
using static MythicArcanist.Main;
using MythicArcanist.Utilities;

namespace MythicArcanist.NewContent.Spells
{
    static class MagicMissileMastered
    {
        public static void Add()
        {
            BlueprintAbility SpellCopy = BlueprintTools.GetBlueprint<BlueprintAbility>("4ac47ddb9fa1eaf43a1b6809980cfbd2"); //MagicMissile

            BlueprintProjectile MagicMissile00 = BlueprintTools.GetBlueprint<BlueprintProjectile>("2e3992d1695960347a7f9bdf8122966f");
            BlueprintProjectile MagicMissile01 = BlueprintTools.GetBlueprint<BlueprintProjectile>("741743ccd287a854fbb68ce70f75fa05");
            BlueprintProjectile MagicMissile02 = BlueprintTools.GetBlueprint<BlueprintProjectile>("674e6d958be63ff4a85a7e5fdc1e818a");
            BlueprintProjectile MagicMissile03 = BlueprintTools.GetBlueprint<BlueprintProjectile>("caadaf27d789793459a3e32cb0615d14");
            BlueprintProjectile MagicMissile04 = BlueprintTools.GetBlueprint<BlueprintProjectile>("43295b5988021f741a28b8bf0424a412");
            BlueprintProjectileTrajectory MagicMissile00_Trajectory = BlueprintTools.GetBlueprint<BlueprintProjectileTrajectory>("7a2392b759f781344989abb8baa96642");
            BlueprintProjectileTrajectory MagicMissile01_Trajectory = BlueprintTools.GetBlueprint<BlueprintProjectileTrajectory>("55404f083ce05174eb3048bf0e3971aa");
            BlueprintProjectileTrajectory MagicMissile02_Trajectory = BlueprintTools.GetBlueprint<BlueprintProjectileTrajectory>("438b395a152816e4aaefa33a5660c82d");
            BlueprintProjectileTrajectory MagicMissile03_Trajectory = BlueprintTools.GetBlueprint<BlueprintProjectileTrajectory>("94cde76eb07e6c442a6b8bc6d42f88cd");
            BlueprintProjectileTrajectory MagicMissile04_Trajectory = BlueprintTools.GetBlueprint<BlueprintProjectileTrajectory>("c5666a5f7f524db49bea07670900dc6a");

            #region Projectiles
            var MagicMissile05 = MagicMissile00.CreateCopy(ThisModContext, "MagicMissile05");
            var MagicMissile06 = MagicMissile01.CreateCopy(ThisModContext, "MagicMissile06");
            var MagicMissile07 = MagicMissile02.CreateCopy(ThisModContext, "MagicMissile07");
            var MagicMissile08 = MagicMissile03.CreateCopy(ThisModContext, "MagicMissile08");
            var MagicMissile09 = MagicMissile04.CreateCopy(ThisModContext, "MagicMissile09");
            var MagicMissile10 = MagicMissile00.CreateCopy(ThisModContext, "MagicMissile10");
            var MagicMissile11 = MagicMissile01.CreateCopy(ThisModContext, "MagicMissile11");
            var MagicMissile12 = MagicMissile02.CreateCopy(ThisModContext, "MagicMissile12");
            var MagicMissile13 = MagicMissile03.CreateCopy(ThisModContext, "MagicMissile13");
            var MagicMissile14 = MagicMissile04.CreateCopy(ThisModContext, "MagicMissile14");
            var MagicMissile15 = MagicMissile00.CreateCopy(ThisModContext, "MagicMissile15");
            var MagicMissile16 = MagicMissile01.CreateCopy(ThisModContext, "MagicMissile16");
            var MagicMissile17 = MagicMissile02.CreateCopy(ThisModContext, "MagicMissile17");
            var MagicMissile18 = MagicMissile03.CreateCopy(ThisModContext, "MagicMissile18");
            var MagicMissile19 = MagicMissile04.CreateCopy(ThisModContext, "MagicMissile19");
            var MagicMissile20 = MagicMissile00.CreateCopy(ThisModContext, "MagicMissile20");
            var MagicMissile21 = MagicMissile01.CreateCopy(ThisModContext, "MagicMissile21");
            var MagicMissile22 = MagicMissile02.CreateCopy(ThisModContext, "MagicMissile22");
            var MagicMissile23 = MagicMissile03.CreateCopy(ThisModContext, "MagicMissile23");
            var MagicMissile24 = MagicMissile04.CreateCopy(ThisModContext, "MagicMissile24");
            var MagicMissile25 = MagicMissile00.CreateCopy(ThisModContext, "MagicMissile25");
            var MagicMissile26 = MagicMissile01.CreateCopy(ThisModContext, "MagicMissile26");
            var MagicMissile27 = MagicMissile02.CreateCopy(ThisModContext, "MagicMissile27");
            var MagicMissile28 = MagicMissile03.CreateCopy(ThisModContext, "MagicMissile28");
            var MagicMissile29 = MagicMissile04.CreateCopy(ThisModContext, "MagicMissile29");
            var MagicMissile30 = MagicMissile00.CreateCopy(ThisModContext, "MagicMissile30");
            var MagicMissile31 = MagicMissile01.CreateCopy(ThisModContext, "MagicMissile31");
            var MagicMissile32 = MagicMissile02.CreateCopy(ThisModContext, "MagicMissile32");
            var MagicMissile33 = MagicMissile03.CreateCopy(ThisModContext, "MagicMissile33");
            var MagicMissile34 = MagicMissile04.CreateCopy(ThisModContext, "MagicMissile34");
            var MagicMissile35 = MagicMissile00.CreateCopy(ThisModContext, "MagicMissile35");
            var MagicMissile36 = MagicMissile01.CreateCopy(ThisModContext, "MagicMissile36");
            var MagicMissile37 = MagicMissile02.CreateCopy(ThisModContext, "MagicMissile37");
            var MagicMissile38 = MagicMissile03.CreateCopy(ThisModContext, "MagicMissile38");
            var MagicMissile39 = MagicMissile04.CreateCopy(ThisModContext, "MagicMissile39");
            #endregion
            #region Trajectories
            var MagicMissile05_Trajectory = MagicMissile00_Trajectory.CreateCopy(ThisModContext, "MagicMissile05_Trajectory");
            var MagicMissile06_Trajectory = MagicMissile01_Trajectory.CreateCopy(ThisModContext, "MagicMissile06_Trajectory");
            var MagicMissile07_Trajectory = MagicMissile02_Trajectory.CreateCopy(ThisModContext, "MagicMissile07_Trajectory");
            var MagicMissile08_Trajectory = MagicMissile03_Trajectory.CreateCopy(ThisModContext, "MagicMissile08_Trajectory");
            var MagicMissile09_Trajectory = MagicMissile04_Trajectory.CreateCopy(ThisModContext, "MagicMissile09_Trajectory");
            var MagicMissile10_Trajectory = MagicMissile00_Trajectory.CreateCopy(ThisModContext, "MagicMissile10_Trajectory");
            var MagicMissile11_Trajectory = MagicMissile01_Trajectory.CreateCopy(ThisModContext, "MagicMissile11_Trajectory");
            var MagicMissile12_Trajectory = MagicMissile02_Trajectory.CreateCopy(ThisModContext, "MagicMissile12_Trajectory");
            var MagicMissile13_Trajectory = MagicMissile03_Trajectory.CreateCopy(ThisModContext, "MagicMissile13_Trajectory");
            var MagicMissile14_Trajectory = MagicMissile04_Trajectory.CreateCopy(ThisModContext, "MagicMissile14_Trajectory");
            var MagicMissile15_Trajectory = MagicMissile00_Trajectory.CreateCopy(ThisModContext, "MagicMissile15_Trajectory");
            var MagicMissile16_Trajectory = MagicMissile01_Trajectory.CreateCopy(ThisModContext, "MagicMissile16_Trajectory");
            var MagicMissile17_Trajectory = MagicMissile02_Trajectory.CreateCopy(ThisModContext, "MagicMissile17_Trajectory");
            var MagicMissile18_Trajectory = MagicMissile03_Trajectory.CreateCopy(ThisModContext, "MagicMissile18_Trajectory");
            var MagicMissile19_Trajectory = MagicMissile04_Trajectory.CreateCopy(ThisModContext, "MagicMissile19_Trajectory");
            var MagicMissile20_Trajectory = MagicMissile00_Trajectory.CreateCopy(ThisModContext, "MagicMissile20_Trajectory");
            var MagicMissile21_Trajectory = MagicMissile01_Trajectory.CreateCopy(ThisModContext, "MagicMissile21_Trajectory");
            var MagicMissile22_Trajectory = MagicMissile02_Trajectory.CreateCopy(ThisModContext, "MagicMissile22_Trajectory");
            var MagicMissile23_Trajectory = MagicMissile03_Trajectory.CreateCopy(ThisModContext, "MagicMissile23_Trajectory");
            var MagicMissile24_Trajectory = MagicMissile04_Trajectory.CreateCopy(ThisModContext, "MagicMissile24_Trajectory");
            var MagicMissile25_Trajectory = MagicMissile00_Trajectory.CreateCopy(ThisModContext, "MagicMissile25_Trajectory");
            var MagicMissile26_Trajectory = MagicMissile01_Trajectory.CreateCopy(ThisModContext, "MagicMissile26_Trajectory");
            var MagicMissile27_Trajectory = MagicMissile02_Trajectory.CreateCopy(ThisModContext, "MagicMissile27_Trajectory");
            var MagicMissile28_Trajectory = MagicMissile03_Trajectory.CreateCopy(ThisModContext, "MagicMissile28_Trajectory");
            var MagicMissile29_Trajectory = MagicMissile04_Trajectory.CreateCopy(ThisModContext, "MagicMissile29_Trajectory");
            var MagicMissile30_Trajectory = MagicMissile00_Trajectory.CreateCopy(ThisModContext, "MagicMissile30_Trajectory");
            var MagicMissile31_Trajectory = MagicMissile01_Trajectory.CreateCopy(ThisModContext, "MagicMissile31_Trajectory");
            var MagicMissile32_Trajectory = MagicMissile02_Trajectory.CreateCopy(ThisModContext, "MagicMissile32_Trajectory");
            var MagicMissile33_Trajectory = MagicMissile03_Trajectory.CreateCopy(ThisModContext, "MagicMissile33_Trajectory");
            var MagicMissile34_Trajectory = MagicMissile04_Trajectory.CreateCopy(ThisModContext, "MagicMissile34_Trajectory");
            var MagicMissile35_Trajectory = MagicMissile00_Trajectory.CreateCopy(ThisModContext, "MagicMissile35_Trajectory");
            var MagicMissile36_Trajectory = MagicMissile01_Trajectory.CreateCopy(ThisModContext, "MagicMissile36_Trajectory");
            var MagicMissile37_Trajectory = MagicMissile02_Trajectory.CreateCopy(ThisModContext, "MagicMissile37_Trajectory");
            var MagicMissile38_Trajectory = MagicMissile03_Trajectory.CreateCopy(ThisModContext, "MagicMissile38_Trajectory");
            var MagicMissile39_Trajectory = MagicMissile04_Trajectory.CreateCopy(ThisModContext, "MagicMissile39_Trajectory");
            #endregion
            #region Trajectory Mods
            //MagicMissile05_Trajectory.PlaneOffset[0].Curve.keys[1].time += (1f / 3f);
            //MagicMissile05_Trajectory.PlaneOffset[0].Curve.keys[2].time += (1f / 3f);
            //MagicMissile05_Trajectory.PlaneOffset[0].Curve.keys[3].time += (1f / 3f);
            //MagicMissile05_Trajectory.PlaneOffset[0].Curve.keys[4].time += (1f / 3f);
            MagicMissile05_Trajectory.PlaneOffset[0].AmplitudeScale += 40f;
            MagicMissile05_Trajectory.UpOffset[0].AmplitudeScale += 40f;
            MagicMissile06_Trajectory.PlaneOffset[0].AmplitudeScale += 40f;
            MagicMissile06_Trajectory.UpOffset[0].AmplitudeScale += 40f;
            MagicMissile07_Trajectory.PlaneOffset[0].AmplitudeScale += 40f;
            MagicMissile07_Trajectory.UpOffset[0].AmplitudeScale += 40f;
            MagicMissile08_Trajectory.PlaneOffset[0].AmplitudeScale += 40f;
            MagicMissile08_Trajectory.UpOffset[0].AmplitudeScale += 40f;
            MagicMissile09_Trajectory.PlaneOffset[0].AmplitudeScale += 40f;
            MagicMissile09_Trajectory.UpOffset[0].AmplitudeScale += 40f;

            MagicMissile05_Trajectory.PlaneOffset[0].FrequencyScale = 2f;
            MagicMissile05_Trajectory.UpOffset[0].FrequencyScale = 2f;
            MagicMissile06_Trajectory.PlaneOffset[0].FrequencyScale = 2f;
            MagicMissile06_Trajectory.UpOffset[0].FrequencyScale = 2f;
            MagicMissile07_Trajectory.PlaneOffset[0].FrequencyScale = 2f;
            MagicMissile07_Trajectory.UpOffset[0].FrequencyScale = 2f;
            MagicMissile08_Trajectory.PlaneOffset[0].FrequencyScale = 2f;
            MagicMissile08_Trajectory.UpOffset[0].FrequencyScale = 2f;
            MagicMissile09_Trajectory.PlaneOffset[0].FrequencyScale = 2f;
            MagicMissile09_Trajectory.UpOffset[0].FrequencyScale = 2f;

            MagicMissile10_Trajectory.PlaneOffset[0].AmplitudeScale += 70f;
            MagicMissile10_Trajectory.UpOffset[0].AmplitudeScale += 70f;
            MagicMissile11_Trajectory.PlaneOffset[0].AmplitudeScale += 70f;
            MagicMissile11_Trajectory.UpOffset[0].AmplitudeScale += 70f;
            MagicMissile12_Trajectory.PlaneOffset[0].AmplitudeScale += 70f;
            MagicMissile12_Trajectory.UpOffset[0].AmplitudeScale += 70f;
            MagicMissile13_Trajectory.PlaneOffset[0].AmplitudeScale += 70f;
            MagicMissile13_Trajectory.UpOffset[0].AmplitudeScale += 70f;
            MagicMissile14_Trajectory.PlaneOffset[0].AmplitudeScale += 70f;
            MagicMissile14_Trajectory.UpOffset[0].AmplitudeScale += 70f;

            MagicMissile10_Trajectory.PlaneOffset[0].FrequencyScale = 2.5f;
            MagicMissile10_Trajectory.UpOffset[0].FrequencyScale = 2.5f;
            MagicMissile11_Trajectory.PlaneOffset[0].FrequencyScale = 2.5f;
            MagicMissile11_Trajectory.UpOffset[0].FrequencyScale = 2.5f;
            MagicMissile12_Trajectory.PlaneOffset[0].FrequencyScale = 2.5f;
            MagicMissile12_Trajectory.UpOffset[0].FrequencyScale = 2.5f;
            MagicMissile13_Trajectory.PlaneOffset[0].FrequencyScale = 2.5f;
            MagicMissile13_Trajectory.UpOffset[0].FrequencyScale = 2.5f;
            MagicMissile14_Trajectory.PlaneOffset[0].FrequencyScale = 2.5f;
            MagicMissile14_Trajectory.UpOffset[0].FrequencyScale = 2.5f;

            MagicMissile15_Trajectory.PlaneOffset[0].AmplitudeScale += 100f;
            MagicMissile15_Trajectory.UpOffset[0].AmplitudeScale += 100f;
            MagicMissile16_Trajectory.PlaneOffset[0].AmplitudeScale += 100f;
            MagicMissile16_Trajectory.UpOffset[0].AmplitudeScale += 100f;
            MagicMissile17_Trajectory.PlaneOffset[0].AmplitudeScale += 100f;
            MagicMissile17_Trajectory.UpOffset[0].AmplitudeScale += 100f;
            MagicMissile18_Trajectory.PlaneOffset[0].AmplitudeScale += 100f;
            MagicMissile18_Trajectory.UpOffset[0].AmplitudeScale += 100f;
            MagicMissile19_Trajectory.PlaneOffset[0].AmplitudeScale += 100f;
            MagicMissile19_Trajectory.UpOffset[0].AmplitudeScale += 100f;

            MagicMissile15_Trajectory.PlaneOffset[0].FrequencyScale = 3f;
            MagicMissile15_Trajectory.UpOffset[0].FrequencyScale = 3f;
            MagicMissile16_Trajectory.PlaneOffset[0].FrequencyScale = 3f;
            MagicMissile16_Trajectory.UpOffset[0].FrequencyScale = 3f;
            MagicMissile17_Trajectory.PlaneOffset[0].FrequencyScale = 3f;
            MagicMissile17_Trajectory.UpOffset[0].FrequencyScale = 3f;
            MagicMissile18_Trajectory.PlaneOffset[0].FrequencyScale = 3f;
            MagicMissile18_Trajectory.UpOffset[0].FrequencyScale = 3f;
            MagicMissile19_Trajectory.PlaneOffset[0].FrequencyScale = 3f;
            MagicMissile19_Trajectory.UpOffset[0].FrequencyScale = 3f;

            MagicMissile20_Trajectory.PlaneOffset[0].AmplitudeScale += 25f;
            MagicMissile20_Trajectory.UpOffset[0].AmplitudeScale += 25f;
            MagicMissile21_Trajectory.PlaneOffset[0].AmplitudeScale += 25f;
            MagicMissile21_Trajectory.UpOffset[0].AmplitudeScale += 25f;
            MagicMissile22_Trajectory.PlaneOffset[0].AmplitudeScale += 25f;
            MagicMissile22_Trajectory.UpOffset[0].AmplitudeScale += 25f;
            MagicMissile23_Trajectory.PlaneOffset[0].AmplitudeScale += 25f;
            MagicMissile23_Trajectory.UpOffset[0].AmplitudeScale += 25f;
            MagicMissile24_Trajectory.PlaneOffset[0].AmplitudeScale += 25f;
            MagicMissile24_Trajectory.UpOffset[0].AmplitudeScale += 25f;

            MagicMissile25_Trajectory.PlaneOffset[0].AmplitudeScale += 30f;
            MagicMissile25_Trajectory.UpOffset[0].AmplitudeScale += 30f;
            MagicMissile26_Trajectory.PlaneOffset[0].AmplitudeScale += 30f;
            MagicMissile26_Trajectory.UpOffset[0].AmplitudeScale += 30f;
            MagicMissile27_Trajectory.PlaneOffset[0].AmplitudeScale += 30f;
            MagicMissile27_Trajectory.UpOffset[0].AmplitudeScale += 30f;
            MagicMissile28_Trajectory.PlaneOffset[0].AmplitudeScale += 30f;
            MagicMissile28_Trajectory.UpOffset[0].AmplitudeScale += 30f;
            MagicMissile29_Trajectory.PlaneOffset[0].AmplitudeScale += 30f;
            MagicMissile29_Trajectory.UpOffset[0].AmplitudeScale += 30f;

            MagicMissile30_Trajectory.PlaneOffset[0].AmplitudeScale += 35f;
            MagicMissile30_Trajectory.UpOffset[0].AmplitudeScale += 35f;
            MagicMissile31_Trajectory.PlaneOffset[0].AmplitudeScale += 35f;
            MagicMissile31_Trajectory.UpOffset[0].AmplitudeScale += 35f;
            MagicMissile32_Trajectory.PlaneOffset[0].AmplitudeScale += 35f;
            MagicMissile32_Trajectory.UpOffset[0].AmplitudeScale += 35f;
            MagicMissile33_Trajectory.PlaneOffset[0].AmplitudeScale += 35f;
            MagicMissile33_Trajectory.UpOffset[0].AmplitudeScale += 35f;
            MagicMissile34_Trajectory.PlaneOffset[0].AmplitudeScale += 35f;
            MagicMissile34_Trajectory.UpOffset[0].AmplitudeScale += 35f;

            MagicMissile35_Trajectory.PlaneOffset[0].AmplitudeScale += 40f;
            MagicMissile35_Trajectory.UpOffset[0].AmplitudeScale += 40f;
            MagicMissile36_Trajectory.PlaneOffset[0].AmplitudeScale += 40f;
            MagicMissile36_Trajectory.UpOffset[0].AmplitudeScale += 40f;
            MagicMissile37_Trajectory.PlaneOffset[0].AmplitudeScale += 40f;
            MagicMissile37_Trajectory.UpOffset[0].AmplitudeScale += 40f;
            MagicMissile38_Trajectory.PlaneOffset[0].AmplitudeScale += 40f;
            MagicMissile38_Trajectory.UpOffset[0].AmplitudeScale += 40f;
            MagicMissile39_Trajectory.PlaneOffset[0].AmplitudeScale += 40f;
            MagicMissile39_Trajectory.UpOffset[0].AmplitudeScale += 40f;
            #endregion
            #region Add Mods
            MagicMissile05.m_Trajectory = MagicMissile05_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile06.m_Trajectory = MagicMissile06_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile07.m_Trajectory = MagicMissile07_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile08.m_Trajectory = MagicMissile08_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile09.m_Trajectory = MagicMissile09_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile10.m_Trajectory = MagicMissile10_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile11.m_Trajectory = MagicMissile11_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile12.m_Trajectory = MagicMissile12_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile13.m_Trajectory = MagicMissile13_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile14.m_Trajectory = MagicMissile14_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile15.m_Trajectory = MagicMissile15_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile16.m_Trajectory = MagicMissile16_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile17.m_Trajectory = MagicMissile17_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile18.m_Trajectory = MagicMissile18_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile19.m_Trajectory = MagicMissile19_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile20.m_Trajectory = MagicMissile20_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile21.m_Trajectory = MagicMissile21_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile22.m_Trajectory = MagicMissile22_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile23.m_Trajectory = MagicMissile23_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile24.m_Trajectory = MagicMissile24_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile25.m_Trajectory = MagicMissile25_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile26.m_Trajectory = MagicMissile26_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile27.m_Trajectory = MagicMissile27_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile28.m_Trajectory = MagicMissile28_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile29.m_Trajectory = MagicMissile29_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile30.m_Trajectory = MagicMissile30_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile31.m_Trajectory = MagicMissile31_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile32.m_Trajectory = MagicMissile32_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile33.m_Trajectory = MagicMissile33_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile34.m_Trajectory = MagicMissile34_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile35.m_Trajectory = MagicMissile35_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile36.m_Trajectory = MagicMissile36_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile37.m_Trajectory = MagicMissile37_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile38.m_Trajectory = MagicMissile38_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            MagicMissile39.m_Trajectory = MagicMissile39_Trajectory.ToReference<BlueprintProjectileTrajectoryReference>();
            //MagicMissile05.CastFx = null;
            //MagicMissile05.CastEffectDuration = 1f;
            //MagicMissile10.CastFx = null;
            //MagicMissile10.CastEffectDuration = 1f;
            //MagicMissile15.CastFx = null;
            //MagicMissile15.CastEffectDuration = 1f;
            //MagicMissile20.CastFx = null;
            //MagicMissile20.CastEffectDuration = 1f;
            //MagicMissile25.CastFx = null;
            //MagicMissile25.CastEffectDuration = 1f;
            //MagicMissile30.CastFx = null;
            //MagicMissile30.CastEffectDuration = 1f;
            //MagicMissile35.CastFx = null;
            //MagicMissile35.CastEffectDuration = 1f;
            //MagicMissile05.Speed += 10f;
            //MagicMissile06.Speed += 10f;
            //MagicMissile07.Speed += 10f;
            //MagicMissile08.Speed += 10f;
            //MagicMissile09.Speed += 10f;
            //MagicMissile10.Speed += 15f;
            //MagicMissile11.Speed += 15f;
            //MagicMissile12.Speed += 15f;
            //MagicMissile13.Speed += 15f;
            //MagicMissile14.Speed += 15f;
            //MagicMissile15.Speed += 20f;
            //MagicMissile16.Speed += 20f;
            //MagicMissile17.Speed += 20f;
            //MagicMissile18.Speed += 20f;
            //MagicMissile19.Speed += 20f;
            //MagicMissile20.Speed += 25f;
            //MagicMissile21.Speed += 25f;
            //MagicMissile22.Speed += 25f;
            //MagicMissile23.Speed += 25f;
            //MagicMissile24.Speed += 25f;
            //MagicMissile25.Speed += 30f;
            //MagicMissile26.Speed += 30f;
            //MagicMissile27.Speed += 30f;
            //MagicMissile28.Speed += 30f;
            //MagicMissile29.Speed += 30f;
            //MagicMissile30.Speed += 35f;
            //MagicMissile31.Speed += 35f;
            //MagicMissile32.Speed += 35f;
            //MagicMissile33.Speed += 35f;
            //MagicMissile34.Speed += 35f;
            //MagicMissile35.Speed += 40f;
            //MagicMissile36.Speed += 40f;
            //MagicMissile37.Speed += 40f;
            //MagicMissile38.Speed += 40f;
            //MagicMissile39.Speed += 40f;
            #endregion

            #region Projectiles
            var MagicMissile00Ref = MagicMissile00.ToReference<BlueprintProjectileReference>();
            var MagicMissile01Ref = MagicMissile01.ToReference<BlueprintProjectileReference>();
            var MagicMissile02Ref = MagicMissile02.ToReference<BlueprintProjectileReference>();
            var MagicMissile03Ref = MagicMissile03.ToReference<BlueprintProjectileReference>();
            var MagicMissile04Ref = MagicMissile04.ToReference<BlueprintProjectileReference>();
            var MagicMissile05Ref = MagicMissile05.ToReference<BlueprintProjectileReference>();
            var MagicMissile06Ref = MagicMissile06.ToReference<BlueprintProjectileReference>();
            var MagicMissile07Ref = MagicMissile07.ToReference<BlueprintProjectileReference>();
            var MagicMissile08Ref = MagicMissile08.ToReference<BlueprintProjectileReference>();
            var MagicMissile09Ref = MagicMissile09.ToReference<BlueprintProjectileReference>();
            var MagicMissile10Ref = MagicMissile10.ToReference<BlueprintProjectileReference>();
            var MagicMissile11Ref = MagicMissile11.ToReference<BlueprintProjectileReference>();
            var MagicMissile12Ref = MagicMissile12.ToReference<BlueprintProjectileReference>();
            var MagicMissile13Ref = MagicMissile13.ToReference<BlueprintProjectileReference>();
            var MagicMissile14Ref = MagicMissile14.ToReference<BlueprintProjectileReference>();
            var MagicMissile15Ref = MagicMissile15.ToReference<BlueprintProjectileReference>();
            var MagicMissile16Ref = MagicMissile16.ToReference<BlueprintProjectileReference>();
            var MagicMissile17Ref = MagicMissile17.ToReference<BlueprintProjectileReference>();
            var MagicMissile18Ref = MagicMissile18.ToReference<BlueprintProjectileReference>();
            var MagicMissile19Ref = MagicMissile19.ToReference<BlueprintProjectileReference>();
            var MagicMissile20Ref = MagicMissile20.ToReference<BlueprintProjectileReference>();
            var MagicMissile21Ref = MagicMissile21.ToReference<BlueprintProjectileReference>();
            var MagicMissile22Ref = MagicMissile22.ToReference<BlueprintProjectileReference>();
            var MagicMissile23Ref = MagicMissile23.ToReference<BlueprintProjectileReference>();
            var MagicMissile24Ref = MagicMissile24.ToReference<BlueprintProjectileReference>();
            var MagicMissile25Ref = MagicMissile25.ToReference<BlueprintProjectileReference>();
            var MagicMissile26Ref = MagicMissile26.ToReference<BlueprintProjectileReference>();
            var MagicMissile27Ref = MagicMissile27.ToReference<BlueprintProjectileReference>();
            var MagicMissile28Ref = MagicMissile28.ToReference<BlueprintProjectileReference>();
            var MagicMissile29Ref = MagicMissile29.ToReference<BlueprintProjectileReference>();
            var MagicMissile30Ref = MagicMissile30.ToReference<BlueprintProjectileReference>();
            var MagicMissile31Ref = MagicMissile31.ToReference<BlueprintProjectileReference>();
            var MagicMissile32Ref = MagicMissile32.ToReference<BlueprintProjectileReference>();
            var MagicMissile33Ref = MagicMissile33.ToReference<BlueprintProjectileReference>();
            var MagicMissile34Ref = MagicMissile34.ToReference<BlueprintProjectileReference>();
            var MagicMissile35Ref = MagicMissile35.ToReference<BlueprintProjectileReference>();
            var MagicMissile36Ref = MagicMissile36.ToReference<BlueprintProjectileReference>();
            var MagicMissile37Ref = MagicMissile37.ToReference<BlueprintProjectileReference>();
            var MagicMissile38Ref = MagicMissile38.ToReference<BlueprintProjectileReference>();
            var MagicMissile39Ref = MagicMissile39.ToReference<BlueprintProjectileReference>();
            #endregion

            string SpellName = "MagicMissileMastered";
            string SpellDisplay = "Magic Missile, Mastered";
            string SpellDesc = "This spell functions like greater magic missile, except it has no maximum to how many additional missiles you may shoot" +
                "(six missiles at 11th level, seven missiles at 13th level, eight missiles at 15th level, nine missiles at 17th level, ten missiles at 19th level, etc).";
            var icon = AssetLoader.LoadInternal(ThisModContext, folder: "Spells", file: $"Icon_{SpellName}.png");
            var ScrollIcon = BlueprintTools.GetBlueprint<BlueprintItemEquipmentUsable>("63caf94a780472b448f50d0bc183c38f").Icon; //ScrollOfMagicMissile.Icon





            var Spell = SpellCopy.CreateCopy(ThisModContext, SpellName, bp =>
            {
                bp.m_Icon = icon;
                bp.SetNameDescription(ThisModContext, SpellDisplay, SpellDesc);
                bp.RemoveComponents<SpellListComponent>();
                bp.RemoveComponents<RecommendationNoFeatFromGroup>();

                bp.GetComponent<AbilityEffectRunAction>()
                    .Actions.Actions
                    .OfType<ContextActionDealDamage>().FirstOrDefault()
                    .Value.DiceType = DiceType.D6;
                bp.GetComponent<AbilityEffectRunAction>()
                    .Actions.Actions
                    .OfType<ContextActionDealDamage>().FirstOrDefault()
                    .Value.BonusValue.Value = 0;
                bp.GetComponent<AbilityEffectRunAction>()
                    .Actions.Actions
                    .OfType<ContextActionDealDamage>().FirstOrDefault()
                    .Value.BonusValue.ValueType = ContextValueType.AbilityParameter;
                bp.GetComponent<AbilityEffectRunAction>()
                    .Actions.Actions
                    .OfType<ContextActionDealDamage>().FirstOrDefault()
                    .Value.BonusValue.m_AbilityParameter = AbilityParameterType.CasterStatBonus;
                bp.GetComponent<ContextRankConfig>().m_UseMax = false;
                bp.GetComponent<ContextRankConfig>().m_Max = 0;
                bp.GetComponent<AbilityDeliverProjectile>().m_Projectiles = new BlueprintProjectileReference[]
                {
                    MagicMissile00Ref,MagicMissile01Ref,MagicMissile02Ref,MagicMissile03Ref,MagicMissile04Ref,
                    MagicMissile05Ref,MagicMissile06Ref,MagicMissile07Ref,MagicMissile08Ref,MagicMissile09Ref,
                    MagicMissile10Ref,MagicMissile11Ref,MagicMissile12Ref,MagicMissile13Ref,MagicMissile14Ref,
                    MagicMissile15Ref,MagicMissile16Ref,MagicMissile17Ref,MagicMissile18Ref,MagicMissile19Ref,
                    MagicMissile20Ref,MagicMissile21Ref,MagicMissile22Ref,MagicMissile23Ref,MagicMissile24Ref,
                    MagicMissile25Ref,MagicMissile26Ref,MagicMissile27Ref,MagicMissile28Ref,MagicMissile29Ref,
                    MagicMissile30Ref,MagicMissile31Ref,MagicMissile32Ref,MagicMissile33Ref,MagicMissile34Ref,
                    MagicMissile35Ref,MagicMissile36Ref,MagicMissile37Ref,MagicMissile38Ref,MagicMissile39Ref
                };
            });

            //Need to add shield spell missile block. 9c0fa9b438ada3f43864be8dd8b3e741 //MageShieldBuff
            //384ed9a25d1d79c47b9bbfd31309f00e //ForceShieldFeature


            if (ThisModContext.ThirdParty.Spells.IsDisabled("MagicMissileMastered")) { return; }
            Spell.AddToSpellList(SpellTools.SpellList.BloodragerSpellList, 5);
            Spell.AddToSpellList(SpellTools.SpellList.MagusSpellList, 5);
            Spell.AddToSpellList(SpellTools.SpellList.WizardSpellList, 5);
            var Scroll = Utilities.ItemTools.CreateScroll(ThisModContext, Spell, ScrollIcon);
            VenderTools.AddScrollToLeveledVenders(Scroll, 2);
            //Utilities.ItemTools.AddToVendor(ThisModContext, Scroll, 2, BlueprintSharedVendorTables.WarCamp_ScrollVendorClericTable);
            //Utilities.ItemTools.AddToVendor(ThisModContext, Scroll, 3, BlueprintSharedVendorTables.Scroll_Chapter3VendorTable);
            //Utilities.ItemTools.AddToVendor(ThisModContext, Scroll, 4, BlueprintSharedVendorTables.Scroll_Chapter5VendorTable);
        }
    }
}
