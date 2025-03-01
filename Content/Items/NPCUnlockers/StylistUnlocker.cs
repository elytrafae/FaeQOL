using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace FaeQOL.Content.Items.NPCUnlockers {
    internal class StylistUnlocker : AbstractNPCUnlocker {
        public override int NPCToUnlock => NPCID.Stylist;

        public override bool IsNPCSaved { 
            get => NPC.savedStylist; 
            set => NPC.savedStylist = value; 
        }

        public override SoundStyle UseSound => SoundID.Meowmere;
        public override Color AnnouncementColor => Color.DeepPink;

        public override int Width => 36;

        public override int Height => 36;

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.SpiderBanner, 2)
                .AddTile(DEFAULT_WORKBENCH)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.BlackRecluseBanner, 2)
                .AddTile(DEFAULT_WORKBENCH)
                .Register();
        }
    }
}
