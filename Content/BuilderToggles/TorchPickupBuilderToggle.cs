using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

// TODO: Continue this (below code is from examplemod)
namespace FaeQOL.Content.BuilderToggles {

    public class TorchPickupBuilderToggle : BuilderToggle {
		public static LocalizedText OnText { get; private set; }
		public static LocalizedText OffText { get; private set; }
		static readonly Color grayColor = new Color(0.6f, 0.6f, 0.6f);

		public static Asset<Texture2D> OnTexture { get; private set; }
        public static Asset<Texture2D> OnHoverTexture { get; private set; }

        public override void SetStaticDefaults() {
			OnText = this.GetLocalization(nameof(OnText));
			OffText = this.GetLocalization(nameof(OffText));
			OnTexture = ModContent.Request<Texture2D>(Texture + "_On");
            OnHoverTexture = ModContent.Request<Texture2D>(Texture + "_HoverOn");

        }

		public override bool Active() => Main.LocalPlayer.unlockedBiomeTorches;

		public override int NumberOfStates => 2;

		public override string DisplayValue() {
			return CurrentState == 1 ? OnText.Value : OffText.Value;
		}

        public override string HoverTexture => Texture + "_Hover";

		public override bool Draw(SpriteBatch spriteBatch, ref BuilderToggleDrawParams drawParams) {
			//drawParams.Color = CurrentState == 1 ? Color.White : grayColor;
			if (CurrentState != 1) {
				return true;
			}

            if (drawParams.Color.Equals(Color.White)) { // This is the icon itself 
				drawParams.Texture = OnTexture.Value;
            } else { // This is the outline
				drawParams.Texture = OnHoverTexture.Value;
            }
				
            return true;
		}

	}

}