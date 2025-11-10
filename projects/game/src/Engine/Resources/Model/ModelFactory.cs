using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using Utils;

namespace Resources
{
    public static class ModelFactory
    {

        

        public class ModelPreset
        {
            public StaticSprite SpriteData;
            public PhysicalBody Body;
            public Vector2 Offset;

            public ModelPreset(PhysicalBodies bodyPreset, StaticSprites spritePreset, Vector2 offset)
            {
                Body = PhysicalBodyFactory.CreatePhysicalBody(bodyPreset, offset);
                SpriteData = StaticSpriteFactory.StaticSpriteMappings[spritePreset];
                Offset = offset;
            }

            public ModelPreset(PhysicalBody body, StaticSprites spritePreset, Vector2 offset)
            {
                Body = body;
                SpriteData = StaticSpriteFactory.StaticSpriteMappings[spritePreset];
                Offset = offset;
            }

            public ModelPreset(PhysicalBodies bodyPreset, StaticSprite spriteData, Vector2 offset)
            {
                Body = PhysicalBodyFactory.CreatePhysicalBody(bodyPreset, offset);
                SpriteData = spriteData;
                Offset = offset;
            }
        }


        //TODO: FIX TO SET THE OFFSET FOR ALL AS new Vector2(X, Y: 16)
        private static readonly Dictionary<Models, ModelPreset> ModelPresetsMap = new()
        {
            { Models.HUMAN_M, new ModelPreset(PhysicalBodies.HUMAN, StaticSprites.ENTITIES_HUMAN_M, new Vector2(10, 5)) },
            { Models.CRATE_0, new ModelPreset(PhysicalBodies.CRATE_0, StaticSprites.ENTITIES_STATIC_CRATE_0, Vector2.Zero) },
            { Models.CRATE_1, new ModelPreset(PhysicalBodies.CRATE_1, StaticSprites.ENTITIES_STATIC_CRATE_1, Vector2.Zero) },
            { Models.BALL, new ModelPreset(PhysicalBodies.CIRCLE, StaticSprites.ENTITIES_STATIC_BALL, Vector2.Zero) },
            { Models.SLIME, new ModelPreset(PhysicalBodies.SLIME, StaticSprites.ENTITIES_SLIME,  new Vector2(0, 0)) },
            { Models.BAT, new ModelPreset(PhysicalBodies.SLIME, StaticSprites.ENTITIES_BAT,  new Vector2(0, 0)) },

            { Models.LEDGE, new ModelPreset(PhysicalBodies.LEDGE, StaticSprites.ENTITIES_LEDGE,  new Vector2(0, 0)) }
        };

        public static Model CreateModel(Models model)
        {
            if (!ModelPresetsMap.TryGetValue(model, out var preset))
            {
                throw new ArgumentOutOfRangeException(nameof(model), model, "Invalid Model type");
            }

            return new Model(preset);
        }

        public static Model CreateModel(StaticSprites sprite, PhysicalBodies bodyPreset)
        {
            return new Model(new ModelPreset(bodyPreset, sprite, new Vector2(0, 0)));
        }

        public static Model CreateModel(StaticSprites sprite, PhysicalBody body)
        {
            return new Model(new ModelPreset(body, sprite, new Vector2(0, 0)));
        }

        public static Model CreateModel(StaticSprite spriteData, PhysicalBodies bodyPreset)
        {
            return new Model(new ModelPreset(bodyPreset, spriteData, new Vector2(0, 0)));
        }
    }
}
