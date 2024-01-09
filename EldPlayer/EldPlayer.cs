

using System;
using System.Collections.Generic;
using System.Linq;



using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Eld.EldPl {

    public partial class EldPlayer : ModPlayer {
        
        

        #region Mouse Control Syncing
        public bool mouseRight = false;
        private bool oldMouseRight = false;
        public Vector2 mouseWorld;
        private Vector2 oldMouseWorld;

        public bool mouseWorldListener = false;
        public bool rightClickListener = false;
        public bool mouseRotationListener = false;

        public bool syncMouseControls = false;
        #endregion


        public override void PreUpdate()
        {
            

            // Syncing mouse controls
            if (Main.myPlayer == Player.whoAmI)
            {
                mouseRight = PlayerInput.Triggers.Current.MouseRight;
                mouseWorld = Main.MouseWorld;

                if (rightClickListener && mouseRight != oldMouseRight)
                {
                    oldMouseRight = mouseRight;
                    syncMouseControls = true;
                    rightClickListener = false;
                }
                if (mouseWorldListener && Vector2.Distance(mouseWorld, oldMouseWorld) > 5f)
                {
                    oldMouseWorld = mouseWorld;
                    syncMouseControls = true;
                    mouseWorldListener = false;
                }
                if (mouseRotationListener && Math.Abs((mouseWorld - Player.MountedCenter).ToRotation() - (oldMouseWorld - Player.MountedCenter).ToRotation()) > 0.15f)
                {
                    oldMouseWorld = mouseWorld;
                    syncMouseControls = true;
                    mouseRotationListener = false;
                }
            }
        }

    }



}