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
            { Models.HERO, new ModelPreset(FlatBodyPreset.HUMANOID, StaticSprites.HERO, new Vector2(10, 5)) },
            { Models.CRATE_BIG, new ModelPreset(FlatBodyPreset.BLOCK, StaticSprites.CRATE, Vector2.Zero) },
            { Models.CRATE_SMALL, new ModelPreset(FlatBodyPreset.BOX, StaticSprites.CRATE_SMALL, Vector2.Zero) },
            { Models.BALL, new ModelPreset(FlatBodyPreset.CIRCLE, StaticSprites.CIRCLE, Vector2.Zero) },
            { Models.PLATFORM, new ModelPreset(FlatBodyPreset.PLATFORM, StaticSprites.PLATFORM, Vector2.Zero) },
            { Models.MOB, new ModelPreset(FlatBodyPreset.HUMANOID, StaticSprites.MOB, new Vector2(10, 5)) }
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
