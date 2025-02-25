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
    }
}
