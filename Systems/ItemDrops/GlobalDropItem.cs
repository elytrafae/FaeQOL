using FaeQOL.Content.Items;
using FaeQOL.Systems.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Systems.ItemDrops {
    internal class GlobalDropItem : GlobalItem {

        public override void ModifyItemLoot(Item item, ItemLoot itemLoot) {
            if (item.type == ItemID.SkeletronBossBag && ModContent.GetInstance<ServerConfig>().EnableKeychain) {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Keychain>()));
            }
            
        }

    }
}
