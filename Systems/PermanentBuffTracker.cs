using FaeQOL.Systems.Config;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FaeQOL.Systems {
    internal class PermanentBuffTracker : GlobalItem {

        public readonly static Dictionary<int, Func<Player, bool>> ItemConsumedConditions = new Dictionary<int, Func<Player, bool>>();

        public override void SetStaticDefaults() {
            ItemConsumedConditions.Add(ItemID.LifeCrystal, (player) => { return player.ConsumedLifeCrystals >= Player.LifeCrystalMax; });
            ItemConsumedConditions.Add(ItemID.ManaCrystal, (player) => { return player.ConsumedManaCrystals >= Player.ManaCrystalMax; });
            ItemConsumedConditions.Add(ItemID.LifeFruit, (player) => { return player.ConsumedLifeFruit >= Player.LifeFruitMax; });
            ItemConsumedConditions.Add(ItemID.DemonHeart, (player) => { return player.CanDemonHeartAccessoryBeShown(); });
            ItemConsumedConditions.Add(ItemID.TorchGodsFavor, (player) => { return player.unlockedBiomeTorches; });
            ItemConsumedConditions.Add(ItemID.CombatBook, (player) => { return NPC.combatBookWasUsed; });
            ItemConsumedConditions.Add(ItemID.CombatBookVolumeTwo, (player) => { return NPC.combatBookVolumeTwoWasUsed; });
            ItemConsumedConditions.Add(ItemID.MinecartPowerup, (player) => { return player.unlockedSuperCart; });
            ItemConsumedConditions.Add(ItemID.AegisCrystal, (player) => { return player.usedAegisCrystal; });
            ItemConsumedConditions.Add(ItemID.AegisFruit, (player) => { return player.usedAegisFruit; });
            ItemConsumedConditions.Add(ItemID.ArcaneCrystal, (player) => { return player.usedArcaneCrystal; });
            ItemConsumedConditions.Add(ItemID.Ambrosia, (player) => { return player.usedAmbrosia; });
            ItemConsumedConditions.Add(ItemID.GummyWorm, (player) => { return player.usedGummyWorm; });
            ItemConsumedConditions.Add(ItemID.GalaxyPearl, (player) => { return player.usedGalaxyPearl; });
            ItemConsumedConditions.Add(ItemID.PeddlersSatchel, (player) => { return NPC.peddlersSatchelWasUsed; });
            ItemConsumedConditions.Add(ItemID.ArtisanLoaf, (player) => { return player.ateArtisanBread; });

            //ModLoader.GetMod("FaeBadLuck").Call("RegisterPermanentBuffItem", ItemID.ArtisanLoaf, (Player player) => { return player.ateArtisanBread; });
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            if (ModContent.GetInstance<ClientConfig>().PermanentBuffTracker) {
                if (ItemConsumedConditions.TryGetValue(item.type, out Func<Player, bool> condition)) {
                    if (condition.Invoke(Main.player[Main.myPlayer])) {
                        tooltips.Add(new TooltipLine(this.Mod, "already_consumed", Language.GetTextValue("Mods.FaeQOL.ItemAlreadConsumed")));
                    }
                }
            }
        }

    }
}
