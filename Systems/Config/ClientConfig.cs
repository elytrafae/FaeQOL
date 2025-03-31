using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace FaeQOL.Systems.Config {
    internal class ClientConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Items")]
        [DefaultValue(true)]
        public bool PermanentBuffTracker;

        [Header("Audio")]
        [Slider]
        [Range(0, 150)]
        [DefaultValue(30)]
        public int WhipNoiseVolume;

        [Slider]
        [Range(0, 150)]
        [DefaultValue(30)]
        public int DrillNoiseVolume;

        [Slider]
        [Range(0, 150)]
        [DefaultValue(30)]
        public int MagicMissleNoiseVolume;
    }
}
