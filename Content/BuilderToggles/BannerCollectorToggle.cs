using FaeQOL.Systems.Config;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FaeQOL.Content.BuilderToggles {
    internal class BannerCollectorToggle : BuilderToggle {

        public static LocalizedText Text { get; private set; }

        public override void SetStaticDefaults() {
            Text = this.GetLocalization(nameof(Text));
        }

        public override string HoverTexture => Texture + "_Hover";

        public override bool Active() => ModContent.GetInstance<ServerConfig>().EnableBannerCollector;

        public override int NumberOfStates => 2;

        public override string DisplayValue() {
            return Text.Value;
        }

        public bool IsOn {
            get => this.CurrentState == 1;
            set => this.CurrentState = value ? 1 : 0;
        }

        public static BannerCollectorToggle Get() {
            return ModContent.GetInstance<BannerCollectorToggle>();
        }

    }
}
