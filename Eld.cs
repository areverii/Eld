using Eld.Effects;
using Terraria;
using Terraria.ModLoader;
using Eld.Projectiles.BaseProjectiles;

namespace Eld
{
	public class Eld : Mod
	{
		internal static Eld Instance;

        public override void Load()
        {
            Instance = this;

			/* LOAD OTHER MODS HERE */

			if (!Main.dedServ)
			{
				LoadClient();
				//ForegroundDrawing.ForegroundManager.Load();
			}

			BaseIdleHoldoutProjectile.LoadAll();


        }

        private void LoadClient() {

			EldShaders.LoadShaders();

		}
	}
}