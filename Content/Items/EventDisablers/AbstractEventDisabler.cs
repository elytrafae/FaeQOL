using FaeQOL.Systems;
using FaeQOL.Systems.Config;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace FaeQOL.Content.Items.EventDisablers {
    internal abstract class AbstractEventDisabler : ModItem {

        public override bool IsLoadingEnabled(Mod mod) {
            return ModContent.GetInstance<ServerConfig>().EnableEventDisablers;
        }

        public abstract EventDisableSystem.EventDisabledData EventDisableData { get; }
        public virtual SoundStyle UseSound => SoundID.AbigailSummon;
        public virtual Color AnnouncementColor => Color.Gold;
        public abstract int ItemToGetThisFrom { get; }

        public abstract int Width { get; }
        public abstract int Height { get; }

        public sealed override string LocalizationCategory => base.LocalizationCategory + ".EventDisablers";
        public LocalizedText EventDisabledMessage => this.GetLocalization(nameof(EventDisabledMessage));

        public sealed override void SetStaticDefaults() {
            _ = EventDisabledMessage;
            PermanentBuffTracker.ItemConsumedConditions.Add(Type, (player, item) => EventDisableData.isDisabled);
            ItemID.Sets.ShimmerTransformToItem[ItemToGetThisFrom] = Type;
        }

        public sealed override void SetDefaults() {
            Item.noMelee = true;
            Item.value = 0;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.UseSound = UseSound;
            Item.width = Width;
            Item.height = Height;
        }

        public override bool CanUseItem(Player player) {
            return !EventDisableData.isDisabled;
        }

        public override bool? UseItem(Player player) {
            EventDisableData.isDisabled = true;
            ChatHelper.DisplayMessage(NetworkText.FromKey(EventDisabledMessage.Key), AnnouncementColor, byte.MaxValue);
            return true;
        }

    }
}
