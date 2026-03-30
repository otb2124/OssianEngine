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
        /// <summary>The compiled Effect loaded from the Content pipeline.</summary>
        public Effect Shader { get; protected set; }

        /// <summary>
        /// Whether this effect is currently active.
        /// Inactive effects are skipped entirely (no render target switch, no blit).
        /// </summary>
        public bool Enabled = true;

        protected PostProcessEffect(Effect shader)
        {
            Shader = shader;
        }

        /// <summary>
        /// Called once per frame before the effect's blit draw call.
        /// Set any shader parameters here (time, intensity, resolution, etc.).
        /// <param name="source">The texture the shader will sample from.</param>
        /// <param name="gameTime">Current game time for animated effects.</param>
        /// </summary>
        public abstract void Apply(Texture2D source, GameTime gameTime);
    }
}