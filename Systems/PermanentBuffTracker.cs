using FaeQOL.Systems.Config;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FaeQOL.Systems {
    internal class PermanentBuffTracker : GlobalItem {

        public readonly static Dictionary<int, Func<Player, Item, bool>> ItemConsumedConditions = new Dictionary<int, Func<Player, Item, bool>>();

        public override void SetStaticDefaults() {
            ItemConsumedConditions.Add(ItemID.LifeCrystal, (player, item) => { return player.ConsumedLifeCrystals >= Player.LifeCrystalMax; });
            ItemConsumedConditions.Add(ItemID.ManaCrystal, (player, item) => { return player.ConsumedManaCrystals >= Player.ManaCrystalMax; });
            ItemConsumedConditions.Add(ItemID.LifeFruit, (player, item) => { return player.ConsumedLifeFruit >= Player.LifeFruitMax; });
            ItemConsumedConditions.Add(ItemID.DemonHeart, (player, item) => { return player.CanDemonHeartAccessoryBeShown(); });
            ItemConsumedConditions.Add(ItemID.TorchGodsFavor, (player, item) => { return player.unlockedBiomeTorches; });
            ItemConsumedConditions.Add(ItemID.CombatBook, (player, item) => { return NPC.combatBookWasUsed; });
            ItemConsumedConditions.Add(ItemID.CombatBookVolumeTwo, (player, item) => { return NPC.combatBookVolumeTwoWasUsed; });
            ItemConsumedConditions.Add(ItemID.MinecartPowerup, (player, item) => { return player.unlockedSuperCart; });
            ItemConsumedConditions.Add(ItemID.AegisCrystal, (player, item) => { return player.usedAegisCrystal; });
            ItemConsumedConditions.Add(ItemID.AegisFruit, (player, item) => { return player.usedAegisFruit; });
            ItemConsumedConditions.Add(ItemID.ArcaneCrystal, (player, item) => { return player.usedArcaneCrystal; });
            ItemConsumedConditions.Add(ItemID.Ambrosia, (player, item) => { return player.usedAmbrosia; });
            ItemConsumedConditions.Add(ItemID.GummyWorm, (player, item) => { return player.usedGummyWorm; });
            ItemConsumedConditions.Add(ItemID.GalaxyPearl, (player, item) => { return player.usedGalaxyPearl; });
            ItemConsumedConditions.Add(ItemID.PeddlersSatchel, (player, item) => { return NPC.peddlersSatchelWasUsed; });
            ItemConsumedConditions.Add(ItemID.ArtisanLoaf, (player, item) => { return player.ateArtisanBread; });

            //ModLoader.GetMod("FaeBadLuck").Call("RegisterPermanentBuffItem", ItemID.ArtisanLoaf, (Player player) => { return player.ateArtisanBread; });
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            if (ModContent.GetInstance<ClientConfig>().PermanentBuffTracker) {
                if (ItemConsumedConditions.TryGetValue(item.type, out Func<Player, Item, bool> condition)) {
                    if (condition.Invoke(Main.player[Main.myPlayer], item)) {
                        tooltips.Add(new TooltipLine(this.Mod, "already_consumed", Language.GetTextValue("Mods.FaeQOL.ItemAlreadConsumed")));
                    }
                }
            }
        }

    }
}
