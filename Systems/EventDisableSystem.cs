using FaeQOL.Systems.Config;
using MonoMod.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace FaeQOL.Systems {
    internal class EventDisableSystem : ModSystem {

        public static bool isBloodMoonDisabled = false;
        public static bool isGoblinArmyDisabled = false;
        public static bool isSolarEclipseDisabled = false;
        // TODO: *Maybe* add the pirate invasion here, too . . . ?

        private const string BLOOD_MOON_TAG = "BloodMoonDisabled";
        private const string GOBLIN_ARMY_TAG = "GoblinArmyDisabled";
        private const string SOLAR_ECLIPSE_TAG = "SolarEclipseDisabled";

        public override void LoadWorldData(TagCompound tag) {
            LoadOrDefault(tag, BLOOD_MOON_TAG, ref isBloodMoonDisabled, false);
            LoadOrDefault(tag, GOBLIN_ARMY_TAG, ref isGoblinArmyDisabled, false);
            LoadOrDefault(tag, SOLAR_ECLIPSE_TAG, ref isSolarEclipseDisabled, false);
        }

        public override void SaveWorldData(TagCompound tag) {
            tag.Add(BLOOD_MOON_TAG, isBloodMoonDisabled);
            tag.Add(GOBLIN_ARMY_TAG, isGoblinArmyDisabled);
            tag.Add(SOLAR_ECLIPSE_TAG, isSolarEclipseDisabled);
        }

        public override void NetSend(BinaryWriter writer) {
            writer.Write(isBloodMoonDisabled);
            writer.Write(isGoblinArmyDisabled);
            writer.Write(isSolarEclipseDisabled);
        }

        public override void NetReceive(BinaryReader reader) {
            isBloodMoonDisabled = reader.ReadBoolean();
            isGoblinArmyDisabled = reader.ReadBoolean();
            isSolarEclipseDisabled = reader.ReadBoolean();
        }

        private static void LoadOrDefault<T>(TagCompound tag, string key, ref T variable, T defaultValue) {
            if (tag.TryGet(key, out T outVal)) {
                variable = outVal;
            } else {
                variable = defaultValue;
            }
        }

        // These Il edits are only made if the config is on!
        public override void Load() {
            if (ModContent.GetInstance<ServerConfig>().EnableEventDisablers) {
                //On_Main.StartInvasion += On_Main_StartInvasion;
                IL_Main.UpdateTime_StartNight += IL_Main_UpdateTime_StartNight;
                IL_Main.UpdateTime_StartDay += IL_Main_UpdateTime_StartDay;
            }
        }

        public override void Unload() {
            //On_Main.StartInvasion -= On_Main_StartInvasion;
            IL_Main.UpdateTime_StartNight -= IL_Main_UpdateTime_StartNight;
            IL_Main.UpdateTime_StartDay -= IL_Main_UpdateTime_StartDay;
        }

        /*
        private void On_Main_StartInvasion(On_Main.orig_StartInvasion orig, int type) {
            bool shouldStart = true;
            if (type == InvasionID.GoblinArmy) {
                shouldStart = !isGoblinArmyDisabled;
            }
            if (shouldStart) {
                orig(type); // TODO: Somehow make it so that the item can still trigger the event just fine.
            }
        }
        */

        private void IL_Main_UpdateTime_StartNight(ILContext il) {
            try {
                var c = new ILCursor(il);
                c.GotoNext(i => i.MatchLdsfld<Main>("bloodMoon"));
                c.Index++;

                // If blood moons are disabled, we turn the blood moon off and replace the boolean for the if condition with false
                c.EmitDelegate((bool isBloodMoon) => {
                    if (isBloodMoonDisabled) {
                        Main.bloodMoon = false;
                        return false;
                    }
                    return isBloodMoon; 
                });
            } catch (Exception e) {
                MonoModHooks.DumpIL(ModContent.GetInstance<FaeQOL>(), il);
            }
        }

        private void IL_Main_UpdateTime_StartDay(ILContext il) {
            try {
                var c = new ILCursor(il);
                // "Hey, this isn't the solar eclipse!!!" No, but the solar eclipse is the
                // only thing checking for this flag in this method, so I am gonna use it so
                // that the achievment for the solar eclipse does not trigger erroniously
                c.GotoNext(i => i.MatchLdsfld<NPC>("downedMechBossAny"));
                c.Index++;

                // Replaces the read boolean with the adjusted value!
                c.EmitDelegate((bool isEclipse) => { return isEclipse && !isSolarEclipseDisabled; });

                // Now, the Goblin Army!
                // We will check the Shadow Orb smashed condition for this one
                c.GotoNext(i => i.MatchLdsfld<WorldGen>("shadowOrbSmashed"));
                c.Index++;

                // Replaces the read boolean with the adjusted value!
                c.EmitDelegate((bool isGoblinArmy) => { return isGoblinArmy && !isGoblinArmyDisabled; });
            } catch (Exception e) {
                MonoModHooks.DumpIL(ModContent.GetInstance<FaeQOL>(), il);
            }
        }


    }
}
