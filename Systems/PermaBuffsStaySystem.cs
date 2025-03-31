using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Systems {
    internal class PermaBuffsStaySystem : ModSystem {

        public static List<int> PermanentBuffs = [BuffID.Sharpened, BuffID.Bewitched, BuffID.WarTable, BuffID.AmmoBox, BuffID.Clairvoyance];

        public override void PostSetupRecipes() {
            foreach (int id in PermanentBuffs) {
                Main.buffNoSave[id] = false;
                Main.persistentBuff[id] = true;
            }
        }

    }
}
