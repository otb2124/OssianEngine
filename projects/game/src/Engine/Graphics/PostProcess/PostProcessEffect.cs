using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphics
{
    /// <summary>
    /// Base class for a single fullscreen post-process pass.
    /// Subclass this and override Apply() to set shader parameters before the blit.
    /// The PostProcessManager handles the render targets and calls Apply() automatically.
    /// </summary>
    public abstract class PostProcessEffect
    {
        public Effect Shader;
        public bool Enabled = true;

        protected PostProcessEffect(Effect shader) { Shader = shader; }

        public virtual void Apply(Texture2D source, GameTime gameTime) { }  // ← Texture2D
    }
}