using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace FaeQOL.Systems {
    internal class EventDisableSystem : ModSystem {

        public static bool isBloodMoonDisabled = false;
        public static bool isGoblinArmyDisabled = false;
        public static bool isSolarEclipseDisabled = false;

        private const string BLOOD_MOON_TAG = "BloodMoonDisabled";
        private const string GOBLIN_ARMY_TAG = "BloodMoonDisabled";
        private const string SOLAR_ECLIPSE_TAG = "BloodMoonDisabled";

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

    }
}
