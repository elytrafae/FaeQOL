using FaeQOL.Systems;
using FaeQOL.Systems.LowLevelTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Utilities;

namespace FaeQOL.CrossMod {
    [JITWhenModsEnabled("ThoriumMod")]
    internal class ThoriumCompatibility : AbstractCrossModCompat {
        public override string ModName => "ThoriumMod";

        public override List<string> PermanentStayBuffs => ["AltarBuff", "ConductorsStandBuff", "NinjaBuff"];

        /*
        public override Dictionary<string, Func<Player, Item, bool>> PermanentBuffItemConditions => new() {
                {"InspirationCrystalNew", InspirationConsumedAll},
                {"InspirationFragment", InspirationConsumedAll},
                {"InspirationShard", InspirationConsumedAll},
                {"InspirationGem", InspirationGemConsumed},
                {"CrystalWave", CrystalWavesConsumed},
                {"AstralWave", AstralWaveConsumed}
            };


        private static bool InspirationConsumedAll(Player player, Item item) {
            if (!ModLoader.HasMod("ThoriumMod")) {
                return true;
            }
            return !InspirationCanUseInner(player, item);
        }

        [JITWhenModsEnabled("ThoriumMod")]
        private static bool InspirationCanUseInner(Player player, Item item) {
            return InspirationConsumableBase.CanBeUsed((InspirationConsumableBase)item.ModItem, player);
        }

        private static bool InspirationGemConsumed(Player player, Item item) {
            if (!ModLoader.HasMod("ThoriumMod")) {
                return true;
            }
            return InspirationGemConsumedInner;
        }

        [JITWhenModsEnabled("ThoriumMod")]
        private static bool InspirationGemConsumedInner => Main.LocalPlayer.GetThoriumPlayer().consumedInspirationGem;

        private static bool CrystalWavesConsumed(Player player, Item item) {
            if (!ModLoader.HasMod("ThoriumMod")) {
                return true;
            }
            return CrystalWavesConsumedInner;
        }

        [JITWhenModsEnabled("ThoriumMod")]
        private static bool CrystalWavesConsumedInner => Main.LocalPlayer.GetThoriumPlayer().consumedCrystalWaveCount >= 5;

        private static bool AstralWaveConsumed(Player player, Item item) {
            if (!ModLoader.HasMod("ThoriumMod")) {
                return true;
            }
            return AstralWaveConsumedInner;
        }

        [JITWhenModsEnabled("ThoriumMod")]
        private static bool AstralWaveConsumedInner => Main.LocalPlayer.GetThoriumPlayer().consumedAstralWave;
        */

        // TODO: Add Permanent Boosts https://thoriummod.wiki.gg/wiki/Permanent_boosts
        // TODO: Add Key support

    }
}
