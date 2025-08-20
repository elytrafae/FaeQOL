using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace FaeQOL.Systems.Config {
    internal class ServerConfig : ModConfig {

        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Slider]
        [Range(-1, 60)]
        [DefaultValue(5)]
        public int NonBossRespawnTimeCap;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool EnableKeychain;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool EnableNPCUnlockers;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool EnableEventDisablers;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool EnableBannerCollector;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool BannerCollectorEffects;

        [Header("Experimental")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool EnableOaths;

    }
}
