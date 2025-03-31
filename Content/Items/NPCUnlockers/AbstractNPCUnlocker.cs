using Terraria.Chat;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using FaeQOL.Systems.Config;
using FaeQOL.Systems;

namespace FaeQOL.Content.Items.NPCUnlockers {
    public abstract class AbstractNPCUnlocker : ModItem {

        public override bool IsLoadingEnabled(Mod mod) {
            return ModContent.GetInstance<ServerConfig>().EnableNPCUnlockers;
        }

        public const int DEFAULT_WORKBENCH = TileID.WorkBenches;

        public abstract int NPCToUnlock { get; }
        public abstract bool IsNPCSaved {get; set;}
        public virtual SoundStyle UseSound => SoundID.Unlock;
        public virtual Color AnnouncementColor => Color.Gold;
        public abstract int Width { get; }
        public abstract int Height { get; }

        public sealed override string LocalizationCategory => base.LocalizationCategory + ".NPCUnlockers";

        public sealed override void SetStaticDefaults() {
            // This is set to true for all NPCs that can be summoned via an Item (calling NPC.SpawnOnPlayer). If this is for a modded boss,
            // write this in the bosses file instead
            NPCID.Sets.MPAllowedEnemies[NPCToUnlock] = true;
            PermanentBuffTracker.ItemConsumedConditions.Add(Type, (player, item) => IsNPCSaved);
        }

        public sealed override void SetDefaults() {
            Item.noMelee = true;
            Item.value = 0;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.makeNPC = NPCToUnlock;
            Item.UseSound = UseSound;
            Item.width = Width;
            Item.height = Height;
        }

        public sealed override bool CanUseItem(Player player) {
            if (IsNPCSaved) {
                return false;
            }
            Vector2 mousePos = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            return !Collision.SolidCollision(mousePos, player.width, player.height); // Town NPCs are typically as big as the player
        }

        public sealed override void OnConsumeItem(Player player) {
            ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.FaeQOL.NPCUnlockedText", ContentSamples.NpcsByNetId[NPCToUnlock].TypeName), AnnouncementColor);
            IsNPCSaved = true;
        }

    }
}
