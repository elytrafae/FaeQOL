using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Enums;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;

namespace FaeQOL.Content.Items.DevSet {

    [AutoloadEquip(EquipType.Wings)]
    public class FaeWings : ModItem {

        public override void SetStaticDefaults() {
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(150, 7f);
        }

        public override void SetDefaults() {
            Item.SetShopValues(ItemRarityColor.Cyan9, Terraria.Item.sellPrice(gold: 5));
            //Item.vanity = true; // Technically not vanity because you can use it to fly
            Item.width = 34;
            Item.height = 36;
            Item.accessory = true;
        }

        

    }
}
