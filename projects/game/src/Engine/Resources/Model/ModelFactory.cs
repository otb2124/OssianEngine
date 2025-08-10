using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Utils;

namespace Resources
{
    public static class ModelFactory
    {

        

        public class ModelPreset
        {
            public FlatBodyPreset bodyPreset;
            public StaticSpriteFactory.SpriteData spriteData;
            public Vector2 offset;

            public ModelPreset(FlatBodyPreset bodyPreset, StaticSprites spritePreset, Vector2 offset)
            {
                this.bodyPreset = bodyPreset;
                this.spriteData = StaticSpriteFactory.spriteMappings[spritePreset];
                this.offset = offset;
            }

            public ModelPreset(FlatBodyPreset bodyPreset, StaticSpriteFactory.SpriteData spriteData, Vector2 offset)
            {
                this.bodyPreset = bodyPreset;
                this.spriteData = spriteData;
                this.offset = offset;
            }
        }

        private static readonly Dictionary<Models, ModelPreset> modelPresets = new()
        {
            { Models.PLAYER, new ModelPreset(FlatBodyPreset.HUMANOID, StaticSprites.ENTITIES_PLAYER, new Vector2(10, 5)) },
            { Models.CRATE_0, new ModelPreset(FlatBodyPreset.CRATE_0, StaticSprites.ENTITIES_STATIC_CRATE_0, Vector2.Zero) },
            { Models.CRATE_1, new ModelPreset(FlatBodyPreset.CRATE_1, StaticSprites.ENTITIES_STATIC_CRATE_1, Vector2.Zero) },
            { Models.BALL, new ModelPreset(FlatBodyPreset.CIRCLE, StaticSprites.ENTITIES_STATIC_BALL, Vector2.Zero) },
            { Models.BANDIT, new ModelPreset(FlatBodyPreset.HUMANOID, StaticSprites.ENTITIES_BANDIT, new Vector2(10, 5)) },
            { Models.SLIME, new ModelPreset(FlatBodyPreset.ANIMAL, StaticSprites.ENTITIES_SLIME,  new Vector2(0, 0)) },

            { Models.LEDGE, new ModelPreset(FlatBodyPreset.LEDGE, StaticSprites.ENTITIES_LEDGE,  new Vector2(0, 0)) }
        };

        public static Model CreateModel(Models model)
        {
            if (!modelPresets.TryGetValue(model, out var preset))
            {
                throw new ArgumentOutOfRangeException(nameof(model), model, "Invalid Model type");
            }

            return new Model(preset);
        }

        public static Model CreateModel(StaticSprites sprite, FlatBodyPreset body)
        {
            return new Model(new ModelPreset(body, sprite, new Vector2(0, 0)));
        }

        public static Model CreateModel(StaticSpriteFactory.SpriteData spriteData, FlatBodyPreset body)
        {
            return new Model(new ModelPreset(body, spriteData, new Vector2(0, 0)));
        }
    }
}
