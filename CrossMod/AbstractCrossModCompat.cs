﻿using FaeQOL.Content.Items.ClassOaths;
using FaeQOL.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.CrossMod {
    internal abstract class AbstractCrossModCompat : ModSystem {

        

        public abstract string ModName { get; }

        /// <summary>
        /// This is for buffs that should stay permanently (like the Furniture buffs)
        /// </summary>
        public virtual List<string> PermanentStayBuffs => [];

        /// <summary>
        /// This is for permanent buff items (like Life Crystals)
        /// </summary>
        public virtual Dictionary<string, Func<Player, Item, bool>> PermanentBuffItemConditions => [];

        public virtual List<string> KeychainKeys => [];

        public abstract void CrossCompatAddOaths(Mod mod, Mod myMod);

        public abstract void CrossCompatAddILAndDetours(Mod mod);

        public virtual void CrossCompatPostSetupContent(Mod mod) {

        }

        ////////////////////
        // Implementation //
        ////////////////////

        public override bool IsLoadingEnabled(Mod mod) {
            return ModLoader.HasMod(ModName);
        }

        public override void Load() {
            Mod mod = ModLoader.GetMod(ModName);
            CrossCompatAddILAndDetours(mod);
        }

        public sealed override void PostSetupContent() {
            Mod mod = ModLoader.GetMod(ModName);
            CrossCompatPostSetupContent(mod);

            foreach (string keyID in KeychainKeys) {
                if (mod.TryFind(keyID, out ModItem keyItem)) {
                    CustomSetsSystem.RegisterKey(keyItem.Type);
                } else {
                    Console.WriteLine("FaeQOL Warning: Item with name " + keyID + " could not be found in mod " + mod.Name + "!");
                }
            }
            
            foreach (string buffID in PermanentStayBuffs) {
                if (mod.TryFind(buffID, out ModBuff modBuff)) {
                    PermaBuffsStaySystem.PermanentBuffs.Add(modBuff.Type);
                } else {
                    Console.WriteLine("FaeQOL Warning: Buff with name " + buffID + " could not be found in mod " + mod.Name + "!");
                }
            }

            foreach (var pair in PermanentBuffItemConditions) {
                if (mod.TryFind(pair.Key, out ModItem modItem)) {
                    PermanentBuffTracker.ItemConsumedConditions.Add(modItem.Type, pair.Value);
                } else {
                    Console.WriteLine("FaeQOL Warning: Item with name " + pair.Key + " could not be found in mod " + mod.Name + "!");
                }
            }
            
        }

        
        // Very hacky, but it works :/
        // This essentially overrides the Load method of this ModSystem and hijacks it
        public void Load(Mod myMod) {
            base.Register();
            Mod mod = ModLoader.GetMod(ModName);
            CrossCompatAddOaths(mod, myMod);
        }


    }
}
