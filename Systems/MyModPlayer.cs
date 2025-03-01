using FaeQOL.Content.Items;
using FaeQOL.Systems.Config;
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

        public override void UpdateDead() {
            int deadTime = ModContent.GetInstance<ServerConfig>().NonBossRespawnTimeCap;
            if (deadTime > -1 && !Utilities.IsBossAlive()) {
                deadTime = Math.Max(deadTime * 60, 1); // Turn into ticks, but make sure there is at least 1 tick minimum for safety!
                if (Player.respawnTimer > deadTime) {
                    Player.respawnTimer = deadTime;
                }
            }
        }

    }
}
