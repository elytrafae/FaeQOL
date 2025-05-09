using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FaeQOL.Systems.PreventStupidUIClicksDuringCombat {
    public class PreventUIClicksPlayer : ModPlayer {

        public int disabledTime = 0;

        public static bool IsUIDisabled() {
            return Main.LocalPlayer.GetModPlayer<PreventUIClicksPlayer>().disabledTime > 0;
        }

        public override void PostUpdate() {
            if (disabledTime > 0) {
                disabledTime--;
            }
            if (Player.itemTime > 0) {
                disabledTime = 10;
            }
            //ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Disabled Time: " + disabledTime), Color.Azure);
        }

    }
}
