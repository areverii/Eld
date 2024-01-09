
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace Eld.Effects {

    public class EldShaders {
        private const string ShaderPath = "Effects/";
        internal const string EldShaderPrefix = "Eld:";


        /* Used by God of the Forest for laser sight */
        internal static Effect PixelatedSightLine;

        private static void RegisterMiscShader(Effect shader, string passName, string registrationName)
        {
            Ref<Effect> shaderPointer = new(shader);
            MiscShaderData passParamRegistration = new(shaderPointer, passName);
            GameShaders.Misc[$"{EldShaderPrefix}{registrationName}"] = passParamRegistration;
        }

        private static void RegisterSceneFilter(ScreenShaderData passReg, string registrationName, EffectPriority priority = EffectPriority.High)
        {
            string prefixedRegistrationName = $"{EldShaderPrefix}{registrationName}";
            Filters.Scene[prefixedRegistrationName] = new Filter(passReg, priority);
            Filters.Scene[prefixedRegistrationName].Load();
        }

        // Shorthand to register a loaded shader in Terraria's graphics engine
        // All shaders registered this way are accessible under Filters.Scene
        // They will use the prefix described above
        private static void RegisterScreenShader(Effect shader, string passName, string registrationName, EffectPriority priority = EffectPriority.High)
        {
            Ref<Effect> shaderPointer = new(shader);
            ScreenShaderData passParamRegistration = new(shaderPointer, passName);
            RegisterSceneFilter(passParamRegistration, registrationName, priority);
        }

        public static void LoadShaders()
        {
            if (Main.dedServ)
                return;

            AssetRepository eldAssets = Eld.Instance.Assets;


            Effect LoadShader(string path) => eldAssets.Request<Effect>($"{ShaderPath}{path}", AssetRequestMode.ImmediateLoad).Value;


            PixelatedSightLine = LoadShader("PixelatedSightLine");
            RegisterScreenShader(PixelatedSightLine, "SightLinePass", "PixelatedSightLine");


        }
    }

}



