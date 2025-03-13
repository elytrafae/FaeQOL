using FaeQOL.Systems.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Systems {
    internal class LowerObnoxiousSoundVolumeSystem : ModSystem {

        public override void Load() {
            On_SoundPlayer.Play += On_SoundPlayer_Play;
        }

        private ReLogic.Utilities.SlotId On_SoundPlayer_Play(On_SoundPlayer.orig_Play orig, SoundPlayer self, ref SoundStyle style, Microsoft.Xna.Framework.Vector2? position, SoundUpdateCallback updateCallback) {
            int volume = GetVolumePercent(style);
            if (volume != 100) {
                SoundStyle newStyle = new(style.SoundPath, style.Variants, style.Type);
                newStyle.Volume = style.Volume * volume / 100f;
                return orig(self, ref newStyle, position, updateCallback);
            } else {
                return orig(self, ref style, position, updateCallback);
            }
        }

        private int GetVolumePercent(SoundStyle style) {
            ClientConfig config = ModContent.GetInstance<ClientConfig>();
            if (style == SoundID.Item152 || style == SoundID.Item153) {
                // Whips
                return config.WhipNoiseVolume;
            }
            if (style == SoundID.Item9) {
                // Magic Missles
                return config.MagicMissleNoiseVolume;
            }
            if (style == SoundID.Item22 || style == SoundID.Item23) {
                // Drills
                return config.DrillNoiseVolume;
            }
            return 100;
        }

    }
}
