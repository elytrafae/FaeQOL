using FaeQOL.Content.Items.ClassOaths;
using FaeQOL.Systems;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace FaeQOL.CrossMod {

    /*
    [ExtendsFromMod("ThoriumMod")]
    [JITWhenModsEnabled("ThoriumMod")]
    internal class ThoriumCompatibility : AbstractCrossModCompat {


        public override string ModName => "ThoriumMod";

        public override List<string> PermanentStayBuffs => ["AltarBuff", "ConductorsStandBuff", "NinjaBuff"];

        public override List<string> KeychainKeys => ["AquaticDepthsBiomeKey", "DesertBiomeKey", "UnderworldBiomeKey"];

        public override Dictionary<string, Func<Player, Item, bool>> PermanentBuffItemConditions => new() {
                {"InspirationCrystalNew", InspirationConsumedAll},
                {"InspirationFragment", InspirationConsumedAll},
                {"InspirationShard", InspirationConsumedAll},
                {"InspirationGem", InspirationGemConsumed},
                {"CrystalWave", CrystalWavesConsumed},
                {"AstralWave", AstralWaveConsumed}
            };


        [JITWhenModsEnabled("ThoriumMod")]
        public override void CrossCompatAddOaths(Mod mod, Mod myMod) {
            if (mod.TryFind("BardDamage", out DamageClass bard)) {
                myMod.AddContent(new FilledOath(bard, "Bard", new(255, 126, 112), []));
            }
            if (mod.TryFind("HealerDamage", out DamageClass healer)) {
                myMod.AddContent(new FilledOath(healer, "Healer", new(222, 151, 0), []));
            }
        }

        [JITWhenModsEnabled("ThoriumMod")]
        private static bool InspirationConsumedAll(Player player, Item item) {
            if (!ModLoader.HasMod("ThoriumMod")) {
                return true;
            }
            return !ThoriumMod.Items.BardItems.InspirationConsumableBase.CanBeUsed((ThoriumMod.Items.BardItems.InspirationConsumableBase)item.ModItem, player);
        }

        [JITWhenModsEnabled("ThoriumMod")]
        private static bool InspirationGemConsumed(Player player, Item item) {
            if (!ModLoader.HasMod("ThoriumMod")) {
                return true;
            }
            return Main.LocalPlayer.GetModPlayer<ThoriumMod.ThoriumPlayer>().consumedInspirationGem;
        }

        [JITWhenModsEnabled("ThoriumMod")]
        private static bool CrystalWavesConsumed(Player player, Item item) {
            if (!ModLoader.HasMod("ThoriumMod")) {
                return true;
            }
            return Main.LocalPlayer.GetModPlayer<ThoriumMod.ThoriumPlayer>().consumedCrystalWaveCount >= 5;
        }

        [JITWhenModsEnabled("ThoriumMod")]
        private static bool AstralWaveConsumed(Player player, Item item) {
            if (!ModLoader.HasMod("ThoriumMod")) {
                return true;
            }
            return Main.LocalPlayer.GetModPlayer<ThoriumMod.ThoriumPlayer>().consumedAstralWave;
        }

        [JITWhenModsEnabled("ThoriumMod")]
        public override void CrossCompatAddILAndDetours(Mod mod) {
            IL.ThoriumMod.Tiles.ChestTileBase.RightClick += ChestTileBase_RightClick;
        }

        [JITWhenModsEnabled("ThoriumMod")]
        private void ChestTileBase_RightClick(MonoMod.Cil.ILContext il) {
            try {
                ILCursor c = new ILCursor(il);

                // Remove the simple "HasItem" call and replace it with our own!
                c.GotoNext(i => i.MatchCallvirt<Player>("HasItemInInventoryOrOpenVoidBag"));
                c.Remove();
                c.EmitDelegate(HasItemInKeychainInventoryOrVoidBag);


                c.GotoNext(i => i.MatchCallvirt<Player>("ConsumeItem"));
                c.Remove();
                c.EmitDelegate(ConsumeItemFromKeychainInventoryOrVoidBag);

            } catch (Exception e) {
                MonoModHooks.DumpIL(ModContent.GetInstance<FaeQOL>(), il);
            }
        }

        [JITWhenModsEnabled("ThoriumMod")]
        public static bool HasItemInKeychainInventoryOrVoidBag(Player player, int itemType) { 
            return Utilities.SearchForKeyInKeychains(player, itemType) != null || player.HasItemInInventoryOrOpenVoidBag(itemType);
        }

        [JITWhenModsEnabled("ThoriumMod")]
        public static bool ConsumeItemFromKeychainInventoryOrVoidBag(Player player, int itemType, bool reverse, bool bag) {
            Item itemFromKeychains = Utilities.SearchForKeyInKeychains(player, itemType);
            if (itemFromKeychains != null) {
                if (ItemLoader.ConsumeItem(itemFromKeychains, player)) {
                    itemFromKeychains.stack--;
                }
                if (itemFromKeychains.stack <= 0) {
                    itemFromKeychains.TurnToAir();
                }
                return true;
            }
            return player.ConsumeItem(itemType, reverse, bag);
        }


    }
    */
}
