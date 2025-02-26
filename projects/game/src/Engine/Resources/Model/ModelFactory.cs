using Microsoft.Xna.Framework;
using System;

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

        public static Model createModel(Models model)
        {
            Vector2 offset;

            switch (model)
            {
                case Models.HERO:
                    offset = new Vector2(10, 5);
                    return new Model(
                        FlatBodyFactory.createFlatBody(FlatBodyFactory.FlatBodyPreset.HUMANOID, offset),
                        ResourceLoader.sprites[SpriteFactory.Sprites.HERO])
                    { bodyOffset = offset };
                case Models.CRATE_BIG:
                    return new Model(
                        FlatBodyFactory.createFlatBody(FlatBodyFactory.FlatBodyPreset.BLOCK), 
                        ResourceLoader.sprites[SpriteFactory.Sprites.CRATE]);
                case Models.CRATE_SMALL:
                    return new Model(FlatBodyFactory.createFlatBody(
                        FlatBodyFactory.FlatBodyPreset.BOX), 
                        ResourceLoader.sprites[SpriteFactory.Sprites.CRATE]);
                case Models.BALL:
                    return new Model(FlatBodyFactory.createFlatBody(
                        FlatBodyFactory.FlatBodyPreset.CIRCLE), 
                        ResourceLoader.sprites[SpriteFactory.Sprites.CIRCLE]);
                case Models.PLATFORM:
                    return new Model(FlatBodyFactory.createFlatBody(
                        FlatBodyFactory.FlatBodyPreset.PLATFORM), 
                        ResourceLoader.sprites[SpriteFactory.Sprites.PLATFORM]);
                case Models.MOB:
                    offset = new Vector2(10, 5);
                    return new Model(FlatBodyFactory.createFlatBody(
                        FlatBodyFactory.FlatBodyPreset.HUMANOID, offset),
                        ResourceLoader.sprites[SpriteFactory.Sprites.MOB])
                    { bodyOffset = offset };
                default:
                    throw new ArgumentOutOfRangeException(nameof(model), model, null);
            }
        }



        public static SpriteFactory.Sprites GetSpriteFromModel(Models model)
        {
            switch (model)
            {
                case Models.HERO:
                    return SpriteFactory.Sprites.HERO;
                case Models.CRATE_BIG:
                    return SpriteFactory.Sprites.CRATE;
                case Models.CRATE_SMALL:
                    return SpriteFactory.Sprites.CRATE;
                case Models.BALL:
                    return SpriteFactory.Sprites.CIRCLE;
                case Models.PLATFORM:
                    return SpriteFactory.Sprites.PLATFORM;
                case Models.MOB:
                    return SpriteFactory.Sprites.MOB;
                default:
                    throw new ArgumentException("Unknown model type", nameof(model));
            }
        }


    }
}
