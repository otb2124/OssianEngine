using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Resources
{
    public static class ModelFactory
    {

        public enum Models
        {
            HERO,
            CRATE_BIG,
            CRATE_SMALL,
            BALL,
            PLATFORM,
            MOB
        }

        public struct ModelPreset
        {
            public FlatBodyFactory.FlatBodyPreset bodyPreset;
            public StaticSpriteFactory.StaticSprites spritePreset;
            public Vector2 offset;

            public ModelPreset(FlatBodyFactory.FlatBodyPreset bodyPreset, StaticSpriteFactory.StaticSprites spritePreset, Vector2 offset)
            {
                this.bodyPreset = bodyPreset;
                this.spritePreset = spritePreset;
                this.offset = offset;
            }
        }

        private static readonly Dictionary<Models, ModelPreset> modelPresets = new()
        {
            { Models.HERO, new ModelPreset(FlatBodyFactory.FlatBodyPreset.HUMANOID, StaticSpriteFactory.StaticSprites.HERO, new Vector2(10, 5)) },
            { Models.CRATE_BIG, new ModelPreset(FlatBodyFactory.FlatBodyPreset.BLOCK, StaticSpriteFactory.StaticSprites.CRATE, Vector2.Zero) },
            { Models.CRATE_SMALL, new ModelPreset(FlatBodyFactory.FlatBodyPreset.BOX, StaticSpriteFactory.StaticSprites.CRATE, Vector2.Zero) },
            { Models.BALL, new ModelPreset(FlatBodyFactory.FlatBodyPreset.CIRCLE, StaticSpriteFactory.StaticSprites.CIRCLE, Vector2.Zero) },
            { Models.PLATFORM, new ModelPreset(FlatBodyFactory.FlatBodyPreset.PLATFORM, StaticSpriteFactory.StaticSprites.PLATFORM, Vector2.Zero) },
            { Models.MOB, new ModelPreset(FlatBodyFactory.FlatBodyPreset.HUMANOID, StaticSpriteFactory.StaticSprites.MOB, new Vector2(10, 5)) }
        };

        public static Model CreateModel(Models model)
        {
            if (!modelPresets.TryGetValue(model, out var preset))
            {
                throw new ArgumentOutOfRangeException(nameof(model), model, "Invalid model type");
            }

            return new Model(preset);
        }

        public static StaticSpriteFactory.StaticSprites GetSpriteFromModel(Models model)
        {
            if (modelPresets.TryGetValue(model, out var preset))
            {
                return preset.spritePreset;
            }

            throw new ArgumentException("Unknown model type", nameof(model));
        }


    }
}
