using FaeQOL.Content.Items;
using FaeQOL.Content.Items.ClassOaths;
using FaeQOL.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace FaeQOL {
    public class FaeQOL : Mod {

        public const bool TEST_MODE = false;

        public override object Call(params object[] args) {
            if (args[0] is not string) {
                throw new ArgumentException(Name + "'s Mod.Call must start with a string command name!");
            }
            string command = (string)args[0];
            switch (command) {
                case "RegisterKey":
                case "RegisterKeyType":
                    AssertType(args, 1, out int keyType);
                    ItemSets.RegisterKey(keyType);
                    return true;
                
                case "GetKeyFromKeychains":
                case "GetKeyStackFromKeychains":
                    AssertType(args, 1, out Player player);
                    if (AssertType(args, 2, out int keyType2, out Item keyTemplateItem)) {
                        return Utilities.SearchForKeyInKeychains(player, keyType2);
                    } else {
                        return Utilities.SearchForKeyInKeychains(player, keyTemplateItem);
                    }

                case "RegisterPermanentBuff":
                    AssertType(args, 1, out int itemType);
                    if (AssertType(args, 2, out Func<Player, bool> func1, out Func<Player, Item, bool> func2)) {
                        PermanentBuffTracker.ItemConsumedConditions.Add(itemType, (player, _) => func1.Invoke(player));
                    } else {
                        PermanentBuffTracker.ItemConsumedConditions.Add(itemType, func2);
                    }
                    return true;

                case "RegisterBuffThatShouldStayOnForever":
                case "RegisterFurnitureBuff":
                    AssertType(args, 1, out int buffType);
                    PermaBuffsStaySystem.PermanentBuffs.Add(buffType);
                    return true;

                case "CreateOath":
                case "CreateClassOath":
                    AssertType(args, 1, out Mod otherMod);
                    AssertType(args, 2, out DamageClass dmgClass);
                    AssertType(args, 3, out string itemNameWithoutOath);
                    AssertType(args, 4, out Color color);
                    AssertType(args, 5, out Tuple<int, int>[] itemsToGive);
                    FilledOath oath = new FilledOath(dmgClass, itemNameWithoutOath, color, itemsToGive);
                    otherMod.AddContent(oath);
                    return oath;

                case "RegisterTorchGodTorch":
                    AssertType(args, 1, out int torchType);
                    ItemSets.IsTorchGodTorch.Add(torchType);
                    return true;

                case "RegisterTorchGodCampfire":
                    AssertType(args, 1, out int campfireType);
                    ItemSets.IsTorchGodCampfire.Add(campfireType);
                    return true;

                default:
                    throw new ArgumentException(Name + "'s Mod.Call could not find a command with the provided name! Please check the documentation for valid names.");
            }
            // No need for a return here.
        }

        private bool TryGetOfType<T>(object[] args, int index, out T var) {
            if (index >= args.Length) {
                var = default; 
                return false;
            }
            if (args[index] is not T) {
                var = default;
                return false;
            }
            var = (T)args[index];
            return true;
        }

        private void AssertType<T>(object[] args, int index, out T var) {
            if (TryGetOfType(args, index, out T v)) {
                var = v;
            } else {
                throw new ArgumentException(Name + "'s Mod.Call threw an exception because argument #" + index + " is not of type " + nameof(T) + "!");
            }
        }

        private bool AssertType<T1, T2>(object[] args, int index, out T1 var1, out T2 var2) {
            var1 = default;
            var2 = default;
            if (TryGetOfType(args, index, out T1 v1)) {
                var1 = v1;
                return true;
            } else if (TryGetOfType(args, index, out T2 v2)) {
                var2 = v2;
                return false;
            } else {
                throw new ArgumentException(Name + "'s Mod.Call threw an exception because argument #" + index + " is not of type " + nameof(T1) + " or of type " + nameof(T2) + "!");
            }
        }

    }
}
