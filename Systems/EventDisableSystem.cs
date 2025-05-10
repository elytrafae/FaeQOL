using FaeQOL.Systems.Config;
using MonoMod.Cil;
using Steamworks;
using Stubble.Core.Classes;
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

        public class EventDisabledData {
            public readonly string Key;
            public bool isDisabled = false;
            public EventDisabledData(string key) {
                Key = key;
            }
        }

        public static readonly EventDisabledData bloodMoon = new("BloodMoonDisabled");
        public static readonly EventDisabledData goblinArmy = new("GoblinArmyDisabled");
        public static readonly EventDisabledData solarEclipse = new("SolarEclipseDisabled");
        public static readonly EventDisabledData pirateInvasion = new("PirateInvasionDisabled");
        public static readonly EventDisabledData slimeRain = new("SlimeRainDisabled");

        public static List<EventDisabledData> eventDisabledDatas = [bloodMoon, goblinArmy, solarEclipse, pirateInvasion, slimeRain];

        public override void LoadWorldData(TagCompound tag) {
            foreach (EventDisabledData data in eventDisabledDatas) {
                LoadOrDefault(tag, data.Key, ref data.isDisabled, false);
            }
        }

        public override void SaveWorldData(TagCompound tag) {
            foreach (EventDisabledData data in eventDisabledDatas) {
                tag.Add(data.Key, data.isDisabled);
            }
        }

        public override void NetSend(BinaryWriter writer) {
            foreach (EventDisabledData data in eventDisabledDatas) {
                writer.Write(data.isDisabled);
            }
        }

        public override void NetReceive(BinaryReader reader) {
            foreach (EventDisabledData data in eventDisabledDatas) {
                data.isDisabled = reader.ReadBoolean();
            }
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
                IL_Main.UpdateTime += IL_Main_UpdateTime;
            }
        }

        public override void Unload() {
            //On_Main.StartInvasion -= On_Main_StartInvasion;
            IL_Main.UpdateTime_StartNight -= IL_Main_UpdateTime_StartNight;
            IL_Main.UpdateTime_StartDay -= IL_Main_UpdateTime_StartDay;
            IL_Main.UpdateTime -= IL_Main_UpdateTime;
        }

        private void IL_Main_UpdateTime_StartNight(ILContext il) {
            try {
                var c = new ILCursor(il);
                c.GotoNext(i => i.MatchLdsfld<Main>("bloodMoon"));
                c.Index++;

                // If blood moons are disabled, we turn the blood moon off and replace the boolean for the if condition with false
                c.EmitDelegate((bool isBloodMoon) => {
                    if (bloodMoon.isDisabled) {
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
                c.EmitDelegate((bool isEclipse) => { return isEclipse && !solarEclipse.isDisabled; });

                // Now, the Goblin Army!
                // We will check the Shadow Orb smashed condition for this one
                c.GotoNext(i => i.MatchLdsfld<WorldGen>("shadowOrbSmashed"));
                c.Index++;

                // Replaces the read boolean with the adjusted value!
                c.EmitDelegate((bool isGoblinArmy) => { return isGoblinArmy && !goblinArmy.isDisabled; });

                // Now, the Pirate Invasion!
                // We will check the Altars Destroyed condition for this one
                c.GotoNext(i => i.MatchLdsfld<WorldGen>("altarCount"));
                c.Index++;

                // Replaces the read int with the adjusted value!
                c.EmitDelegate((int altarsDestroyed) => { return pirateInvasion.isDisabled ? 0 : altarsDestroyed; });
            } catch (Exception e) {
                MonoModHooks.DumpIL(ModContent.GetInstance<FaeQOL>(), il);
            }
        }

        private void IL_Main_UpdateTime(ILContext il) {
            try {
                var c = new ILCursor(il);
                // Slime rain
                // First thing that checks the day time in this method is the slime rain condition
                c.GotoNext(i => i.MatchLdsfld<Main>("dayTime"));
                c.Index++;

                // Replaces the read boolean with the adjusted value!
                c.EmitDelegate((bool isDay) => { return isDay && !slimeRain.isDisabled; });

            } catch (Exception e) {
                MonoModHooks.DumpIL(ModContent.GetInstance<FaeQOL>(), il);
            }
        }


    }
}
