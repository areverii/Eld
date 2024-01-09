using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eld {

    public static partial class EldUtils {

        public static void DrawItemGlowmaskSingleFrame(this Item item, SpriteBatch spriteBatch, float rotation, Texture2D glowmaskTexture)
        {
            Vector2 origin = new Vector2(glowmaskTexture.Width / 2f, glowmaskTexture.Height / 2f - 2f);
            spriteBatch.Draw(glowmaskTexture, item.Center - Main.screenPosition, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
        }


    }
}