using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using FaeQOL.Systems;

namespace FaeQOL.Content.Items.TestItems {
    internal class EnableAllEvents : ModItem {

        public override bool IsLoadingEnabled(Mod mod) {
            return FaeQOL.TEST_MODE;
        }

        public override void SetDefaults() {
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.DrinkOld;
            Item.UseSound = SoundID.Chat;
        }

        public override bool? UseItem(Player player) {
            foreach (EventDisableSystem.EventDisabledData data in EventDisableSystem.eventDisabledDatas) {
                data.isDisabled = false;
            }
            return null;
        }

    }
}
