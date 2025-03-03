using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Utils;

namespace Resources
{
    public static class ModelFactory
    {

        

        public struct ModelPreset
        {
            public FlatBodyPreset bodyPreset;
            public StaticSprites spritePreset;
            public Vector2 offset;

            public ModelPreset(FlatBodyPreset bodyPreset, StaticSprites spritePreset, Vector2 offset)
            {
                this.bodyPreset = bodyPreset;
                this.spritePreset = spritePreset;
                this.offset = offset;
            }
        }

        private static readonly Dictionary<Models, ModelPreset> modelPresets = new()
        {
            { Models.PLAYER, new ModelPreset(FlatBodyPreset.HUMANOID, StaticSprites.ENTITIES_PLAYER, new Vector2(10, 5)) },
            { Models.CRATE_0, new ModelPreset(FlatBodyPreset.CRATE_0, StaticSprites.ENTITIES_STATIC_CRATE_0, Vector2.Zero) },
            { Models.CRATE_1, new ModelPreset(FlatBodyPreset.CRATE_1, StaticSprites.ENTITIES_STATIC_CRATE_1, Vector2.Zero) },
            { Models.BALL, new ModelPreset(FlatBodyPreset.CIRCLE, StaticSprites.ENTITIES_STATIC_BALL, Vector2.Zero) },
            { Models.PLATFORM, new ModelPreset(FlatBodyPreset.PLATFORM, StaticSprites.ENTITIES_STATIC_PLATFORM, Vector2.Zero) },
            { Models.MOB, new ModelPreset(FlatBodyPreset.HUMANOID, StaticSprites.ENTITIES_MOB0, new Vector2(10, 5)) }
        };

        public static Model CreateModel(Models model)
        {
            if (!modelPresets.TryGetValue(model, out var preset))
            {
                throw new ArgumentOutOfRangeException(nameof(model), model, "Invalid model type");
            }

            return new Model(preset);
        }

        public static StaticSprites GetSpriteFromModel(Models model)
        {
            if (modelPresets.TryGetValue(model, out var preset))
            {
                return preset.spritePreset;
            }

            throw new ArgumentException("Unknown model type", nameof(model));
        }
    }
}
