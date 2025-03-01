using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Content.Items.TestItems {
    internal class TimeRipper : ModItem {

        public override bool IsLoadingEnabled(Mod mod) {
            return FaeQOL.TEST_MODE;
        }

        public override void SetDefaults() {
            Item.useTime = 1;
            Item.useAnimation = 1;
            Item.useStyle = ItemUseStyleID.DrinkOld;
            Item.UseSound = SoundID.Chat;
            Item.autoReuse = true;
        }

        public override bool? UseItem(Player player) {
            bool stopEvents = false;
            Main.UpdateTime_StartDay(ref stopEvents);
            stopEvents = false;
            Main.UpdateTime_StartNight(ref stopEvents);
            return null;
        }

    }
}
