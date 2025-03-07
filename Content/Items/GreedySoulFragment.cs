using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace FaeQOL.Content.Items {
    public class GreedySoulFragment : ModItem {

        public override void SetStaticDefaults() {
            // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 8));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation

            ItemID.Sets.ItemIconPulse[Item.type] = true; // The item pulses while in the player's inventory
            ItemID.Sets.ItemNoGravity[Item.type] = true; // Makes the item have no gravity

            Item.ResearchUnlockCount = 25; // Configure the amount of this item that's needed to research it in Journey mode.
        }

        public override void SetDefaults() {
            Item.width = 22;
            Item.height = 22;
            Item.maxStack = Terraria.Item.CommonMaxStack;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Terraria.Item.buyPrice(0, 0, 0, 5);
        }

        public override void PostUpdate() {
            Lighting.AddLight(Item.Center, Color.DarkRed.ToVector3() * 0.55f * Main.essScale); // Makes this item glow when thrown out of inventory.
        }

    }
}
