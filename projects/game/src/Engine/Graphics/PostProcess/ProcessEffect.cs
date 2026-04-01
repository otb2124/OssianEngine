using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;

namespace Graphics
{
    /// <summary>
    /// Base class for a single fullscreen post-process pass.
    /// Subclass this and override Apply() to set shader parameters before the blit.
    /// The PostProcessManager handles the render targets and calls Apply() automatically.
    /// </summary>
    public abstract class ProcessEffect
    {
        public Effect Shader;
        public bool Enabled = true;
        public Shaders ShaderType;

        protected ProcessEffect() { }

        public virtual void LoadShader()
        {
            Shader = ResourceLoader.shaders[ShaderType].Shader;
        }

        public virtual void Apply(Texture2D source, GameTime gameTime) { }  // ← Texture2D

        public virtual void ApplyMultiPass(Sprites sprites,
            RenderTarget2D source, RenderTarget2D target, RenderTarget2D scratch) { }

        public virtual void Trigger() { }
    }
}