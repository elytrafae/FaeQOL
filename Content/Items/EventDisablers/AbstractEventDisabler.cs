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

        public abstract bool IsEventDisabled { get; set; }
        public virtual SoundStyle UseSound => SoundID.AbigailSummon;
        public virtual Color AnnouncementColor => Color.Gold;

        public sealed override string LocalizationCategory => base.LocalizationCategory + ".EventDisablers";
        public LocalizedText EventDisabledMessage => this.GetLocalization(nameof(EventDisabledMessage));

        public override void SetStaticDefaults() {
            _ = EventDisabledMessage;
            PermanentBuffTracker.ItemConsumedConditions.Add(Type, (player) => IsEventDisabled);
        }

        public sealed override void SetDefaults() {
            Item.noMelee = true;
            Item.value = 0;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.UseSound = UseSound;
            Item.width = 80;
            Item.height = 80;
        }

        public override bool CanUseItem(Player player) {
            return !IsEventDisabled;
        }

        public override bool? UseItem(Player player) {
            IsEventDisabled = true;
            ChatHelper.DisplayMessage(NetworkText.FromKey(EventDisabledMessage.Key), AnnouncementColor, byte.MaxValue);
            return true;
        }

    }
}
