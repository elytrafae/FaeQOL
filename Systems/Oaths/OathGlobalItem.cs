using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FaeQOL.Systems.Oaths {
    public class OathGlobalItem : GlobalItem {

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            if (item.damage > 0 && Main.LocalPlayer.GetModPlayer<OathPlayer>().DoOathsBlockDamage(item.DamageType)) {
                tooltips.Add(new TooltipLine(Mod, "OathsBlockDamage", Language.GetTextValue("Mods.FaeQOL.OathsBlockDamage")));
            }
        }

    }
}
