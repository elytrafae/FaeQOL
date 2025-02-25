using FaeQOL.Content.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace FaeQOL.Systems {
    internal class MyModPlayer : ModPlayer {

        public List<Keychain> keychainsInInventory = new();

        public override void ResetEffects() {
            keychainsInInventory.Clear();
        }

        public static MyModPlayer Get(Player player) {
            return player.GetModPlayer<MyModPlayer>();
        }

    }
}
