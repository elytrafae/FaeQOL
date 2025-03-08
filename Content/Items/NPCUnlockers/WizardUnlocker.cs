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
    internal class WizardUnlocker : AbstractNPCUnlocker {
        public override int NPCToUnlock => NPCID.Wizard;
        public override bool IsNPCSaved { 
            get => NPC.savedWizard; 
            set => NPC.savedWizard = value; 
        }
        public override SoundStyle UseSound => SoundID.MaxMana;
        public override Color AnnouncementColor => new Color(18, 144, 255);

        public override int Width => 38;

        public override int Height => 35;

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.Pearlwood, 5)
                .AddIngredient(ItemID.SoulofLight, 4)
                .AddIngredient(ItemID.SoulofNight, 4)
                .AddTile(DEFAULT_WORKBENCH)
                .Register();
        }
    }
}
